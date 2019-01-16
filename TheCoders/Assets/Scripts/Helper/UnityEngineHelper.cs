using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UnityEngineHelper
{
    public static void TransformPosition(GameObject go, float x, float y, float z)
	{
		TransformPosition(go.transform, x, y, z);
	}

	public static void TransformPosition(Transform tr, float x, float y, float z)
	{
		var newPosition = tr.position;
		newPosition.x += x;
		newPosition.y += y;
		newPosition.z += z;
		tr.position = newPosition;
	}

	public static void TansformLocalScale(GameObject go, float x, float y, float z)
	{
		TansformLocalScale(go.transform, x, y, z);
	}

	public static void TansformLocalScale(Transform tr, float x, float y, float z)
	{
		var newLocalScale = tr.localScale;
		newLocalScale.x += x;
		newLocalScale.y += y;
		newLocalScale.z += z;
		tr.localScale = newLocalScale;
	}

	public static void TransformAngle(GameObject go, float x, float y, float z)
	{
		TransformAngle(go.transform, x, y, z);
	}

	public static void TransformAngle(Transform tr, float x, float y, float z)
	{
		var newAngle = tr.localEulerAngles;
		newAngle.x += x;
		newAngle.y += y;
		newAngle.z += z;
		tr.localEulerAngles = newAngle;
	}
}
