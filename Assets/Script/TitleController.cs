using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour
{

    public SteamVR_TrackedObject trackedObj;
    
    private SteamVR_LoadLevel loadlevel;
    private string levelNames = "readme";

    public AudioClip se;
    AudioSource audioSource;
    bool change;

    // Use this for initialization
    void Start () {
        loadlevel = gameObject.GetComponent<SteamVR_LoadLevel>();
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = se;
        change = false;
    }
	
	// Update is called once per frame
	void Update () {
        var device = SteamVR_Controller.Input((int)trackedObj.index);
        if (!audioSource.isPlaying)
        {
            if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
            {
                audioSource.PlayOneShot(se);
                change = true;
            }
        }
        if (change&&!audioSource.isPlaying)
        {
            loadlevel.loadingScreenDistance = 4f;
            loadlevel.levelName = levelNames;
            loadlevel.Trigger();
            //SteamVR_LoadLevel.Begin(levelNames, false,1f);
            Debug.Log("stage");
            
        }
        
    }
}
