using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
	Handles the events and scopes of different objects in the main game loop.
	Singleton object - needs to persist between levels/waves
*/
public class GameMode : MonoBehaviour
{
	public static GameMode instance;
	private PopulationController PopController;

	// Awake is called upon construction
	void Awake()
	{
	
	}

	// Start is called before the first frame update
	void Start()
    {
		Debug.Log("Start!");
		PopController = GetComponent<PopulationController>();
	}

    // Main game loop logic here
    void Update()
    {
        
    }
}
