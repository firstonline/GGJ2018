using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Arena2D : MonoBehaviour
{
    public static float Width { get; private set; }
    public static float Height { get; private set; }

	//bottom left corner
	public Vector2 MinBounds;
	//top right corner
	public Vector2 MaxBounds;

	void Awake()
	{
		Width = Mathf.Abs(MinBounds.x - MaxBounds.x);
		Height = Mathf.Abs(MinBounds.y - MaxBounds.y);
	}


	public Vector2 RandomArenaBorderPosition() 
	{
		/*
			Can spawn anywhere on the 4 vectors:
				min width to max width (min height)
				min width to max width (max height)
				min height to max height (min width)
				min height to max height (max width)
		*/

		Vector2 bottomLeft = MinBounds;
		Vector2 bottomRight = new Vector2(MaxBounds.x, MinBounds.y);
		Vector2 topLeft = new Vector2(MinBounds.x, MaxBounds.y);
		Vector2 topRight = MaxBounds;

		//A random number to select which vector
		int selection = Random.Range(0, 4);

		//A random number to select a position along the selected vector.
		Vector2 result = new Vector2();
		float randomPositionNumber = Random.Range(0.0f, 1.0f);

		switch(selection) 
		{
			case 0:
				//Top Horizontal
				result = Vector2.Lerp(topLeft, topRight, randomPositionNumber);
				break;
			case 1:
				//Bottom horizontal
				result = Vector2.Lerp(bottomLeft, bottomRight, randomPositionNumber);
				break;
			case 2:
				//Left vertical
				result = Vector2.Lerp(bottomLeft, topLeft, randomPositionNumber);
				break;
			case 3:
				//Right vertical
				result = Vector2.Lerp(bottomRight, topRight, randomPositionNumber);
				break;
		}

		return result;
	}
}
