using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketUpgrade : IUpgrade
{
	public override UpgradeType UpgradeType { get { return UpgradeType.Rocket; } }

	public override void ApplyUpgrade()
	{
		throw new System.NotImplementedException();
	}

	public override void RemoveUpgrade()
	{
		throw new System.NotImplementedException();
	}
}
