using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : Enemy
{
    public float shootInterval = 1;

    protected override void Start()
    {
        base.Start();
        StartCoroutine("Attack");
    }

    IEnumerator Attack()
    {
        while (!dead)
        {
            yield return new WaitForSeconds(shootInterval);
            Shoot();
        }

    }
}