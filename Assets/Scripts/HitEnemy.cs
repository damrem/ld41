using UnityEngine;
using System;

public class HitEnemy : MonoBehaviour
{
    public float bounceOnEnemyHeadFactor = 0.75f;
    void OnTriggerEnter2D(Collider2D collider)
    {
        switch (collider.tag)
        {
            case "EnemyWeakPoint":
                Dbg.Log(this, "EnemyWeakPoint");
                Game.teller.Comment("You kill an monster by jumping on its head.");
                GetComponent<JumpBehavior>().Jump(bounceOnEnemyHeadFactor);
                if (GetComponent<Mortal>().IsAlive) collider.GetComponentInParent<Mortal>().Die();
                break;
            case "Lethal":
                Dbg.Log(this, "Lethal");
                Game.teller.Comment("You've been killed by a monster.");
                if (collider.GetComponentInParent<Mortal>().IsAlive) GetComponent<Mortal>().Die();
                break;
        }
    }
}
