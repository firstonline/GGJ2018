using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RocketsManager : MonoBehaviour
{
	[SerializeField] private List<RocketData> m_rocketsData;
	[SerializeField] private Rocket m_rocketPrefab;
	[SerializeField] private GameObject target;
	[SerializeField] private RocketButton[] m_spawnButtons;

	private ObjectPooler m_pooler;
	private PopulationController m_popController;

	public static RocketsManager Instance
	{
		get
		{
			if (ms_instance == null)
			{
				ms_instance = FindObjectOfType<RocketsManager>();
			}
			return ms_instance;
		}
	}

	private static RocketsManager ms_instance;

	private void Awake()
	{
		m_pooler = new ObjectPooler(new GameObject[] { m_rocketPrefab.gameObject });
		if (ms_instance == null)
		{
			ms_instance = this;
		}
	}

	private void Start()
	{
		// expect to have 3 of them
		var rocketData = GetRocketData(RocketType.Small);
		m_spawnButtons[0].Initialise(rocketData);
		rocketData = GetRocketData(RocketType.Normal);
		m_spawnButtons[1].Initialise(rocketData);
		rocketData = GetRocketData(RocketType.Big);
		m_spawnButtons[2].Initialise(rocketData);

		m_popController = GameMode.Instance.GetPopController();
	}

	public void CreateRocket(RocketType rocketType)
	{
		var rocketData = GetRocketData(rocketType);

		if (rocketData.StorageAmount > rocketData.CreatedRockets)
		{
			int humanResourceCount = m_popController.GetCurrentPopulation();

			if (humanResourceCount >= rocketData.HumansCost)
			{
				m_popController.ReducePopulation(rocketData.HumansCost);
				rocketData.CreatedRockets++;
				UpdateRocketButton(rocketData, true);
			}
			else
			{
				Debug.Log("Not enough human resources " + rocketType.ToString());
			}
		}
		else
		{
			Debug.Log("Rocket Storage Is Full for " + rocketType.ToString());
		}
	}

	public void LaunchRocket(RocketType rocketType)
	{
		var rocketData = GetRocketData(rocketType);
		if (target != null && rocketData.CreatedRockets > 0)
		{
			rocketData.CreatedRockets--;
			UpdateRocketButton(rocketData, false);
			var rocket = m_pooler.GetNewObject().GetComponent<Rocket>();
			rocket.Initialise(target, rocketData.Damage, rocketData.RocketSprite, Vector3.zero, Vector3.zero);
		}
	}

	public RocketData GetRocketData(RocketType rocketType)
	{
		return m_rocketsData.FirstOrDefault(x => x.RocketType == rocketType);
	}

	public void IncreaseRocketStorage(RocketType rocketType, int amount)
	{
		GetRocketData(rocketType).StorageAmount += amount;
	}

	public void UnlockRocket(RocketType rocketType)
	{
		var rocketData = GetRocketData(rocketType);
		rocketData.Unlocked = true;
		int buttonIndex = (int)rocketData.RocketType;
		m_spawnButtons[buttonIndex].UnlockButton();
	}

	private void UpdateRocketButton(RocketData rocketData, bool increase)
	{
		int buttonIndex = (int)rocketData.RocketType;
		m_spawnButtons[buttonIndex].SetStorageText(rocketData.CreatedRockets, rocketData.StorageAmount);
		if (increase)
		{
			if (rocketData.CreatedRockets == 1)
			{
				m_spawnButtons[buttonIndex].ShowLaunchButton();
			}
		}
		else
		{
			if (rocketData.CreatedRockets == 0)
			{
				m_spawnButtons[buttonIndex].HideLaunchButton();
			}
		}
	}
}
