using UnityEngine;
using System.Collections;

public class EventBGM : MonoBehaviour {

    [SerializeField] private AudioClip ebgm;
    AudioSource audioSource;

    // Use this for initialization
    void Start () {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = ebgm;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter()
    {
        //audioSource.PlayOneShot(ebgm);
        audioSource.Play();
    }

    void OnTriggerExit()
    {
        audioSource.Stop();
    }
}
