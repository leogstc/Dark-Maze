using UnityEngine;
using System.Collections;

public class Enemy2 : MonoBehaviour {

    [SerializeField] private float EnemyHP = 3;
    [SerializeField] private AudioClip attackAC;
    [SerializeField] private AudioClip deadAC;
    [SerializeField] private AudioClip walkAC;
    [SerializeField] private AudioClip hitAC;
    [SerializeField] private int EnemyAtk = 10;
    NavMeshAgent agent2;
    private GameObject player;
    private Animator ani;
    public GameObject hiteffect;
    bool atk2;

    void OnCollisionStay(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            PlayerLife plife = col.gameObject.GetComponent<PlayerLife>();
            if (!atk2)
            {
                if (plife)
                {
                    AnimatorStateInfo asi = this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
                    if (asi.normalizedTime > 1f && asi.IsName("attack"))
                    {
                        plife.Damage(EnemyAtk);
                        atk2 = true;
                    }
                }
            }
        }
    }

    void OnTriggerStay()
    {
        agent2.enabled = true;
        this.GetComponent<CapsuleCollider>().enabled = true;
        this.GetComponent<BoxCollider>().enabled = false;
        ani.enabled = true;
    }

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        ani = this.GetComponent<Animator>();
        agent2 = this.GetComponent<NavMeshAgent>();
        atk2 = false;
        agent2.enabled = false;
        this.GetComponent<CapsuleCollider>().enabled = false;
        ani.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (agent2.enabled && EnemyHP > 0)
        {
            agent2.destination = player.transform.position;
            if (Vector3.Distance(this.transform.position, player.transform.position) < 2.8f)
            {
                SwitchAttack(true);
                agent2.Stop();
                if (!this.GetComponent<AudioSource>().isPlaying)
                {
                    PlaySound(attackAC);
                    atk2 = false;
                }

            }
            else
            {
                SwitchAttack(false);
                agent2.Resume();
                if (!this.GetComponent<AudioSource>().isPlaying)
                    PlaySound(walkAC);
            }
        }
        if (EnemyHP <= 0)
        {
            AnimatorStateInfo asi = this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
            if (asi.normalizedTime > 1f && asi.IsName("back_fall"))
            {
                Destroy(this.gameObject, 2f);
            }
        }
    }

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

    void SwitchAttack(bool atk)
    {
        ani.SetBool("isAttack", atk);
    }

    void SwitchDead()
    {
        ani.SetBool("isDead", true);
        agent2.Stop();
        this.GetComponent<CapsuleCollider>().enabled = false;
        PlaySound(deadAC);
    }

    void PlaySound(AudioClip ac)
    {
        this.GetComponent<AudioSource>().clip = ac;
        this.GetComponent<AudioSource>().Play();
    }
}
