using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    HelperScript helper;
    Rigidbody2D rb;
    Animator anim;
    public GameObject player;
    public GameObject enemy;
    public bool isGrounded;
    public bool isAttacking;
    public bool doPatrol;

    public GameObject triggerArea;
    float waitDelay;

    public LayerMask playerLayerMask;


    float ex, px;
    float r = 4f;

    public bool chasePlayer = false;

    int health = 10;
    int playerHealth = 10;
    
    private float hitRange = 0.3f;

    int enemyDir;
    public bool playerInsideTriggerArea;


    void Start()
    {
        // Add the helper script and store a reference to it                                               
        helper = gameObject.AddComponent<HelperScript>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        enemyDir = 6;
        doPatrol= false;
        isAttacking = false;
        playerInsideTriggerArea = false;
        waitDelay = 0;
    }


    void Update()
    {
        if( waitDelay > 0)
        {
            //set idle here
            anim.SetBool("idle", true);
            waitDelay -= Time.deltaTime;
            //print("delay=" + waitDelay);
            return;
        }


        EnemyChase1();
        EnemyPatrol();

        //EnemyAttack();

        //EnemyGrounded();

        EnemyDie();

    }

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
    void EnemyChase1()
    {
        //check for player entering trigger area

        

        if (playerInsideTriggerArea == false)
        {
            return;
        }



        ex = enemy.transform.position.x;
        px = player.transform.position.x;


        if (playerInsideTriggerArea == true)
        {

            if (helper.ExtendedRayCollisionCheck(0.5f, 0.4f) && (helper.ExtendedRayCollisionCheck(-0.5f, 0.4f) == true))
            {
                isGrounded = true;
            }

            else
            {
                isGrounded = false;
            }




            if (ex > px)
            {

                rb.linearVelocity = new Vector2(r, 0);
                anim.SetBool("move", true);
                helper.FlipObject(false);
            }

            if (ex < px)
            {
                anim.SetBool("move", true);
                rb.linearVelocity = new Vector2(-r, 0);
                helper.FlipObject(true);

            }
        }
    }

    void EnemyPatrol()
    {


        if (anim.GetBool("attack") == true)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        if (anim.GetBool("attack") == false)
        {
            anim.SetBool("move", true);
        }

            
        if (triggerArea.GetComponent<EnemyTriggerScript>().playerInsideTriggerArea == true)
        {
            return;
        }




        rb.linearVelocity = new Vector2(enemyDir, 0);



        // check for righthand side
        if (helper.ExtendedRayCollisionCheck(0.5f, 0.0f) == false)
        {
            if (enemyDir > 0)
            {
                enemyDir = -enemyDir;
                helper.FlipObject(true);
                anim.SetBool("move", true);
            }
        }

        // check for lefthand side

        if (helper.ExtendedRayCollisionCheck(-0.5f, 0.0f) == false)
        {
            if (enemyDir < 0)
            {
                enemyDir = -enemyDir;
                helper.FlipObject(false);
                anim.SetBool("move", true);
            }


        }
    }

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    void TakeDamage(float damage)
    {
        print(damage);
        health = health - 5;
        print(health);
        
        


    }

    void EnemyDie()
    {
        if (health == 0)
        {
            anim.SetBool("die", true);
        }
    }
    
    
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    

    public void EnemyIsAttacking()
    {
        isAttacking = true;
        waitDelay = 3;
        

    }

    /*public void EnemyAttackEnd()
    {
        anim.SetBool("attack", false);
        RaycastHit2D hit;
        Vector3 direction = Vector2.right;
        Vector3 origin = new Vector2(transform.position.x, transform.position.y + 0.5f);


        hit = Physics2D.Raycast(origin, direction, hitRange * 1, playerLayerMask);
        Debug.DrawRay(origin, direction * hitRange * 3, Color.red);

        print("check for player hit");
        if (hit)
        {
            if (hit.transform.gameObject.tag == "Player")
            {
                print("enemy hit");
                hit.transform.gameObject.SendMessage("TakeDamage", 5);
            }


        }







    }*/


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            
            playerInsideTriggerArea = true;
            print("trigger attack");
            anim.SetBool("attack", true);
            isAttacking = false;        
            

           


        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (isAttacking == true)
            {
                //do damage to player
                print("do damage");
                playerHealth = playerHealth - 4;
                print(playerHealth);
                isAttacking = false;



            }

            if (isAttacking == false)
            {
                anim.SetBool("move", true);
            }

        }



    }


    
}