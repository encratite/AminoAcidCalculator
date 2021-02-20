using System.Collections.Generic;

namespace AminoAcidCalculator
{
	class OptimalSource
	{
		public IEnumerable<AminoAcidProfile> Profile { get; set; } = null;
		public decimal? MuscleGain { get; set; } = null;
	}
}
