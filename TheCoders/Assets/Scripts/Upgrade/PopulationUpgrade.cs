using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulationUpgrade : IUpgrade
{
	public override UpgradeType UpgradeType { get { return UpgradeType.Population; } }
	public float FlatGrowthIncrease;
	public float PercentageGrowthIncrease;

	public override void ApplyUpgrade()
	{
		var popController = GameMode.Instance.GetPopController();

		if (FlatGrowthIncrease > 0)
		{
			popController.Growth += FlatGrowthIncrease;
		}
		else if (PercentageGrowthIncrease > 0)
		{
			popController.Growth *= (1.0f + PercentageGrowthIncrease);
		}
	}

	public override void RemoveUpgrade()
	{
		var popController = GameMode.Instance.GetPopController();

		if (FlatGrowthIncrease > 0)
		{
			popController.Growth -= FlatGrowthIncrease;
		}
		else if (PercentageGrowthIncrease > 0)
		{
			popController.Growth /= (1.0f + PercentageGrowthIncrease);
		}
	}
}
