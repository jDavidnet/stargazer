using UnityEngine;
using System.Collections;

public class HelperText : MonoBehaviour 
{
	public Transform cameraTransform;
	public GameObject prefab;

	public float ShowText(string text)
	{
		Vector3 spawnPosition = cameraTransform.transform.position + cameraTransform.TransformDirection(Vector3.forward * 15f);
		GameObject go = Instantiate(prefab, spawnPosition, cameraTransform.rotation) as GameObject;
		TextMesh tm = go.GetComponent<TextMesh>();
		tm.text = text;
		
		Animator animator = go.GetComponent<Animator>();
		animator.Update(0f);
		AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
		Destroy(go, state.length);

		return state.length;
	}
}
