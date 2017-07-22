using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    public readonly float maxLife = 100; //最大体力
    public float life = 100; //現在体力
    public bool isdead;
    bool scenechange;

    public Text LifeGUI;
    public Text overGUI;
    public Text timeUI;

    [SerializeField] private AudioClip Damagese;
    [SerializeField] private AudioClip deadse;
    AudioSource audioSource;

    // Use this for initialization
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = Damagese;
        life = maxLife;
        isdead = false;
        LifeGUI.color = Color.yellow;
        scenechange = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (life <= 0 && !isdead) 
        {
            //体力が0になったら
            audioSource.clip = deadse;
            audioSource.PlayOneShot(deadse);
            Dead();
        }

        if (isdead && !scenechange) 
        {
            SteamVR_LoadLevel.Begin("end", false, 5f,50f);
            scenechange = true;
            Debug.Log("end");
        }
    }

    public void Damage(float damage)
    {
        if (!isdead)
        {
            life -= damage; //体力を減らす
            audioSource.PlayOneShot(Damagese);
        }

    }

    public void Dead()
    {
        //SceneManager.LoadScene(Application.loadedLevel); //シーンの再読み込み
        overGUI.color = Color.red;
        overGUI.text = "GAME OVER".ToString();
        this.GetComponentInChildren<MoveController>().enabled = false;
        isdead = true;
        LifeGUI.enabled = false;
        timeUI.enabled = false;
    }

    void OnGUI()
    {
        LifeGUI.text = ("Life " + life).ToString();
    }
}
