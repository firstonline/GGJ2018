using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{
	[SerializeField] private GameObject m_inGameMenu;
	[SerializeField] private TextMeshProUGUI m_currentPopulationText;
	[SerializeField] private TextMeshProUGUI m_maxPopulationText;
	[SerializeField] private string m_humanCostPrefix = "Human Population:";
	[SerializeField] private Image m_fillBar;
	[SerializeField] private GameObject m_endGamePopup;
	[SerializeField] private GameObject m_dimmer;
	[SerializeField] private List<Animator> m_panelAnimators;
	[SerializeField] private TextMeshProUGUI m_statsText;
	[SerializeField] private UpgradePanelScript m_upgradePanel;

	private float m_cooldown;
	private bool m_panelsAreOpen;

	public static GameUIController Instance
	{
		get
		{
			if (ms_instance == null)
			{
				ms_instance = FindObjectOfType<GameUIController>();
			}
			return ms_instance;
		}
	}

	private static GameUIController ms_instance;

	private void Awake()
	{
		if (ms_instance == null)
		{
			ms_instance = this;
		}
	}

	private void Start()
	{
		m_inGameMenu.SetActive(false);
	}

	private void Update()
	{
		if (Input.GetKeyUp(KeyCode.Escape))
		{
			TriggerMenu();
		}

		if (m_cooldown <= 0.0f)
		{
			if (Input.GetKeyUp(KeyCode.Space))
			{
				PanelButtonClicked();
			}
		}
		else
		{
			m_cooldown -= Time.deltaTime;
		}
	}

	public void UpdatePopulationText(int currentPopulation, int maxPopulation)
	{
		m_currentPopulationText.text = currentPopulation.ToString();
		m_maxPopulationText.text = maxPopulation.ToString();
		m_fillBar.fillAmount = (float)currentPopulation / (float)maxPopulation;
	}

	public void ExitGame()
	{
		Application.Quit();
	}

	public void Restart()
	{
		SceneManager.LoadScene("Game");
	}

	public void ShowEndGamePopup()
	{
		m_endGamePopup.SetActive(true);
	}

	public void PanelButtonClicked()
	{
		m_cooldown = 0.4f;
		if (m_panelsAreOpen)
		{
			StartCoroutine(SlideOutPanels());
		}
		else
		{
			StartCoroutine(SlideInPanels());
		}
	}

	public void UpdateStatsScreen()
	{
		if (m_statsText != null)
		{
			var playerPop = GameMode.Instance.GetPopController();
			var rocketManager = RocketsManager.Instance;
			var manualRocketData = rocketManager.GetRocketData(RocketType.Small);

			m_statsText.text =
			"STATS" + "\n" + "\n" +
			"Passive Growth: " + playerPop.Growth + "\n" +
			"Click Growth: " + GameMode.Instance.Planet.GetComponent<Planet>().PopulationGainPerClick + "\n" +
			"Click Damage: " + GameMode.Instance.PlayerDamagePerClick + "\n" +
			"Rocket Damage: " + manualRocketData.Damage + "\n" +
			"Rocket Build Time: " + manualRocketData.TimeToConstruct + "\n";

			var autoRocketData = rocketManager.GetRocketData(RocketType.AutoAimWeak);

			if (autoRocketData.TimeToConstruct > 0.0f)
			{
				m_statsText.text +=
					"AutoRocket Damage: " + autoRocketData.Damage + "\n" +
					"Auto Rocket Build: " + autoRocketData.TimeToConstruct + "\n";
			}

		}
	}

	public void SetUpgPanel(AbilityButton node)
	{
		m_upgradePanel.SetUpgradeNode(node);
	}

	public void TriggerMenu()
	{
		bool menuActive = !m_inGameMenu.activeInHierarchy;
		m_inGameMenu.SetActive(menuActive);
		if (menuActive)
		{
			Time.timeScale = 0;
		}
		else
		{
			Time.timeScale = 1;
		}
	}

	private IEnumerator SlideInPanels()
	{
		UpdateStatsScreen();
		m_panelsAreOpen = true;
		for (int i = 0; i < m_panelAnimators.Count; i++)
		{
			m_panelAnimators[i].SetTrigger("SlideIn");
		}
		m_dimmer.SetActive(true);
		
		yield return new WaitForSeconds(0.4f);
		Time.timeScale = 0;
	}

	private IEnumerator SlideOutPanels()
	{
		Time.timeScale = 1;
		yield return new WaitForEndOfFrame();
		m_panelsAreOpen = false;
		m_dimmer.SetActive(false);
		for (int i = 0; i < m_panelAnimators.Count; i++)
		{
			m_panelAnimators[i].SetTrigger("SlideOut");
		}
	}
}
