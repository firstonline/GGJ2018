using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControler : MonoBehaviour
{
	public void ExitGame()
	{
		Application.Quit();
	}

	public void LoadScene()
	{
		SceneManager.LoadScene("Game");
	}
}
