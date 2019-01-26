using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RocketButton : MonoBehaviour
{
	[SerializeField] private Button m_launchButton;
	[SerializeField] private Button m_createButton;
	[SerializeField] private TextMeshProUGUI m_humanCost;
	[SerializeField] private TextMeshProUGUI m_storage;
	[SerializeField] private Image m_progressBar;

	private RocketType m_rocketType;

	public void Initialise(RocketData rocketData)
	{
		m_rocketType = rocketData.RocketType;
		m_launchButton.onClick.AddListener(LaunchRocket);
		m_createButton.onClick.AddListener(CreateRocket);
		m_humanCost.text = rocketData.HumansCost.ToString();
		m_progressBar.fillAmount = 0.0f;
		SetStorageText(rocketData.CreatedRockets, rocketData.StorageAmount);
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

	public void UpdateProgressBar(float progress)
	{
		m_progressBar.fillAmount = progress;
	}
}
