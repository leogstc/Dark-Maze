using UnityEngine;
using System.Collections;

public class tennmetu : MonoBehaviour
{

    private GameObject textObject; //点滅させたい文字

    private float nextTime;
    public float interval = 0.8f; //点滅周期

    // Use this for initialization
    void Start()
    {
        textObject = GameObject.Find("push");
        nextTime = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //一定時間ごとに点滅
        if (Time.fixedTime > nextTime)
        {
            float alpha = textObject.GetComponent<CanvasRenderer>().GetAlpha();
            if (alpha == 1.0f)
                textObject.GetComponent<CanvasRenderer>().SetAlpha(0.0f);
            else
                textObject.GetComponent<CanvasRenderer>().SetAlpha(1.0f);

            nextTime += interval;
        }

    }
}