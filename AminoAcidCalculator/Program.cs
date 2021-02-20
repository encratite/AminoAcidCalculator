using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AminoAcidCalculator
{
	class Program
	{
		static void Main(string[] arguments)
		{
			var currentProfiles = new AminoAcidProfile[] { };
			var optimalSource = new OptimalSource();
			var optimalSource2 = new OptimalSource();
			int granularity = 10;
			Evaluate(granularity, true, currentProfiles, Profiles.Sources, optimalSource, optimalSource2);
			PrintOptimalSource("Optimal combination of plant-based protein sources for muscle gain", optimalSource, granularity);
			PrintOptimalSource("When limiting blend to just two ingredients", optimalSource2, granularity);
			foreach (var profile in Profiles.Sources)
			{
				var simpleProfile = new AminoAcidProfile[] { profile };
				decimal muscleGain = GetMuscleGain(simpleProfile);
				Console.WriteLine($"In comparison to 100% \"{profile.Source}\" protein: {muscleGain * 100:0.0} g");
			}
		}

		private static void PrintOptimalSource(string title, OptimalSource optimalSource, int granularity)
		{
			var composition = new Dictionary<string, int>();
			foreach (var profile in optimalSource.Profile)
			{
				string key = profile.Source;
				if (!composition.ContainsKey(key))
					composition[key] = 0;
				composition[key]++;
			}
			Console.WriteLine($"{title}:");
			foreach (var pair in composition.OrderByDescending(pair => pair.Value))
				Console.WriteLine($"    {(decimal)pair.Value / granularity:P0} {pair.Key}");
			Console.WriteLine($"Theoretical muscle gain per 100 g of pure protein from this blend: {optimalSource.MuscleGain.Value / granularity * 100:0.0} g");
			Console.WriteLine(string.Empty);
		}

		private static void Evaluate
		(
			int remainingCombinations,
			bool enableMultithreading,
			IEnumerable<AminoAcidProfile> currentProfiles,
			IEnumerable<AminoAcidProfile> profiles,
			OptimalSource optimalSource,
			OptimalSource optimalSource2
		)
		{
			if (remainingCombinations <= 0)
			{
				decimal muscleGain = GetMuscleGain(currentProfiles);
				UpdateOptimalSource(muscleGain, currentProfiles, optimalSource);
				var sourceNames = new HashSet<string>();
				foreach (var profile in currentProfiles)
					sourceNames.Add(profile.Source);
				if (sourceNames.Count <= 2)
					UpdateOptimalSource(muscleGain, currentProfiles, optimalSource2);
				return;
			}
			int newRemainingCombinations = remainingCombinations - 1;
			var tasks = new List<Task>();
			foreach (var profile in profiles)
			{
				var newProfileList = new AminoAcidProfile[] { profile };
				var newCurrentProfiles = currentProfiles.Concat(newProfileList);
				Action evaluate = () => Evaluate(newRemainingCombinations, false, newCurrentProfiles, profiles, optimalSource, optimalSource2);
				if (enableMultithreading)
				{
					var task = new Task(evaluate);
					task.Start();
					tasks.Add(task);
				}
				else
				{
					evaluate();
				}
			}
			if (enableMultithreading)
			{
				foreach (var task in tasks)
					task.Wait();
			}
		}

		private static void UpdateOptimalSource(decimal muscleGain, IEnumerable<AminoAcidProfile> currentProfiles, OptimalSource optimalSource)
		{
			if (!optimalSource.MuscleGain.HasValue || muscleGain > optimalSource.MuscleGain.Value)
			{
				optimalSource.MuscleGain = muscleGain;
				optimalSource.Profile = currentProfiles;
			}
		}

		private static decimal GetMuscleGain(IEnumerable<AminoAcidProfile> profiles)
		{
			var muscleVector = Profiles.HumanMuscle.GetAminoAcidVector();
			var compositionVector = new int[muscleVector.Length];
			for (int i = 0; i < compositionVector.Length; i++)
				compositionVector[i] = 0;
			foreach (var profile in profiles)
			{
				var sourceVector = profile.GetAminoAcidVector();
				for (int i = 0; i < compositionVector.Length; i++)
					compositionVector[i] += sourceVector[i];
			}
			decimal? muscleGain = null;
			for (int i = 0; i < compositionVector.Length; i++)
			{
				decimal aminoAcidGain = (decimal)compositionVector[i] / muscleVector[i];
				if (!muscleGain.HasValue || aminoAcidGain < muscleGain.Value)
					muscleGain = aminoAcidGain;
			}
			return muscleGain.Value;
		}
	}
}
