using UnityEngine;
using System.Collections;

public class light : MonoBehaviour {

    [SerializeField] private Light lights;
    private float nextTime;
    public float interval; //点滅周期

    // Use this for initialization
    void Start () {
        lights.enabled = true;
        nextTime = 1;
    }
	
	// Update is called once per frame
	void Update () {
        interval = Random.Range(0f, 1f);
        if (Time.fixedTime > nextTime)
        {
            if (lights.enabled)
                lights.enabled = false;
            else
                lights.enabled = true;
            nextTime += interval;
        }
    }
}
