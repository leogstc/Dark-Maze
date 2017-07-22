using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ReadmeManager : MonoBehaviour {


    public SteamVR_TrackedObject trackedObj;
    public Text start;

    //public Image controllerreadme;
    [SerializeField] private Image[] readme;
    //public Image readme2;
    //public Image readme3;
    private Image now;

    int no;
    bool scenechange;
    bool readmeover;

    public AudioClip se;
    AudioSource audioSource;

    private SteamVR_LoadLevel loadlevel;
    private string levelNames = "stage_select";

    // Use this for initialization
    void Start () {
        loadlevel = gameObject.GetComponent<SteamVR_LoadLevel>();
        //controllerreadme.enabled = false;
        //readme2.enabled = false;
        //readme3.enabled = false;
        for (int i=1;i<readme.Length;++i)
        {
            readme[i].enabled = false;
        }
        now = readme[0];
        no = 0;
        start.color = Color.red;
        scenechange = false;
        readmeover = false;

        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = se;
    }
	
	// Update is called once per frame
	void Update () {
        //次のページ
        var device = SteamVR_Controller.Input((int)trackedObj.index);
        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            Vector2 pos = device.GetAxis();
            if (pos.x > 0 && no < readme.Length - 1) 
            {
                no++;
                if (no >= readme.Length) no = readme.Length - 1;
                now.enabled = false;
                readme[no].enabled = true;
                now = readme[no];

                audioSource.PlayOneShot(se);
            }

            if (pos.x < 0 && no > 0) 
            {
                no--;
                if (no <= 0) no = 0;
                now.enabled = false;
                readme[no].enabled = true;
                now = readme[no];

                audioSource.PlayOneShot(se);
            }
        }
        //最後のページ
        if(no== readme.Length - 1)
        {
            start.text = "Press  trigger  to  start".ToString();
            readmeover = true;
        }

        if (readmeover)
        {
            //トリガー押して次のシーンへ
            if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger) && !scenechange)
            {
                readme[no].enabled = false;
                start.enabled = false;
                loadlevel.loadingScreenDistance = 4f;
                loadlevel.levelName = levelNames;
                loadlevel.Trigger();
                scenechange = true;
            }
        }
    }
}
