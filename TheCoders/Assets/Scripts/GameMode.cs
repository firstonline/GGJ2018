﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


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

	[HideInInspector]
	public Arena2D Arena;
	private CometSpawner CometSpawner;
	public GameObject Planet;

	[Header("Comet Spawn Variables")]
	// Spawn a comet every X seconds
	[SerializeField]
	private float CometSpawnDelay = 2.0f;
	[SerializeField]
	private float CometSpeed = 0.2f;
	[SerializeField]
	private int CometHealth = 10;
	[SerializeField]
	private int CometDamage = 10;

	private float TimeElapsedSinceLastSpawn = 0.0f;

	// Awake is called upon construction
	void Awake()
	{
		if (ms_instance == null)
		{
			ms_instance = this;
		}
		PopController = GetComponent<PopulationController>();
		Arena = GetComponent<Arena2D>();
		CometSpawner = GetComponent<CometSpawner>();
	}

	// Start is called before the first frame update
	void Start()
    {
	}

    // Main game loop logic here
    void Update()
    {
		TimeElapsedSinceLastSpawn += Time.deltaTime;
		if (TimeElapsedSinceLastSpawn >= CometSpawnDelay)
		{
			TimeElapsedSinceLastSpawn = 0.0f;
			CometSpawner.SpawnComet( CometSpeed, CometHealth, CometDamage );
		}
    }

	public PopulationController GetPopController()
	{
		return PopController;
	}

	public CometSpawner GetCometSpawner()
	{
		return CometSpawner;
	}

	public void GameOver()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
