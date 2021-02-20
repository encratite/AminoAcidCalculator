namespace AminoAcidCalculator
{
	static class Profiles
	{
		public static AminoAcidProfile HumanMuscle = new AminoAcidProfile
		(
			"Human muscle",
			61,
			107,
			173,
			227,
			105,
			139,
			102,
			83
		);

		public static AminoAcidProfile[] Sources = new AminoAcidProfile[]
		{
			new AminoAcidProfile
			(
				"Oat",
				50,
				10,
				195,
				116,
				33,
				103,
				62,
				80
			),
			new AminoAcidProfile
			(
				"Lupin",
				50,
				19,
				120,
				143,
				49,
				67,
				66,
				62
			),
			new AminoAcidProfile
			(
				"Wheat",
				34,
				40,
				153,
				103,
				16,
				68,
				54,
				60
			),
			new AminoAcidProfile
			(
				"Hemp",
				49,
				113,
				147,
				160,
				40,
				76,
				54,
				62
			),
			new AminoAcidProfile
			(
				"Soy",
				57,
				22,
				171,
				143,
				64,
				84,
				67,
				78
			),
			new AminoAcidProfile
			(
				"Brown rice",
				50,
				131,
				174,
				126,
				31,
				94,
				62,
				79
			),
			new AminoAcidProfile
			(
				"Pea",
				55,
				20,
				177,
				136,
				79,
				92,
				72,
				79
			)
		};
	}
}
