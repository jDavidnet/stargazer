/******************************************************************************\
* Copyright (C) Leap Motion, Inc. 2011-2014.                                   *
* Leap Motion proprietary. Licensed under Apache 2.0                           *
* Available at http://www.apache.org/licenses/LICENSE-2.0.html                 *
\******************************************************************************/

using UnityEngine;
using System.Collections;
using Leap;

// The model for our rigid hand made out of various polyhedra.
public class RigidHand : SkeletalHand {

  Controller controller;
  private LineRenderer lineRenderer;
  public float filtering = 0.5f;

  void Start() {
    palm.rigidbody.maxAngularVelocity = Mathf.Infinity;
    Leap.Utils.IgnoreCollisions(gameObject, gameObject);
  }

  public override void InitHand() {
    base.InitHand();
	controller = new Controller ();
	controller.EnableGesture (Gesture.GestureType.TYPE_SWIPE);
  }

  public override void UpdateHand() {

	GameObject go = GameObject.FindGameObjectWithTag("CameraCenter");

    for (int f = 0; f < fingers.Length; ++f) {
      if (fingers[f] != null)
        fingers[f].UpdateFinger();
		

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

	Frame frame = controller.Frame();
	foreach (Gesture g in frame.Gestures())
		if (g.Type == Gesture.GestureType.TYPE_SWIPE)
			print ("Swipe");
    if (palm != null) {
      // Set palm velocity.
      Vector3 target_position = GetPalmCenter();
      palm.rigidbody.velocity = (target_position - palm.transform.position) *
                                (1 - filtering) / Time.deltaTime;

      // Set palm angular velocity.
      Quaternion target_rotation = GetPalmRotation();
      Quaternion delta_rotation = target_rotation *
                                  Quaternion.Inverse(palm.transform.rotation);
      float angle = 0.0f;
      Vector3 axis = Vector3.zero;
      delta_rotation.ToAngleAxis(out angle, out axis);

      if (angle >= 180) {
        angle = 360 - angle;
        axis = -axis;
      }
      if (angle != 0) {
        float delta_radians = (1 - filtering) * angle * Mathf.Deg2Rad;
        palm.rigidbody.angularVelocity = delta_radians * axis / Time.deltaTime;
      }
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
}
