using UnityEngine;
using System.Collections;

public class ArrowManager : MonoBehaviour {

    public static ArrowManager Instance;
    public SteamVR_TrackedObject trackedObj;
    private GameObject currentArrow;
    public GameObject arrowPrefab;

    public GameObject StringAttachPoint;
    public GameObject ArrowStartPoint;
    public GameObject StringStartPoint;
    [SerializeField] private AudioClip se;
    AudioSource audioSource;
    private bool isAttach = false;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void Ondeststory()
    {
        //if (Instance == this)
        //    Instance = null;
    }

    // Use this for initialization
    void Start () {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = se;

    }

    // Update is called once per frame
    void Update () {
        AttachArrow();
        PullString();
        ArrowReset();
    }

    private void PullString() //弓を引く
    {
        if(isAttach)
        {
            float dist = (StringStartPoint.transform.position - trackedObj.transform.position).magnitude;
            StringAttachPoint.transform.localPosition = StringStartPoint.transform.localPosition + new Vector3(4f * dist, 0f, 0f);

            var device = SteamVR_Controller.Input((int)trackedObj.index);
            device.TriggerHapticPulse(1000);
            if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
            {
                Fire();
                audioSource.PlayOneShot(se);
            }
        }
    }

    private void Fire()
    {
        float dist = (StringStartPoint.transform.position - trackedObj.transform.position).magnitude;

        currentArrow.transform.parent = null;
        currentArrow.GetComponent<Arrow>().Fired();

        Rigidbody r = currentArrow.GetComponent<Rigidbody>();
        r.velocity = currentArrow.transform.forward * 25f * dist;
        r.useGravity = true;

        currentArrow.GetComponent<Collider> ().isTrigger = false;

        StringAttachPoint.transform.position = StringStartPoint.transform.position;

        currentArrow = null;
        isAttach = false;
    }

    private void AttachArrow()
    {
        if(currentArrow==null)
        {
            currentArrow = Instantiate(arrowPrefab);
            currentArrow.transform.parent = trackedObj.transform;
            currentArrow.transform.localPosition = new Vector3(0.0f, 0.0f, 0.342f);
            currentArrow.transform.localRotation = Quaternion.identity;

        }
    }

    public void AttachingBowtoArrow()
    {
        currentArrow.transform.parent = StringAttachPoint.transform;
        currentArrow.transform.position = ArrowStartPoint.transform.position;
        currentArrow.transform.rotation = ArrowStartPoint.transform.rotation;

        isAttach = true;

    }

    //矢の位置をリセットする
    private void ArrowReset()
    {
        var device = SteamVR_Controller.Input((int)trackedObj.index);
        if (device.GetTouchUp(SteamVR_Controller.ButtonMask.ApplicationMenu))
        {
            currentArrow.transform.localPosition = new Vector3(0.0f, 0.0f, 0.342f);
        }
    }
}
