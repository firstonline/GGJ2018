using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public abstract class IUpgrade : ScriptableObject
{
	public enum UpgradeUnlockType
	{
		UnlockBaseUpgrade,
		PopulationAmount,
	}

	public Image UpgradeImage;
	public UpgradeUnlockType UnlockType;	// When will the player see it for the first time
	public string Name;
	public string Description;
	public int Order;					
	public int RequiredHumansToSee;
	public int BaseCost;
	public float HumanResearchTime;

	public abstract UpgradeType UpgradeType { get; }
	public abstract void ApplyUpgrade();
	public abstract void RemoveUpgrade();
}
