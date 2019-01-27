using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanelScript : MonoBehaviour
{
	public string TitleTextString;
	public string DescTextString;
	[SerializeField]
	private Text TitleText;
	[SerializeField]
	private Text DescText;
	[SerializeField]
	private Button PurchaseButton;
	[SerializeField]
	private GameObject[] UIPanelsToDisable;
	private AbilityButton UpgradeNode = null;

	//Period between checking unlock condition
	private float DelayToCheckCondition = 1.0f;
	private float CheckClock = 0.0f;

	void Update()
	{
		//Disable purchase button if unlock conditions not satisfied
		if (gameObject.activeSelf && UpgradeNode != null)
		{
			if (CheckClock >= DelayToCheckCondition)
			{
				PurchaseButton.enabled = UpgradeNode.UnlockFeasibleCheck();
				CheckClock = 0.0f;
			}
			else
			{
				CheckClock += Time.deltaTime;
			}
		}
	}

	//Set the upgrade button that revealed this popup (to pull data)
	public void SetUpgradeNode(AbilityButton Node)
	{
		UpgradeNode = Node;
		TitleTextString = "Buy " + Node.Title;
		if (Node.Levels.Length > 1)
		{
			TitleTextString = TitleTextString + " Lv. " + (Node.LevelCount + 1);
		}
		DescTextString = Node.Description;
		UpdateTexts();
		CheckClock = 0.0f;
		SetActive(true);
	}

	private void UpdateTexts()
	{
		TitleText.text = TitleTextString;
		DescText.text = DescTextString;
	}

	public void Purchase()
	{
		if (UpgradeNode != null)
		{
			UpgradeNode.Unlock();
		}
		else
		{
			Debug.Log("UpgradeNode was NULL!");
		}
		SetActive(false);
	}

	public void Cancel()
	{
		SetActive(false);
	}

	private void SetActive( bool Active )
	{
		foreach ( GameObject UIPanelParent in UIPanelsToDisable)
		{
			UIPanelParent.SetActive(!Active);
		}
		gameObject.SetActive(Active);
	}
}
