using UnityEngine;
using System.Collections;
using Leap;

public class CustomRigidHand : RigidHand 
{
	Controller controller;
	private LineRenderer lineRenderer;

	public override void InitHand ()
	{
		base.InitHand();

		controller = new Controller ();
		controller.EnableGesture (Gesture.GestureType.TYPE_SWIPE);
	}

	public override void UpdateHand() 
	{
		base.UpdateHand();

		GameObject go = GameObject.FindGameObjectWithTag("CameraCenter");

		RaycastHit hit;
		for (int f = 0; f < fingers.Length; ++f) 
		{
			Vector3 direction = (fingers[f].GetTipPosition() - go.transform.position).normalized;
//			Debug.DrawRay(go.transform.position, direction * 1000f);
			if (Physics.Raycast(go.transform.position, direction, out hit))
			{
				hit.collider.SendMessage("OnMouseOver", SendMessageOptions.DontRequireReceiver);
			}
		}

//		Debug.DrawRay(go.transform.position, GetPalmNormal() * 1000f);
		if (Physics.Raycast(go.transform.position, GetPalmNormal(), out hit))
		{
			hit.collider.SendMessage("OnMouseOver", SendMessageOptions.DontRequireReceiver);
		}

//		Frame frame = controller.Frame();
//		foreach (Gesture g in frame.Gestures())
//			if (g.Type == Gesture.GestureType.TYPE_SWIPE)
//				print ("Swipe");

		// which hand is this?
//      if (hand_.IsLeft) {
//        lineRenderer = FingerLineRenderers.Left;
//      } else if (hand_.IsRight) {
//        lineRenderer = FingerLineRenderers.Right;
//      } else {
//        return;
//      }
//      Vector3 start = GetPalmCenter();
//      Vector3 end = GetPalmNormal() * 100.0f;
//      if (lineRenderer != null) {
//        lineRenderer.SetPosition(0, start);
//        lineRenderer.SetPosition(1, end);
//      }
//      RaycastHit hit;
//      if (Physics.Raycast (start, end, out hit))
//        if (hit.collider)
//          if (hit.collider.gameObject.name.StartsWith("L_")) {
//            print ("Palm hit: " + hit.collider.gameObject.name);
//            hit.collider.SendMessage("OnMouseOver", SendMessageOptions.DontRequireReceiver);
//          }

	}
}
