using UnityEngine;
using System.Collections;

public class TextAlpha : MonoBehaviour 
{
	private Color color = Color.clear;

	void Awake() 
	{
		color = renderer.material.color;
		color.a = 0f;
		renderer.material.color = color;
	}
	
	private void OnMouseOver()
	{
		color.a = Mathf.Clamp01(color.a + 0.01f);
		renderer.material.color = color;
	}
}
