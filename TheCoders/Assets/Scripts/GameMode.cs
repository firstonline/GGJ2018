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

	[HideInInspector]
	public Arena2D Arena;
	private CometSpawner CometSpawner;
	public GameObject Planet;

	[Header("Comet Spawns")]
	// Spawn a comet every X seconds
	[SerializeField]
	private float CometSpawnDelay = 2.0f;
	[SerializeField]
	private float CometSpeed = 0.2f;

	private float TimeElapsedSinceLastSpawn = 0.0f;

	// Awake is called upon construction
	void Awake()
	{
		instance = this;
		Arena = GetComponent<Arena2D>();
		PopController = GetComponent<PopulationController>();
		CometSpawner = GetComponent<CometSpawner>();
	}

	// Start is called before the first frame update
	void Start()
    {
		Debug.Log("Start!");
	}

    // Main game loop logic here
    void Update()
    {
		TimeElapsedSinceLastSpawn += Time.deltaTime;
		if (TimeElapsedSinceLastSpawn >= CometSpawnDelay)
		{
			TimeElapsedSinceLastSpawn = 0.0f;
			CometSpawner.SpawnComet( CometSpeed, 10, 10 );
		}
    }
}
