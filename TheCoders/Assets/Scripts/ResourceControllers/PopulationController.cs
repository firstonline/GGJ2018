using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PopulationController : MonoBehaviour
{
	//Template for function type that is used in 
	public delegate float Modifier(float val);

	// Max population
	[SerializeField]
	public int PopulationMaximum { get; private set; }

	// Current population
	[SerializeField]
	public int PopulationCurrent { get; private set; }

	// Initial growth rate - per second
	[SerializeField]
	private float StartingGrowthRate = 1.1f;

	// Current grow rate (following modifier evaluation) - per second
	[SerializeField]
	public float CurrentGrowRate { get; private set; }

	// Have a list of modifiers that can be indexed by ID
	public Dictionary<uint, Modifier> Modifiers;

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
		// Set current rate to 1.0f and apply initial modifier
		CurrentGrowRate = 1.0f;
		Modifiers = new Dictionary<uint, Modifier>();

		//Add the base rate multiplier
		Modifier BaseRateCalc = delegate (float x)
		{
			return (x * StartingGrowthRate);
		};

		AddModifier( 0, BaseRateCalc );
    }

    // Update is called once per frame
    void Update()
    {
		//Debug.Log(CurrentGrowRate);
    }
}
