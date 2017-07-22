using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    Vector3 startPosition;
    [SerializeField] private float EnemyHP = 3;
    [SerializeField] private AudioClip attackAC;
    [SerializeField] private AudioClip deadAC;
    [SerializeField] private AudioClip walkAC;
    [SerializeField] private AudioClip hitAC;
    [SerializeField] private int EnemyAtk = 10;
    NavMeshAgent agent;
    private GameObject player;
    private Animator ani;
    //public float score = 10;
    public GameObject hiteffect;
    //private ScoreSystem Score;
    bool atk;

    void OnCollisionStay(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            PlayerLife plife = col.gameObject.GetComponent<PlayerLife>();
            if (!atk)
            {
                if (plife)
                {
                    AnimatorStateInfo asi = this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
                    if (asi.normalizedTime > 1f && asi.IsName("attack"))
                    {
                        plife.Damage(EnemyAtk);
                        atk = true;
                    }
                }
            }
        }
    }
    //プレイヤーを見つけたら追いかける
    void OnTriggerStay()
    {
        agent.enabled = true;
        this.GetComponent<CapsuleCollider>().enabled = true;
        ani.enabled = true;
    }

    // Use this for initialization
    void Start () {
        //Score = FindObjectOfType<ScoreSystem>();
        startPosition = transform.localPosition;
        player = GameObject.FindGameObjectWithTag("Player");
        ani = this.GetComponent<Animator>();
        agent = this.GetComponent<NavMeshAgent>();
        atk = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (agent.enabled && EnemyHP > 0)
        {
            agent.destination = player.transform.position;
            if (Vector3.Distance(this.transform.position, player.transform.position) < 2.8f)
            {
                SwitchAttack(true);
                agent.Stop();
                if (!this.GetComponent<AudioSource>().isPlaying)
                {
                    PlaySound(attackAC);
                    atk = false;
                }

            }
            else
            {
                SwitchAttack(false);
                agent.Resume();
                if (!this.GetComponent<AudioSource>().isPlaying)
                    PlaySound(walkAC);
            }
        }
        if (EnemyHP <= 0)
        {
            AnimatorStateInfo asi = this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
            if (asi.normalizedTime > 1f && asi.IsName("back_fall"))
            {
                Destroy(this.gameObject,2f);
            }
        }
    }

    //public void Damage(float damage)
    //{
    //    life -= damage; //体力から差し引く
    //    if (life <= 0)
    //    {
    //        //体力が0以下になった時
    //        Dead(); //死亡処理
    //        Score.AddScore(score);
    //        Instantiate(effect, transform.position, transform.rotation);
    //        DestroyObject(GameObject.Find("ExplosionMobile(Clone)"), 0.5f);
          
    //    }
    //}
    public void Hit(float damage)
    {
        GameObject effect = Instantiate(hiteffect, transform.position, Quaternion.identity) as GameObject;
        effect.transform.localPosition = transform.position + new Vector3(0.0f, 0.5f, 0.0f);
        Destroy(effect, 0.3f);
        float t = EnemyHP - damage;
        PlaySound(hitAC);
        if (t > 0)
        {
            EnemyHP = t;
        }
        else
        {
            EnemyHP = 0;
            //Score.AddScore(score);
            SwitchDead();
        }
    }
    //public void Dead()
    //{
    //    //EaudioSource.PlayOneShot(Ese);
    //    DestroyObject(this.gameObject, 0.5f);
    //}

    void SwitchAttack(bool atk)
    {
        ani.SetBool("isAttack", atk);
    }

    void SwitchDead()
    {
        ani.SetBool("isDead", true);
        agent.Stop();
        this.GetComponent<CapsuleCollider>().enabled = false;
        PlaySound(deadAC);
    }

    void PlaySound(AudioClip ac)
    {
        this.GetComponent<AudioSource>().clip = ac;
        this.GetComponent<AudioSource>().Play();
    }
}
