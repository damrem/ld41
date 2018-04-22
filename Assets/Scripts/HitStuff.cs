using UnityEngine;
using System;

public class HitStuff : MonoBehaviour
{
    public float bounceOnEnemyHeadFactor = 0.75f;
    bool isAtDoor = false;
    public bool IsAtDoor { get { return isAtDoor; } }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Dbg.Log(this, "trigger", collider.tag);
        switch (collider.tag)
        {
            case "EnemyWeakPoint":
                Dbg.Log(this, "EnemyWeakPoint");
                Game.teller.Tell("You kill an monster by jumping on its head.");
                GetComponent<JumpBehavior>().Jump(bounceOnEnemyHeadFactor);
                if (GetComponent<Mortal>().IsAlive) collider.GetComponentInParent<Mortal>().Die();
                break;
            case "Lethal":
                Dbg.Log(this, "Lethal");
                Game.teller.Tell("You've been killed by a monster.");
                if (collider.GetComponentInParent<Mortal>().IsAlive) GetComponent<Mortal>().Die();
                break;
            case "Door":
                isAtDoor = true;
                Game.teller.Tell("You reach a door. Who knows where it could lead you... Probably to next level?");
                Game.teller.Comment("Type \"enter\".");
                break;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Door":
                isAtDoor = false;
                Game.teller.Tell("You leave the door, like you don't care. Up to you.");
                Game.teller.Tell("Maybe next time?");
                break;
        }
    }
}
