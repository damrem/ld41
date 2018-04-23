using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Mortal : MonoBehaviour
{
    private bool isDead;
    private Coroutine dyingAnimation;

    public bool IsDead
    {
        get
        {
            return isDead;
        }
    }
    public bool IsAlive
    {
        get
        {
            return !isDead;
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Die()
    {
        Dbg.Log(this, "Die");
        GetComponent<Walk>().direction = WalkDirection.None;
        dyingAnimation = StartCoroutine(AnimateDying());
        GetComponent<Rigidbody2D>().simulated = false;
        foreach (Rigidbody2D body in GetComponentsInChildren<Rigidbody2D>()) body.simulated = false;
        //GetComponent<BoxCollider2D>().enabled = false;
        isDead = true;
    }

    public void Resurrect()
    {
        isDead = false;
        if (dyingAnimation != null) StopCoroutine(dyingAnimation);
        transform.position = new Vector2();
        transform.localScale = new Vector3(1, 1, 1);
        foreach (Rigidbody2D body in GetComponentsInChildren<Rigidbody2D>()) body.simulated = true;
        GetComponent<Rigidbody2D>().simulated = true;
        GetComponent<Walk>().direction = WalkDirection.None;
    }

    IEnumerator AnimateDying()
    {
        while (Mathf.Abs(transform.localScale.x) > float.Epsilon)
        {
            float intermediateScale = transform.localScale.x * 0.75f;
            transform.localScale = new Vector3(intermediateScale, intermediateScale, intermediateScale);
            yield return null;
        }
    }
}
