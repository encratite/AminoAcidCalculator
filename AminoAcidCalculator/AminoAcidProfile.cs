namespace AminoAcidCalculator
{
	class AminoAcidProfile
	{
		public string Source { get; private set; }

		public int Threonine { get; private set; }
		public int Methionine { get; private set; }
		public int Phenylalanine { get; private set; }
		public int Histidine { get; private set; }
		public int Lysine { get; private set; }
		public int Valine { get; private set; }
		public int Isoleucine { get; private set; }
		public int Leucine { get; private set; }

		public AminoAcidProfile
		(
			string source,
			int threonine,
			int methionine,
			int phenylalanine,
			int histidine,
			int lysine,
			int valine,
			int isoleucine,
			int leucine
		)
		{
			Source = source;
			Threonine = threonine;
			Methionine = methionine;
			Phenylalanine = phenylalanine;
			Histidine = histidine;
			Lysine = lysine;
			Valine = valine;
			Isoleucine = isoleucine;
			Leucine = leucine;
		}

		public int[] GetAminoAcidVector()
		{
			return new int[]
			{
				Threonine,
				Methionine,
				Phenylalanine,
				Histidine,
				Lysine,
				Valine,
				Isoleucine,
				Leucine
			};
		}
	}
}
