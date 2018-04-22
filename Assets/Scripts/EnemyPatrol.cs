using UnityEngine;
using System;
using System.Collections.Generic;

public class EnemyPatrol : MonoBehaviour
{
    Rigidbody2D body;
    public WalkDirection initialWalkDirection = WalkDirection.Left;
    //WalkDirection walkDirection;

    //public float walkMaxVelocity = 4;
    //public float walkAcceleration = 40;
    Walk walk;

    // Use this for initialization
    void Start()
    {
        walk = GetComponent<Walk>();
        walk.direction = initialWalkDirection;
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //wWalk(walkDirection);
        if (body.velocity.magnitude < float.Epsilon) ToggleWalkDirection();
    }

    private void ToggleWalkDirection()
    {
        if (walk.direction == WalkDirection.Left) walk.direction = WalkDirection.Right;
        else walk.direction = WalkDirection.Left;
    }

    //void Walk(WalkDirection direction)
    //{
    //    Vector2 directedVector = Game.directedWalkVectorMap.Get(direction);
    //    if (body.velocity.magnitude < walkMaxVelocity) body.AddForce(directedVector * walkAcceleration);
    //    if (body.velocity.magnitude > walkMaxVelocity) body.velocity.SetMagnitude(walkMaxVelocity);
    //}
}
