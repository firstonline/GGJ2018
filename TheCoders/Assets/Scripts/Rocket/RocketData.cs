using UnityEngine;

[System.Serializable]
public class RocketData
{
	public RocketType RocketType;
	public Sprite RocketSprite;
	public int HumansCost;
	public int Damage;
	public int StorageAmount;
	public int CreatedRockets;
	public bool Unlocked;
	public float TimeToConstruct;
}