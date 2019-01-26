using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
	Handles the events and scopes of different objects in the main game loop.
	Singleton object - needs to persist between levels/waves
*/
public class GameMode : MonoBehaviour
{
	public static GameMode Instance
	{
		get
		{
			if (ms_instance == null)
			{
				ms_instance = FindObjectOfType<GameMode>();
			}
			return ms_instance;
		}
	}

	private static GameMode ms_instance;

	private PopulationController PopController;

	// Awake is called upon construction
	void Awake()
	{
		if (ms_instance == null)
		{
			ms_instance = this;
		}
		PopController = GetComponent<PopulationController>();
	}

	// Start is called before the first frame update
	void Start()
    {
	}

    // Main game loop logic here
    void Update()
    {
        
    }

	public PopulationController GetPopController()
	{
		return PopController;
	}
}
