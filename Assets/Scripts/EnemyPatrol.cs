using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public WalkDirection initialWalkDirection = WalkDirection.Left;

    Walk walk;

    void Start()
    {
        walk = GetComponent<Walk>();
        walk.direction = initialWalkDirection;
    }

    private void ToggleWalkDirection()
    {
        if (walk.direction == WalkDirection.Left) walk.direction = WalkDirection.Right;
        else walk.direction = WalkDirection.Left;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ToggleWalkDirection();
    }

    
}
