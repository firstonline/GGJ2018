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

	private void Awake()
	{
		m_pooler = new ObjectPooler(new GameObject[] { m_rocketPrefab.gameObject });
	}

	private void Start()
	{
		// expect to have 3 of them
		m_spawnButtons[0].Initialise(()=> { SpawnRocket(RocketType.Small); }, m_rocketsData[0].HumansCost);
		m_spawnButtons[1].Initialise(() => { SpawnRocket(RocketType.Normal); }, m_rocketsData[1].HumansCost);
		m_spawnButtons[2].Initialise(() => { SpawnRocket(RocketType.Big); }, m_rocketsData[0].HumansCost);
	}

	public void SpawnRocket(RocketType rocketType)
	{
		var rocketData = GetRocketData( rocketType);

		int humanResourceCount = 100;
		if (humanResourceCount > rocketData.HumansCost)
		{
			var rocket = m_pooler.GetNewObject().GetComponent<Rocket>();
			rocket.Initialise(target, rocketData.Damage, rocketData.RocketSprite, Vector3.zero, Vector3.zero);
		}
		else
		{
			Debug.Log("Not enough human resources");
		}
	}

	public RocketData GetRocketData(RocketType rocketType)
	{
		return m_rocketsData.FirstOrDefault(x => x.RocketType == rocketType);
	}
}
