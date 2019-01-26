using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public abstract class IUpgrade : ScriptableObject
{
	public enum UpgradeUnlockType
	{
		PopulationAmount,
		HumanCost
	}

	public Image UpgradeImage;
	public UpgradeUnlockType UnlockType;
	public string Name;
	public string Description;
	public int Order;
	public int RequiredHumans;
	public int HumanFreeze;
	public int HumanCost;

	public abstract UpgradeType UpgradeType { get; }
	public abstract void ApplyUpgrade();
	public abstract void RemoveUpgrade();
}
