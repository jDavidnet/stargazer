using UnityEngine;
using System.Collections;

public class FingerLineRenderers : MonoBehaviour {

	private static LineRenderer[] lineRenderers;
	public static LineRenderer Left
	{
		get
		{
			return lineRenderers[0];
		}
	}
	public static LineRenderer Right
	{
		get
		{
			return lineRenderers[1];
		}
	}

	// Use this for initialization
	void Start () {
		lineRenderers = new LineRenderer[2];
		lineRenderers [0] = GameObject.Find ("FingerLineRendererLeft").GetComponent<LineRenderer>();
		lineRenderers [1] = GameObject.Find ("FingerLineRendererRight").GetComponent<LineRenderer>();
		print ("FingerLineRenderers.Start(): " + lineRenderers.Length);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
