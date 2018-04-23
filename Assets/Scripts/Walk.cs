using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Helpers;

public class Walk : MonoBehaviour
{
    public float acceleration = 50;
    public float maxVelocity = 5;
    public WalkDirection direction = WalkDirection.None;
    public float stopDeceleration = 5;
    Rigidbody2D body;
    float initialBodyDrag;
    Coroutine animation;

    // Use this for initialization
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        initialBodyDrag = body.drag;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 directedVector = GameManager.directedWalkVectorMap.Get(direction);
        if (body.velocity.magnitude < maxVelocity) body.AddForce(directedVector * acceleration);
        if (body.velocity.magnitude > maxVelocity) body.velocity.SetMagnitude(maxVelocity);
    }

    public void Left()
    {
        body.drag = initialBodyDrag;
        direction = WalkDirection.Left;
        animation = StartCoroutine(Animate());
    }

    public void Right()
    {
        body.drag = initialBodyDrag;
        direction = WalkDirection.Right;
        animation = StartCoroutine(Animate());
    }

    public void Stop()
    {
        if (body != null) body.drag = stopDeceleration;
        direction = WalkDirection.None;
        if (animation != null) StopCoroutine(animation);
    }

    IEnumerator Animate()
    {
        Dbg.Log(this, "Animate");
        while (true)
        {
            Dbg.Log(this, "while");
            GetComponent<Transform>().localScale.Scale(new Vector2(transform.localScale.x, Rnd.Float()));
            yield return null;
        }
    }
}
