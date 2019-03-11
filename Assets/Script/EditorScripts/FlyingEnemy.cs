using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : Enemy
{
    public float horizontalSpeed;
    public float VerticalSpeed;
    public float amplitude;

    private Vector3 tempPosition;

    protected override void Start ()
    {
        base.Start();
        tempPosition = transform.position;
    }

    public override void Move ()
    {
        base.Move();

        tempPosition.y = Mathf.Sin(Time.realtimeSinceStartup * VerticalSpeed) * amplitude;
        transform.position = new Vector3(transform.position.x, tempPosition.y, transform.position.z);
    }

    public override void Death()
    {
        base.Death();
        GetComponent<BoxCollider2D>().enabled = false;
        dead = true;
        Jump();
    }
}
