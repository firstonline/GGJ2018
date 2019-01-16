using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIController : MonoBehaviour
{
	[SerializeField] private GameObject m_inGameMenu;

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

	public void ExitGame()
	{
		Application.Quit();
	}

	public void Restart()
	{
		SceneManager.LoadScene("Game");
	}

}
