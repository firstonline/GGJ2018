using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RocketButton : MonoBehaviour
{
	[SerializeField] private GameObject m_lockedPanel;
	[SerializeField] private GameObject m_unlockedPanel;

	[SerializeField] private Button m_launchButton;
	[SerializeField] private Button m_createButton;
	[SerializeField] private TextMeshProUGUI m_humanCost;
	[SerializeField] private TextMeshProUGUI m_storage;
	[SerializeField] private Animator m_animator;

	private RocketType m_rocketType;

	public void Initialise(RocketData rocketData)
	{
		m_rocketType = rocketData.RocketType;
		m_launchButton.onClick.AddListener(LaunchRocket);
		m_createButton.onClick.AddListener(CreateRocket);
		m_humanCost.text = rocketData.HumansCost.ToString();
		SetStorageText(rocketData.CreatedRockets, rocketData.StorageAmount);
		m_lockedPanel.SetActive(!rocketData.Unlocked);
		m_unlockedPanel.SetActive(rocketData.Unlocked);
	}

	private void LaunchRocket()
	{
		RocketsManager.Instance.LaunchRocket(m_rocketType);
	}

	private void CreateRocket()
	{
		RocketsManager.Instance.CreateRocket(m_rocketType);
	}

	public void SetStorageText(int currentAmount, int maxAmount)
	{
		m_storage.text = currentAmount + "/" + maxAmount;
	}

	public void SetCostText(int cost)
	{
		m_humanCost.text = cost.ToString();
	}

	public void HideLaunchButton()
	{
		m_animator.SetTrigger("LaunchHide");
	}

	public void ShowLaunchButton()
	{
		m_animator.SetTrigger("LaunchAppear");
	}

	public void UnlockButton()
	{
		m_lockedPanel.SetActive(false);
		m_unlockedPanel.SetActive(true);
	}
}
