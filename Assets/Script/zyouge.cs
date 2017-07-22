using UnityEngine;
using System.Collections;

public class zyouge : MonoBehaviour
{
    Vector3 startPosition;

    public float amplitude;
    public float speed;

	// Use this for initialization
	void Start ()
    {
        startPosition = transform.localPosition;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //変位を計算
        float y = amplitude * Mathf.Sin(Time.time * speed);

        //ｙを変位させたポジションに際設定
        transform.localPosition = startPosition + new Vector3(0, y, 0);
	
	}
}
