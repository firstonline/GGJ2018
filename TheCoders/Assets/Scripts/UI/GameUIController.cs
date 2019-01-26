using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{
	[SerializeField] private GameObject m_inGameMenu;
	[SerializeField] private TextMeshProUGUI m_populationText;
	[SerializeField] private string m_humanCostPrefix = "Human Population:";
	[SerializeField] private Image m_fillBar;
	[SerializeField] private GameObject m_endGamePopup;

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
	}

	public void UpdatePopulationText(int currentPopulation, int maxPopulation)
	{
		m_populationText.text = currentPopulation + "/" + maxPopulation;
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
}