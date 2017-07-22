using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour {

    public float attack = 10f; 

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            //プレイヤーと衝突した時
            Attack(col.gameObject); //攻撃する
        }
    }

    //攻撃する際に呼び出す
    public void Attack(GameObject hit)
    {
        hit.gameObject.SendMessage("Damage", attack);   //Playerの"Damage"関数を呼び出す
    }
}
