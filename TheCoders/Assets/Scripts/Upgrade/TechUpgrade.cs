using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TechUpgrade : IUpgrade
{
	public override UpgradeType UpgradeType { get { return UpgradeType.Tech; } }

	public override void ApplyUpgrade()
	{
		throw new System.NotImplementedException();
	}

	public override void RemoveUpgrade()
	{
		throw new System.NotImplementedException();
	}
}
