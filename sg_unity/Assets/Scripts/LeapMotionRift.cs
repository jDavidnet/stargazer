using UnityEngine;
using System.Collections;
using OVR;

public class LeapMotionRift : MonoBehaviour {
  public Camera normalCamera;
  public OVRCameraController riftCamera;
  public HandController leapController;

  private Camera activeCamera;

  public Camera GetActiveCamera()
  {
    return activeCamera;
  }

  private bool isRiftConnected()
  {
    Hmd hmd = OVR.Hmd.GetHmd();
    ovrTrackingState ss = hmd.GetTrackingState();
    return (ss.StatusFlags & (uint)ovrStatusBits.ovrStatus_HmdConnected) != 0;
  }

	// Use this for initialization
	void Start () {
    Vector3 leapControllerPosition = new Vector3(0.0f, 0.0f, 0.08f); // Leap is positioned outwards by 80cm to mirror the world position
    if (isRiftConnected())
    {
      riftCamera.gameObject.SetActive(true);
      normalCamera.gameObject.SetActive(false);
      activeCamera = riftCamera.CameraMain;
      leapControllerPosition.x = riftCamera.IPD / 2.0f; // Centers the Leap Controller between two Rift cameras
    }
    else
    {
      normalCamera.gameObject.SetActive(true);
      riftCamera.gameObject.SetActive(false);
      activeCamera = normalCamera.camera;
    }
    leapController.transform.parent = activeCamera.transform;
    leapController.transform.rotation = activeCamera.transform.rotation * Quaternion.Euler(270.0f, 180.0f, 0.0f);
    leapController.transform.localPosition = leapControllerPosition;
	}
}