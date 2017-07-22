using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour {

    public Text scoreGUI;

    private float score;

    // Use this for initialization
    void Start ()
    {
        score = 0;
        scoreGUI.color = Color.blue;
	}
	
	// Update is called once per frame
	void Update ()
    {
        scoreGUI.text = ("Score：" + score).ToString();
	}

    public void AddScore(float addScore)
    {
        score += addScore;
    }

}
