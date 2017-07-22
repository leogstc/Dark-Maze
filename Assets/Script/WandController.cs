using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class WandController : MonoBehaviour
{
    private Valve.VR.EVRButtonId moveButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad;
    private Valve.VR.EVRButtonId runButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
    public GameObject FPSController;
    public GameObject ViveCameraHead;
    float speed = 3;
    float rotate = 0;
    float cnt;
    bool run = false;

    private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
    private SteamVR_TrackedObject trackedObj;

    [SerializeField] private AudioClip[] m_FootstepSounds;

    private AudioSource m_AudioSource;

    // Use this for initialization
    void Start()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        m_AudioSource = GetComponent<AudioSource>();
        cnt = 0.1f;
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (controller == null)
        {
            Debug.Log("Controller not initialized");
            return;
        }

        //if(controller.GetPress(moveButton)) //移動
        //{
        //    Vector2 pos = controller.GetAxis();
        //    Debug.Log("x: " + pos.x + " y: " + pos.y);
        //    if (pos.y > 0) 
        //    {
        //        Vector3 moveDistance = ViveCameraHead.transform.TransformDirection(Vector3.forward);
        //        Vector3 moveDistance2 = new Vector3(moveDistance.x * Time.deltaTime * speed, 0, moveDistance.z * Time.deltaTime * speed);
        //        FPSController.transform.position += moveDistance2;
        //        ProgressStepCycle();
        //    }
        //    if (pos.y < 0) 
        //    {
        //        Vector3 moveDistance = ViveCameraHead.transform.TransformDirection(Vector3.forward);
        //        Vector3 moveDistance2 = new Vector3(moveDistance.x * Time.deltaTime * speed, 0, moveDistance.z * Time.deltaTime * speed);
        //        FPSController.transform.position -= moveDistance2;
        //        ProgressStepCycle();
        //    }
        //}

        //if (controller.GetPressDown(runButton))
        //{
        //    speed = 6;
        //    cnt = 0.2f;
        //    run = true;
        //}
        //if(controller.GetPressUp(runButton))
        //{
        //    speed = 3;
        //    cnt = 0.4f;
        //    run = false;
        //}
    }

    void FixedUpdate()
    {
        if (controller.GetPress(moveButton)) //移動
        {
            Vector2 pos = controller.GetAxis();
            Debug.Log("x: " + pos.x + " y: " + pos.y);
            if (pos.y > 0)
            {
                Vector3 moveDistance = ViveCameraHead.transform.TransformDirection(Vector3.forward);
                Vector3 moveDistance2 = new Vector3(moveDistance.x * Time.deltaTime * speed, 0, moveDistance.z * Time.deltaTime * speed);
                FPSController.transform.position += moveDistance2;
                ProgressStepCycle();
            }
            if (pos.y < 0)
            {
                Vector3 moveDistance = ViveCameraHead.transform.TransformDirection(Vector3.forward);
                Vector3 moveDistance2 = new Vector3(moveDistance.x * Time.deltaTime * speed, 0, moveDistance.z * Time.deltaTime * speed);
                FPSController.transform.position -= moveDistance2;
                ProgressStepCycle();
            }
        }

        if (controller.GetPressDown(runButton))
        {
            speed = 6;
            cnt = 0.2f;
            run = true;
        }
        if (controller.GetPressUp(runButton))
        {
            speed = 3;
            cnt = 0.4f;
            run = false;
        }
    }

    private void ProgressStepCycle()
    {
        cnt -= Time.deltaTime;
        if (cnt <= 0)
        {
            PlayFootStepAudio();
            cnt = 0.4f;
        }
    }

    private void PlayFootStepAudio() //足音
    {
        int n = Random.Range(1, m_FootstepSounds.Length);
        m_AudioSource.clip = m_FootstepSounds[n];
        m_AudioSource.PlayOneShot(m_AudioSource.clip);
        m_FootstepSounds[n] = m_FootstepSounds[0];
        m_FootstepSounds[0] = m_AudioSource.clip;
    }

}