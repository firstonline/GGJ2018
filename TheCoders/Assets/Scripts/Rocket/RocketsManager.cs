using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RocketsManager : MonoBehaviour
{
	[SerializeField] private List<RocketData> m_rocketsData;
	[SerializeField] private Rocket m_rocketPrefab;
	[SerializeField] private RocketButton m_spawnButton;

	private GameObject target;
	private ObjectPooler m_pooler;
	private PopulationController m_popController;

	private bool m_constructing;
	private RocketData m_rocketInConstruction;
	private float m_constructionTime;
	
	private RocketData m_autoRocketData;
	private bool m_autoSpawnEnabled;
	private float m_autoConstructionTimeLeft;

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
		m_spawnButton.Initialise(rocketData);
		m_popController = GameMode.Instance.GetPopController();
		EnableAutoSpawnRockets();
	}

	private void Update()
	{
		if (m_constructing)
		{
			if (m_constructionTime > 0.0f)
			{
				m_constructionTime -= Time.deltaTime;
				m_spawnButton.UpdateProgressBar(1.0f - m_constructionTime / m_rocketInConstruction.TimeToConstruct);
			}
			else
			{
				m_constructing = false;
				m_rocketInConstruction.CreatedRockets++;
				m_spawnButton.SetStorageText(m_rocketInConstruction.CreatedRockets, m_rocketInConstruction.StorageAmount);
				m_rocketInConstruction = null;
				m_spawnButton.UpdateProgressBar(0.0f);
			}
		}

		if (m_autoSpawnEnabled)
		{
			if (m_autoConstructionTimeLeft > 0.0f)
			{
				m_autoConstructionTimeLeft -= Time.deltaTime;
				// m_spawnButton.UpdateProgressBar(1.0f - m_constructionTime / m_rocketInConstruction.TimeToConstruct);
			}
			else
			{
				m_autoConstructionTimeLeft = m_autoRocketData.TimeToConstruct;
				SelectBestTarget();
				if (target != null)
				{
					var rocket = m_pooler.GetNewObject().GetComponent<Rocket>();
					rocket.Initialise(target, m_autoRocketData.Damage, m_autoRocketData.RocketSprite, Vector3.zero);
				}
			}
		}
	}

	public void CreateRocket(RocketType rocketType)
	{
		var rocketData = GetRocketData(rocketType);

		if (!m_constructing && rocketData.StorageAmount > rocketData.CreatedRockets)
		{
			int humanResourceCount = m_popController.GetCurrentPopulation();

			if (humanResourceCount >= rocketData.HumansCost)
			{
				m_popController.ReducePopulation(rocketData.HumansCost);
				m_rocketInConstruction = rocketData;
				m_constructionTime = rocketData.TimeToConstruct;
				m_constructing = true;
			}
			else
			{
				Debug.Log("Not enough human resources " + rocketType.ToString());
			}
		}
		else
		{
			Debug.Log("Cannot construct " + rocketType.ToString());
		}
	}

	public void LaunchRocket(RocketType rocketType)
	{
		var rocketData = GetRocketData(rocketType);
		SelectBestTarget();
		if (target != null && rocketData.CreatedRockets > 0)
		{
			rocketData.CreatedRockets--;
			m_spawnButton.SetStorageText(rocketData.CreatedRockets, rocketData.StorageAmount);
			var rocket = m_pooler.GetNewObject().GetComponent<Rocket>();
			rocket.Initialise(target, rocketData.Damage, rocketData.RocketSprite, Vector3.zero);
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
	}

	public void EnableAutoSpawnRockets()
	{
		m_autoSpawnEnabled = true;
		m_autoRocketData = GetRocketData(RocketType.AutoAimWeak);
		m_autoConstructionTimeLeft = m_autoRocketData.TimeToConstruct;
	}

	public void DisableAutoSpawn()
	{
		m_autoSpawnEnabled = false;
	}

	public int GetActiveRocketsCount()
	{
		return m_pooler.CountActiveElements();
	}


	private void SelectBestTarget()
	{
		List<GameObject> Comets = GameMode.Instance.GetCometSpawner().GetActiveComets();
		GameObject PriorityTarget = null;
		float minDistance = 0.0f;

		foreach (GameObject Target in Comets )
		{
			float TargetDistance = Mathf.Abs(Vector3.Distance(GameMode.Instance.Planet.transform.position, Target.transform.position));
			if ( PriorityTarget == null || TargetDistance < minDistance )
			{
				PriorityTarget = Target;
				minDistance = TargetDistance;
			}
		}

		target = PriorityTarget;
	}

}
