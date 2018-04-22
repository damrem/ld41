using UnityEngine;
using System;

public class HitEnemy : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        Dbg.Log(this, "start");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Dbg.Log(this, "OnCollisionEnter2D", collider.tag);
        switch (collider.tag)
        {
            //case "Lethal":
            //    GetComponent<Mortal>().Die();
            //    break;
            case "EnemyWeakPoint":
                Dbg.Log(this, collider.GetComponentInParent<Mortal>());
                collider.GetComponentInParent<Mortal>().Die();
                break;
            case "Lethal":
                if (collider.enabled) GetComponent<Mortal>().Die();
                break;
        }
    }

    //void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Collider2D collider = collision.collider;
    //    Dbg.Log(this, "OnCollisionEnter2D", collider.tag);
    //    switch (collider.tag)
    //    {
    //        case "Lethal":
    //            if(collider.enabled) GetComponent<Mortal>().Die();
    //            break;
    //        //case "EnemyWeakPoint":
    //        //    collider.GetComponent<Mortal>().Die();
    //        //    break;
    //    }
    //    //Dbg.Log(this, "Lethal", collision.collider.CompareTag("Lethal"));
    //    //Dbg.Log(this, "EnemyWeakPoint", collision.collider.CompareTag("EnemyWeakPoint"));
    //}
}
