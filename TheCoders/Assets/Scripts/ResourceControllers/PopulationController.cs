using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PopulationController : MonoBehaviour
{
	//Template for function type that is used in 
	public delegate float Modifier(float val);

	public float Growth
	{
		get { return CurrentGrowRate; }
		set { CurrentGrowRate = value; }
	}

	// Starting Population
	[SerializeField]
	private int StartingPopulation;

	// Max population
	[SerializeField]
	private int PopulationMaximum;

	// Current population
	[SerializeField]
	private int PopulationCurrent;

	// Float version for small decimal additions
	private float PopulationCurrentF;

	// Initial growth rate - per second
	[SerializeField]
	private float StartingGrowthRate = 1.1f;

	// Current grow rate (following modifier evaluation) - per second
	[SerializeField]
	private float CurrentGrowRate;

	[SerializeField]
	private bool LogPopulationStatsToConsole = true;

	// Have a list of modifiers that can be indexed by ID
	public Dictionary<uint, Modifier> Modifiers;

	// Add value directly to current population
	public void AddPopulation(int Value)
	{
		PopulationCurrent += Value;
		PopulationCurrentF += Value;
	}

	// Deduct value directly to current population
	public void ReducePopulation(int amount)
	{
		PopulationCurrent -= amount;
		PopulationCurrentF -= amount;
		CheckGameOver();
	}

	// Add a modifier (then evaluate growth rate)
	public void AddModifier(uint key, Modifier expr)
	{
		Modifiers.Add(key, expr);
		EvaluateGrowthRate();
	}

	// Remove a modifier (then evaluate growth rate)
	public void RemoveModifier(uint key)
	{
		Modifiers.Remove(key);
		EvaluateGrowthRate();
	}

	public int GetCurrentPopulation()
	{
		return PopulationCurrent;
	}

	public void SetCurrentPopulation(int Value)
	{
		PopulationCurrentF = Value;
		PopulationCurrent = Value;
	}

	public int GetPopulationLimit()
	{
		return PopulationMaximum;
	}

	public void SetPopulationLimit(int Value)
	{
		PopulationMaximum = Value;
	}

	public float GetGrowthRate()
	{
		return CurrentGrowRate;
	}

	public void SetGrowthRate(float Value)
	{
		CurrentGrowRate = Value;
	}

	// Determine the current growth rate by totalling modifiers (not efficient)
	private void EvaluateGrowthRate()
	{
		float newGrowthRate = 1.0f;

		// Apply each modifier in the dictionary
		foreach ( KeyValuePair<uint, Modifier> kvp in Modifiers )
		{
			newGrowthRate = kvp.Value(newGrowthRate);
		}
		CurrentGrowRate = newGrowthRate;
	}

    // Start is called before the first frame update
    void Start()
    {
		PopulationCurrent = StartingPopulation;
		PopulationCurrentF = StartingPopulation;

		// Set current rate to 1.0f and apply initial modifier
		CurrentGrowRate = 1.0f;
		Modifiers = new Dictionary<uint, Modifier>();

		//Add the base rate multiplier
		Modifier BaseRateCalc = delegate (float x)
		{
			return (x * StartingGrowthRate);
		};

		AddModifier( 0, BaseRateCalc );
		GameUIController.Instance.UpdatePopulationText(PopulationCurrent, PopulationMaximum);
	}

    // Update is called once per frame
    void Update()
    {
		PopulationCurrentF += (Time.deltaTime * CurrentGrowRate);
		PopulationCurrentF = Mathf.Clamp(PopulationCurrentF, 0, (float)PopulationMaximum);
		PopulationCurrent = (int)(PopulationCurrentF);
		GameUIController.Instance.UpdatePopulationText(PopulationCurrent, PopulationMaximum);
	}

	void CheckGameOver()
	{
		if ( PopulationCurrent <= 0 )
		{
			GameMode.Instance.GameOver();
		}
	}
}
