using UnityEngine;
using System.Collections.Generic;

public class SkillTree : MonoBehaviour
{
	[SerializeField] private List<PopulationUpgrade> m_populationUpgrades;
	[SerializeField] private List<TechUpgrade> m_techUpgrades;
	[SerializeField] private List<RocketUpgrade> m_rocketUpgrades;

	// int is upgrade times for cost calculation
	private Dictionary<PopulationUpgrade, int> m_populationUpgradeRanking;
	private Dictionary<TechUpgrade, int> m_techUpgradesRanking;
	private Dictionary<RocketUpgrade, int> m_rocketUpgradesRanking;

	private void Awake()
	{
		m_populationUpgradeRanking = new Dictionary<PopulationUpgrade, int>();
		m_techUpgradesRanking = new Dictionary<TechUpgrade, int>();
		m_rocketUpgradesRanking = new Dictionary<RocketUpgrade, int>();

		for (int i = 0; i < m_populationUpgrades.Count; i++)
		{
			m_populationUpgradeRanking.Add(m_populationUpgrades[i], 0);
		}

		for (int i = 0; i < m_techUpgradesRanking.Count; i++)
		{
			m_techUpgradesRanking.Add(m_techUpgradesRanking[i], 0);
		}

		for (int i = 0; i < m_rocketUpgrades.Count; i++)
		{
			m_rocketUpgradesRanking.Add(m_rocketUpgrades[i], 0);
		}
	}
}
