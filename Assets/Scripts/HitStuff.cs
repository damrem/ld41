using UnityEngine;
using System;

public class HitStuff : MonoBehaviour
{
    public float bounceOnEnemyHeadFactor = 0.75f;
    static public bool isAtDoor = false;

    void OnTriggerEnter2D(Collider2D collider)
    {
        Dbg.Log(this, "triggerenter", collider.tag);
        switch (collider.tag)
        {
            case "EnemyWeakPoint":
                Dbg.Log(this, "EnemyWeakPoint", GetComponent<Mortal>().IsAlive);
                GameManager.instance.Tell("You kill a monster by jumping on its head.");
                GetComponent<JumpBehavior>().Jump(bounceOnEnemyHeadFactor);
                
                if (GetComponent<Mortal>().IsAlive) collider.GetComponentInParent<Mortal>().Die();
                break;

            case "Lethal":
                Dbg.Log(this, "Lethal");
                GameManager.instance.Tell(
                    "You've been killed by a monster.",
                    "That's bad oviously.",
                    "It means you're dead.",
                    "And you'll have to start the level again."
                );
                if (collider.GetComponentInParent<Mortal>().IsAlive) GetComponent<Mortal>().Die();
                Loader.Respawn();
                break;

            case "Door":
                isAtDoor = true;
                GameManager.instance.Tell("You reach a door.", "Who knows where it could lead you...", "Probably to the next level?", "Type \"exit\".");
                break;

            case "Pit":
                Loader.Respawn();
                GameManager.instance.Tell(
                    "You fell into a very very deep pit.",
                    "Imagine the noise the impact has made and the look of what's left of your \"body\".",
                    "You can't respawn in real life. Don't try this at home.",
                    "But you're lucky it's a game: you've respawned."
                );
                break;
            case "Finish":
                Dbg.Log(this, "Finish");
                GameManager.instance.isAtFinalDoor = true;
                break;
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        Dbg.Log(this, "triggerexit", collider.tag);
        switch (collider.tag)
        {
            case "Door":
                isAtDoor = false;
                GameManager.instance.Tell(
                    "You leave the door, like you don't care. Up to you.",
                    "Maybe next time?"
                );
                break;
            case "Finish":
                Dbg.Log(this, "Unfinish");
                GameManager.instance.isAtFinalDoor = false;
                break;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Dbg.Log(this, "OnCollisionEnter2D", collision, collision.gameObject.tag);
        switch (collision.gameObject.tag)
        {
            case "Wall":
                GameManager.instance.Tell("You hit a wall.", "Laaame.");
            break;
        }
    }
}
