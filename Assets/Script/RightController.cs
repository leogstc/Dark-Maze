using UnityEngine;
using System.Collections;

public class RightController : MonoBehaviour {

    private Valve.VR.EVRButtonId moveButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad;
    public GameObject FPSController;
    float rotate = 0;

    private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
    private SteamVR_TrackedObject trackedObj;

    // Use this for initialization
    void Start () {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }
	
	// Update is called once per frame
	void Update () {

        if (controller == null)
        {
            Debug.Log("Controller not initialized");
            return;
        }
        //カメラ視点変更
        if (controller.GetPressDown(moveButton))
        {
            Vector2 pos = controller.GetAxis();
            Debug.Log("x: " + pos.x + " y: " + pos.y);
            if (pos.x > 0.5)
            {
                rotate += 30f;
                FPSController.transform.rotation = Quaternion.Euler(0, rotate, 0);
            }
            if (pos.x < -0.5)
            {
                rotate -= 30f;
                FPSController.transform.rotation = Quaternion.Euler(0, rotate, 0);
            }
        }
    }
}
