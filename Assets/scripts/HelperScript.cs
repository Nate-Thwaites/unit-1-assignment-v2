using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperScript : MonoBehaviour
{

    public LayerMask groundLayerMask;
    public LayerMask deathLayerMask;
    public float r = -7f;
    public GameObject player;
    public GameObject enemy;
    Animator anim;
    Rigidbody2D rb;

    public bool isGrounded;

    

    void Start()
    {
        // set the mask to be "Ground"
        groundLayerMask = LayerMask.GetMask("Ground");
        deathLayerMask = LayerMask.GetMask("death");
        isGrounded = true;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();


    }




    public void FlipObject(bool flip)
    {
        // get the SpriteRenderer component
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();

        if (flip == true)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }
    }


    public bool DoRayCollisionCheck()
    {
        float rayLength = 0.5f; // length of raycast


        //cast a ray downward 
        RaycastHit2D hit;

        hit = Physics2D.Raycast(transform.position, Vector2.down, rayLength, groundLayerMask);

        Color hitColor = Color.red;


        if (hit.collider != null)
        {
            print("Player has collided with Ground layer");
            hitColor = Color.green;
        }
        // draw a debug ray to show ray position
        // You need to enable gizmos in the editor to see these
        Debug.DrawRay(transform.position, Vector2.down * rayLength, hitColor);
        return hit.collider;


    }

    public bool ExtendedRayCollisionCheck(float xoffs, float yoffs)
    {

        float rayLength = 0.5f; // length of raycast
        bool hitSomething = false;

        // convert x and y offset into a Vector3 
        Vector3 offset = new Vector3(xoffs, yoffs, 0);

        //cast a ray downward 
        RaycastHit2D hit;


        hit = Physics2D.Raycast(transform.position + offset, -Vector2.up, rayLength, groundLayerMask);

        Color hitColor = Color.red;


        if (hit.collider != null)
        {
            //print("Player has collided with Ground layer");
            hitColor = Color.green;
            hitSomething = true;
        }
        // draw a debug ray to show ray position
        // You need to enable gizmos in the editor to see these
        Debug.DrawRay(transform.position + offset, -Vector3.up * rayLength, hitColor);

        return hitSomething;

    }

    public bool DeathRayCollisionCheck(float xoffs, float yoffs)
    {

        float rayLength = 0.5f; // length of raycast
        bool hitSomething = false;

        // convert x and y offset into a Vector3 
        Vector3 offset = new Vector3(xoffs, yoffs, 0);

        //cast a ray downward 
        RaycastHit2D hit;


        hit = Physics2D.Raycast(transform.position + offset, -Vector2.up, rayLength, deathLayerMask);

        Color hitColor = Color.red;


        if (hit.collider != null)
        {
            print("Player has collided with death layer");
            hitColor = Color.green;
            hitSomething = true;
        }
        // draw a debug ray to show ray position
        // You need to enable gizmos in the editor to see these
        Debug.DrawRay(transform.position + offset, -Vector3.up * rayLength, hitColor);

        return hitSomething;

    }

   /* public bool EnemyChase()
    {



        bool chasePlayer = false;

        ex = enemy.transform.position.x;
        px = player.transform.position.x;


        

        if (ExtendedRayCollisionCheck(0.5f, 0.4f) && (ExtendedRayCollisionCheck(-0.5f, 0.4f) == true))
        {
            isGrounded = true;
        }

        else
        {
            isGrounded = false;
        }


        ex = enemy.transform.position.x;
        px = player.transform.position.x;

        if (ex > px)
        {
            rb.velocity = new Vector2(r, 0);
            anim.SetBool("walk", true);
            FlipObject(true);
        }

        if (ex < px)
        {
            r = r * -1;
            rb.velocity = new Vector2(r, 0);
        }





        chasePlayer = true;
        return chasePlayer;
    }*/

    
}