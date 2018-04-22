using UnityEngine;
using System;

public class HitEnemy : MonoBehaviour
{
    public float bounceOnEnemyHeadFactor = 0.5f;
    void OnTriggerEnter2D(Collider2D collider)
    {
        switch (collider.tag)
        {
            case "EnemyWeakPoint":
                Dbg.Log(this, collider.GetComponentInParent<Mortal>());
                GetComponent<JumpBehavior>().Jump(bounceOnEnemyHeadFactor);
                collider.GetComponentInParent<Mortal>().Die();
                break;
            case "Lethal":
                GetComponent<Mortal>().Die();
                break;
        }
    }
}
