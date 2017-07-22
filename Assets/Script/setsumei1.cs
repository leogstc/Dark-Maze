using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class setsumei1 : MonoBehaviour {

    public Text start;
    private float frametime;
    private float nextTime;
    private bool isSceneChange;
    [SerializeField] private AudioClip se;
    AudioSource audioSource;
    public SteamVR_TrackedObject trackedObj;
    bool change;

    // Use this for initialization
    void Start () {
        frametime = 0;
        nextTime = 3;
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = se;
        change = false;
        isSceneChange = false;
    }
	
	// Update is called once per frame
	void Update () {

        frametime += Time.deltaTime;

        if (frametime > nextTime)
        {
            start.color = Color.red;
            start.text = "Press  trigger  to  survive".ToString();
        }

        var device = SteamVR_Controller.Input((int)trackedObj.index);
        if (!audioSource.isPlaying)
        {
            if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
            {
                audioSource.PlayOneShot(se);
                change = true;
            }
        }
        //次のシーンへ
        if (change && !audioSource.isPlaying && !isSceneChange) 
        {
            SteamVR_LoadLevel.Begin("stage1", false, 1f);
            isSceneChange = true;
            Debug.Log("stage1");
        }
    }
}
