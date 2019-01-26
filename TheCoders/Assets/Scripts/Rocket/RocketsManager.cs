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

	private void Start()
	{
		// expect to have 3 of them
		m_spawnButtons[0].Initialise(()=> { SpawnRocket(RocketType.Small); });
		m_spawnButtons[1].Initialise(() => { SpawnRocket(RocketType.Normal); });
		m_spawnButtons[2].Initialise(() => { SpawnRocket(RocketType.Big); });
	}

	public void SpawnRocket(RocketType rocketType)
	{
		var rocketData = GetRocketData( rocketType);

		int humanResourceCount = 100;
		if (humanResourceCount > rocketData.HumansCost)
		{
			var rocket = Instantiate(m_rocketPrefab);
			rocket.Initialise(target, rocketData.Damage, rocketData.RocketSprite);
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
