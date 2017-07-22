using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class setsumei3 : MonoBehaviour {

    public Text start;

    private float frametime;
    private float nextTime;
    private bool isSceneChange;

    public AudioClip se;
    public SteamVR_TrackedObject trackedObj;
    bool change;

    AudioSource audioSource;

    // Use this for initialization
    void Start()
    {
        frametime = 0;
        nextTime = 3;
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = se;
        change = false;
    }

    // Update is called once per frame
    void Update()
    {
        frametime += Time.deltaTime;

        if (frametime > nextTime)
        {
            start.color = Color.red;
            start.text = "Press  trigger  to  escape".ToString();
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

        if (change && !audioSource.isPlaying && !isSceneChange) 
        {
            SteamVR_LoadLevel.Begin("stage3", false, 1f);
            isSceneChange = true;
            Debug.Log("stage3");
        }
    }
}
