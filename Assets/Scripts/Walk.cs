using UnityEngine;
using System.Collections.Generic;

public class Walk : MonoBehaviour
{
    public float acceleration = 50;
    public float maxVelocity = 5;
    public WalkDirection direction = WalkDirection.None;
    public float stopDeceleration = 5;
    Rigidbody2D body;
    float initialBodyDrag;

    // Use this for initialization
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        initialBodyDrag = body.drag;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 directedVector = Game.directedWalkVectorMap.Get(direction);
        if (body.velocity.magnitude < maxVelocity) body.AddForce(directedVector * acceleration);
        if (body.velocity.magnitude > maxVelocity) body.velocity.SetMagnitude(maxVelocity);
    }

    public void Left()
    {
        body.drag = initialBodyDrag;
        direction = WalkDirection.Left;
    }

    public void Right()
    {
        body.drag = initialBodyDrag;
        direction = WalkDirection.Right;
    }

    public void Stop()
    {
        body.drag = stopDeceleration;
        direction = WalkDirection.None;
    }
}
