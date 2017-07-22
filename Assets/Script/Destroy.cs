using UnityEngine;
using System.Collections;

public class Destroy : MonoBehaviour
{

    public AudioClip takese;
    AudioSource audioSource;
    private GameObject _parent;
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "arrow")
        {
            //相手のタグがplayerならば、自分を消す
            audioSource.PlayOneShot(takese);
            DestroyObject(this.gameObject);
            //DestroyObject(_parent,1f);
            _parent.GetComponentInParent<MeshRenderer>().enabled = false;
            _parent.GetComponentInParent<BoxCollider>().enabled = false;
        }
    }

    // Use this for initialization
    void Start ()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = takese;
        //親オブジェクトを取得
        
        _parent = transform.root.gameObject;

            Debug.Log("Parent:" + _parent.name);
    }

        // Update is called once per frame
        void Update () {
	
	}
}
