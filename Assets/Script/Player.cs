using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
   
  
    public int jumpCount;

    public int maxHearts = 3;
    private int currentHearts;
    public List<GameObject> UIHearts = new List<GameObject>();

    public Vector3 resetPoint;

    public LayerMask boxLayer;

    public float bumpForceUp = 50;
    public float bumpForceHorizonral = 100;
    public float invincibilityTime = 3;

    protected override void Start()
    {
        base.Start();
        resetPoint = transform.position;
        currentHearts = maxHearts;

    }

    // Update is called once per frame
    void Update()
    {

        
        Move();
        if (Input.GetButtonDown("Jump") && jumpCount < 2)
        {
            Jump();
        }
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
        
        anim.SetBool("isGrounded", isGrounded);

    }


    public override void Move()
    {
        horizontal = Input.GetAxis("Horizontal");
        base.Move();
        anim.SetBool("isMoving", Mathf.Abs(horizontal) > 0);
    }

    public override void Jump()
    {
       base.Jump();
       jumpCount += 1;
    }

    
    public override void OnGroundHit()
    {
        base.OnGroundHit();
        jumpCount = 0;
    }

    public override void OnEnemyHit(GameObject enemy)
    {
        base.OnEnemyHit(enemy);
        BoxCollider2D myCollider = GetComponent<BoxCollider2D>();
        //BoxCollider2D enemyCollider = enemy.GetComponent<BoxCollider2D>();
        // float heightDifference = transform.position.y - enemy.transform.position.y;
        float direction = enemy.transform.position.x - transform.position.x;
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, myCollider.size * 0.9f, 0, new Vector2(direction, 0), 1, boxLayer);
        if(!(hit && hit.collider.gameObject == enemy))
        {
            enemy.GetComponent<Entity>().Death();
        }
        else
        {
            Vector3 force = new Vector3();
            force.x = Mathf.Sign(transform.position.x - enemy.transform.position.x) * bumpForceHorizonral;
            force.y = bumpForceUp;
            rb.AddForce(force);
            StopAllCoroutines();
            StartCoroutine("MakeInvicible");
            Death();
        }
    }

    public override void Death()
    {
        base.Death();
        currentHearts--;
        UIHearts[currentHearts].SetActive(false);
        if (currentHearts <= 0)
        {
            transform.position = resetPoint;
            ResetHearts();
        }
    }

       void ResetHearts()
        {
            currentHearts = maxHearts;
            foreach(GameObject heart in UIHearts)
            {
                heart.SetActive(true);
            }
        }

    public bool IsMaxHearts()
    {
        return currentHearts == maxHearts;
    }

    public void AddHeart()
    {
        if(currentHearts < maxHearts)
        {
            UIHearts[currentHearts].SetActive(true);
            currentHearts++;
        }
    }

    IEnumerator MakeInvicible()
    {
        gameObject.layer = 11;  //Invicible layer
        StartCoroutine("PlayInvinibilityEffect");
        yield return new WaitForSeconds(invincibilityTime);
        gameObject.layer = 9;   // Player layer
        StopCoroutine("PlayInvinibilityEffect");
        anim.gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }

    IEnumerator PlayInvinibilityEffect()
    {
        bool toggle = false;
        while(true)
        {
            anim.gameObject.GetComponent<SpriteRenderer>().enabled = toggle;
            yield return new WaitForSeconds(0.5f);
            toggle = !toggle;
        }
    }
    

}
