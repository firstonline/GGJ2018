using UnityEngine;
using System.Collections;

public abstract class IUpgrade : ScriptableObject
{
	public abstract UpgradeType UpgradeType { get; }
	public abstract void ApplyUpgrade();
	public abstract void RemoveUpgrade();
}
