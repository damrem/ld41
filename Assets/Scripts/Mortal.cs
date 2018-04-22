using UnityEngine;
using System;
using System.Collections;

public class Mortal : MonoBehaviour
{

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
        StartCoroutine(AnimateDying());
        GetComponent<Rigidbody2D>().simulated = false;
        //GetComponent<BoxCollider2D>().enabled = false;


    }

    IEnumerator AnimateDying()
    {
        while (Mathf.Abs(transform.localScale.x) > float.Epsilon)
        {
            float intermediateScale = transform.localScale.x * 0.75f;
            transform.localScale = new Vector3(intermediateScale, intermediateScale, intermediateScale);
            yield return null;
        }
        transform.localScale = new Vector3(1, 1, 1);
    }
}
