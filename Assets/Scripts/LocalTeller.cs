using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LocalTeller : MonoBehaviour
{
    //It seems like you have only one way around. Why don't you type "right"?
    public List<string> speeches;
    public float delaySec = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.Tell(speeches);
        }
    }

}
