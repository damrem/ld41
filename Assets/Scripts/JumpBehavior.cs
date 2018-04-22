using UnityEngine;
using System;
public class JumpBehavior: MonoBehaviour
{
    public float jumpPower = 500;
    Rigidbody2D body;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    public void Jump(float factor=1)
    {
        Dbg.Log(this, "Jump");
        body.velocity = new Vector2(body.velocity.x, 0);
        body.AddForce(Vector2.up * jumpPower * factor);
    }
}
