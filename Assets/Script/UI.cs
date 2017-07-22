using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour {

	public Text timeGUI;
    public Text overGUI;
    public Text lifeUI;


	public int MAX_TIME = 30;

	private float time;

    public bool isgameover;
    bool scenechange;

    // Use this for initialization
    void Start () {
		time = MAX_TIME;
		timeGUI.text = time.ToString();
        timeGUI.color = Color.blue;
        isgameover = false;
        scenechange = false;
    }
	
	// Update is called once per frame
	void Update () {
		time -= 1f * Time.deltaTime;
		timeGUI.text = ("残り："+(int)time).ToString();

		if (time < 0.0f)
		{
			GameOver();
            time = 0;
		}

        if (isgameover && !scenechange) 
        {
            SteamVR_LoadLevel.Begin("endclear", false, 5f);
            scenechange = true;
            Debug.Log("end");
        }
	}

	void GameOver()
	{
        //gameOverScript.SendMessage("Lose");
        lifeUI.enabled = false;
        timeGUI.enabled = false;
        overGUI.color = Color.red;
        overGUI.text = "Survive".ToString();
        isgameover = true;

	}
}
