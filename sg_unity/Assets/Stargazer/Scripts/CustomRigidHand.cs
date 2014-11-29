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

		for (int f = 0; f < fingers.Length; ++f) 
		{
			//		Vector3 start = fingers[f].GetTipPosition();
			Vector3 direction = (fingers[f].GetTipPosition() - go.transform.position).normalized; //GetPalmNormal();
			RaycastHit hit;
			if (Physics.Raycast(go.transform.position, direction, out hit))
			{
				//			if (hit.collider.gameObject.name.StartsWith("L_")) {
				//				print ("Finger " + f + " hit: " + hit.collider.gameObject.name);
				//				Debug.DrawRay(go.transform.position, direction);
				hit.collider.SendMessage("OnMouseOver", SendMessageOptions.DontRequireReceiver);
				//			}
			}
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
