using UnityEngine;
using System.Collections;

public class Stage3Goal : MonoBehaviour {

    void OnTriggerStay()
    {
      SteamVR_LoadLevel.Begin("endclear", false,1f);
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
