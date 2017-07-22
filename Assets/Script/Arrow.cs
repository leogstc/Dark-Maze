using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {

    private bool isAttach;
    private bool isFired;
    private bool isChange;
    public float attack = 1;
    void OnTriggerStay()
    {
        AttachArrow();
    }

    void OnTriggerEnter()
    {
        AttachArrow();
    }

    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Enemy" && isFired) 
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy)
            {
                enemy.Hit(attack);
            }
        }

        if (collision.gameObject.tag == "Enemys3" && isFired) 
        {
            Enemy2 enemy2 = collision.gameObject.GetComponent<Enemy2>();
            if (enemy2)
            {
                enemy2.Hit(attack);
            }
        }
        Destroy(gameObject);

        //ステージ選択＆シーン切り替え
        if (collision.gameObject.tag == "stage1" && !isChange) 
        {
            SteamVR_LoadLevel.Begin("setumei1", false, 1f);
            isChange = true;
            Debug.Log("stage1");
        }
        if (collision.gameObject.tag == "stage2" && !isChange) 
        {
            SteamVR_LoadLevel.Begin("setumei2", false, 1f);
            isChange = true;
            Debug.Log("stage2");
        }
        if (collision.gameObject.tag == "stage3" && !isChange) 
        {
            SteamVR_LoadLevel.Begin("setumei3", false, 1f);
            isChange = true;
            Debug.Log("stage3");
        }
        if (collision.gameObject.tag == "title" && !isChange) 
        {
            SteamVR_LoadLevel.Begin("darkmaze_title", false, 1f);
            isChange = true;
            Debug.Log("title");
        }
        if (collision.gameObject.tag == "retry" && !isChange) 
        {
            SteamVR_LoadLevel.Begin("stage_select", false, 1f);
            isChange = true;
            Debug.Log("select");
        }
    }

    void Start()
    {
        isFired = false;
        isAttach = false;
        isChange = false;
    }

    void Update()
    {
        if(isFired&& transform.GetComponent<Rigidbody>().velocity.magnitude>5f)
        {
            transform.LookAt(transform.position + transform.GetComponent<Rigidbody>().velocity);
        }
    }

    public void Fired()
    {
        isFired = true;
        this.GetComponentInChildren<TrailRenderer>().enabled = true;
    }

    private void AttachArrow() //弓を触れたら
    {
        var device = SteamVR_Controller.Input((int)ArrowManager.Instance.trackedObj.index);
        if(!isAttach && device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
        {
            ArrowManager.Instance.AttachingBowtoArrow();
            isAttach = true;
            
        }

    }
}
