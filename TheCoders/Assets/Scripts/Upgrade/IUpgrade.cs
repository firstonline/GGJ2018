using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public abstract class IUpgrade : ScriptableObject
{
	public Image UpgradeImage;
	public abstract UpgradeType UpgradeType { get; }
	public abstract void ApplyUpgrade();
	public abstract void RemoveUpgrade();
}
