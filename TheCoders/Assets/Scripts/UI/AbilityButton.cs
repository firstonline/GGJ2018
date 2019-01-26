using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityButton : MonoBehaviour
{
	//Text/Image displayed on button
	public string Title;
	public string Description;
	public Sprite Icon;
	private bool IsLocked = true;

	public enum ValueType
	{
		Population,
		PopulationLimit,
		GrowthRate
	}

	public enum OperationType
	{
		Addition,
		Subtraction,
		Multiplication,
		Division
	}

	[System.Serializable]
	public class ModifierOption
	{
		public ValueType ValueType;
		public OperationType OpType;
		public float Value;

		//Apply the current modifier
		public void ApplyModifier()
		{
			switch (ValueType)
			{
				case ValueType.Population:
					int Population = GameMode.Instance.GetPopController().GetCurrentPopulation();
					switch (OpType)
					{
						case OperationType.Addition:
							GameMode.Instance.GetPopController().AddPopulation((int)Value);
							break;
						case OperationType.Subtraction:
							GameMode.Instance.GetPopController().ReducePopulation((int)Value);
							break;
						case OperationType.Multiplication:
							GameMode.Instance.GetPopController().SetCurrentPopulation( (int)((float)Population * Value) );
							break;
						case OperationType.Division:
							GameMode.Instance.GetPopController().SetCurrentPopulation( (int)((float)Population * Value) );
							break;
					}
					break;
				case ValueType.PopulationLimit:
					int PopulationLimit = GameMode.Instance.GetPopController().GetPopulationLimit();
					switch (OpType)
					{
						case OperationType.Addition:
							GameMode.Instance.GetPopController().SetPopulationLimit( PopulationLimit + (int)Value );
							break;
						case OperationType.Subtraction:
							GameMode.Instance.GetPopController().SetPopulationLimit(PopulationLimit - (int)Value);
							break;
						case OperationType.Multiplication:
							GameMode.Instance.GetPopController().SetPopulationLimit(PopulationLimit * (int)Value);
							break;
						case OperationType.Division:
							GameMode.Instance.GetPopController().SetPopulationLimit(PopulationLimit / (int)Value);
							break;
					}
					break;
				case ValueType.GrowthRate:
					float GrowthRate = GameMode.Instance.GetPopController().GetGrowthRate();
					switch (OpType)
					{
						case OperationType.Addition:
							GameMode.Instance.GetPopController().SetGrowthRate( GrowthRate + Value );
							break;
						case OperationType.Subtraction:
							GameMode.Instance.GetPopController().SetGrowthRate(GrowthRate - Value);
							break;
						case OperationType.Multiplication:
							GameMode.Instance.GetPopController().SetGrowthRate(GrowthRate * Value);
							break;
						case OperationType.Division:
							GameMode.Instance.GetPopController().SetGrowthRate(GrowthRate / Value);
							break;
					}
					break;
			}
		}

		//See if the current user values can satisfy this cost option
		public static bool EvaluateCostOption(ModifierOption TestCondition)
		{
			switch (TestCondition.ValueType)
			{
				case ValueType.Population:
					switch (TestCondition.OpType)
					{
						case OperationType.Subtraction:
							return (GameMode.Instance.GetPopController().GetCurrentPopulation() - TestCondition.Value > 0);
						default:
							return true;
					}
				case ValueType.PopulationLimit:
					switch (TestCondition.OpType)
					{
						case OperationType.Subtraction:
							return (GameMode.Instance.GetPopController().GetPopulationLimit() - TestCondition.Value > 0);
						default:
							return true;
					}
				case ValueType.GrowthRate:
					switch (TestCondition.OpType)
					{
						case OperationType.Subtraction:
							return (GameMode.Instance.GetPopController().GetGrowthRate() - TestCondition.Value > 0);
						default:
							return true;
					}
				default:
					return true;
			}
		}
	}

	[Tooltip("To unlock this ability, apply the following costs")]
	public ModifierOption[] AbilityCosts;

	private void Awake()
	{
		gameObject.GetComponent<Image>().sprite = Icon;
		gameObject.GetComponentInChildren<Text>().text = Title;
	}

	public bool Unlock()
	{
		if (!IsLocked)
		{
			return true;
		}

		bool CanUnlockWithResources = true;
		foreach ( ModifierOption Cost in AbilityCosts )
		{
			CanUnlockWithResources &= ModifierOption.EvaluateCostOption(Cost);
		}

		if (CanUnlockWithResources)
		{
			foreach (ModifierOption Cost in AbilityCosts)
			{
				Cost.ApplyModifier();
			}
			return true;
		}
		else
		{
			return false;
		}
	}

	public bool IsUnlocked()
	{
		return IsLocked;
	}
}
