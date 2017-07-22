using UnityEngine;
using System.Collections;

public class SountScript : MonoBehaviour {

    public SteamVR_TrackedObject trackedObj;
    public AudioClip se;
    AudioSource audioSource;

    // Use this for initialization
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = se;
    }

    // Update is called once per frame
    void Update()
    {
        var device = SteamVR_Controller.Input((int)trackedObj.index);
        if (device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
        {
            audioSource.PlayOneShot(se);
        }
    }
}
