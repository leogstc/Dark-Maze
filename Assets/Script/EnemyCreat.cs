using UnityEngine;
using System.Collections;

public class EnemyCreat : MonoBehaviour {

    [SerializeField] private GameObject zombie;
    [SerializeField] private float interval = 5f;
    private float timecount = 0;

    // Use this for initialization
    void Start () {
        timecount = 2f;

    }
	
	// Update is called once per frame
	void Update () {
        if(timecount>0)
        {
            timecount -= Time.deltaTime;
        }
        else
        {
            GameObject e = Instantiate(zombie, this.transform.position, this.transform.rotation) as GameObject;
            timecount=interval;
        }
    }

}
