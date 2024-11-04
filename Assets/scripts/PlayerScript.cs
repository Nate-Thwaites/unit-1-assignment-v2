using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class PlayerScript : MonoBehaviour
{


    Rigidbody2D rb;
    Animator anim;
    
    bool isGrounded;
    bool isJumping;

    public float playerHealth = 10;

    public LayerMask enemyLayerMask;
    public LayerMask playerLayerMask;

    public GameObject triggerArea;

    private int hitRange = 1;
    

    //bool isDead;

    bool isMoving;

    //bool changeScene;

    HelperScript helper;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        helper = gameObject.AddComponent<HelperScript>();
        

        isGrounded = true;
        isJumping = false;
        //isDead = false;
        //changeScene = false;
        isMoving = false;
    }

    // Update is called once per frame
    void Update()
    {
        MoveSprite();
        SpriteJump();
        SpriteLand();
        DoGroundCheck();
        SpriteAttack();
        //PlayerDead();
        //PlayerDamage();
        
        

        


        int yMovement = (int)Input.GetAxisRaw("");
        if (yMovement == 1)
        {
            SpriteJump();
        }
    }
    
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
   
    void MoveSprite()
    {


        if (anim.GetBool("attack") == true)
        {
            print("movesprite says attack is true");
            rb.linearVelocity = Vector2.zero;
            return;
           

        }

        if (anim.GetBool("attack") == false)
        {

            anim.SetBool("run", false);



            //moving left
            if (Input.GetKey("left") == true) //detects key pressesd
            {
                rb.linearVelocity = new Vector2(-6f, rb.linearVelocity.y); //speed of movement, the minus means left
                anim.SetBool("run", true); //calls animation
                helper.FlipObject(true);
                isMoving = true;

            }

            if (Input.GetKey("right") == true)
            {

                rb.linearVelocity = new Vector2(6f, rb.linearVelocity.y);
                anim.SetBool("run", true);
                helper.FlipObject(false); // x axis doesn't flip
                isMoving = true;
            }

          
        }

        if (Input.GetKey("left") != true && Input.GetKey("right") != true)
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            anim.SetBool("run", false);

        }

    }

        

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void SpriteJump()
    {
        if (Input.GetKeyDown("space") && (isGrounded == true)) //the next peice of code will only execute if both are true
        {
            rb.AddForce(new Vector3(0, 5, 0), ForceMode2D.Impulse); //sends him upwards, as it is coordinbates, x y z, it sends the sprite up with a force of 5
        }
    }

    void SpriteLand()
    {
        if (isJumping && isGrounded && (rb.linearVelocity.y <= 0)) //checks to see if you're on the ground and jumping, but you velocity is less thatn or equal to 0 
        {
            isJumping = false;
        }
    }

    void DoGroundCheck()
    {
        isGrounded = false;
        anim.SetBool("jump", true);

        if (helper.ExtendedRayCollisionCheck(0.48f, 0.48f) == true)

        {
            isGrounded = true;
            anim.SetBool("jump", false);
        }

        if (helper.ExtendedRayCollisionCheck(-0.48f, 0.48f) == true)
        {
            isGrounded = true;
            anim.SetBool("jump", false);
        }
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void SpriteAttack()
    {

        if (Input.GetKeyDown("m") && (isGrounded == true))
        {
            //print("you attacked");
            anim.SetBool("attack", true);
            
        }

        if (Input.GetKeyDown("m") && (isMoving == true))
        {
            print("you attacked");
            anim.SetBool("attack", true);
            anim.SetBool("run", false);
            isMoving = false;
        }


    }

    public void AttackEnd()
    {
        anim.SetBool("attack", false);
        anim.SetBool("run", false);

        RaycastHit2D hit;
        Vector3 direction = Vector2.right;
        Vector3 origin = new Vector2(transform.position.x, transform.position.y + 0.5f);


        hit = Physics2D.Raycast(origin, direction, hitRange*1, enemyLayerMask);
        Debug.DrawRay(origin, direction*hitRange*3, Color.red);

        print("check for enemy hit");
        if (hit)
        {
            if (hit.transform.gameObject.tag == "Enemy")
            {
                print("enemy hit");
                hit.transform.gameObject.SendMessage("TakeDamage", 5);
            }


        }







    }



    /*public void PlayerDead()
    {
        



        if (triggerArea.GetComponent<DeathTrigger>().playerDead == true)
        {
            SceneManager.LoadScene(sceneName: "Death Screen");
            print("dead");
            isDead = true;
        }

        if (isDead == true && (Input.GetKey("space")))
        {
            SceneManager.LoadScene(sceneName: "Game");
            isDead = false;
        }*/


        /*if (helper.DeathRayCollisionCheck(0f, 0.48f) == true)
        {
            changeScene = true;
        }


        if (changeScene == true)
        {
            SceneManager.LoadScene(sceneName: "Death Screen");
            print("dead");
            isDead = true;
        }

        
        


        if (Input.GetKey("space") && (isDead == true))
        {
            SceneManager.LoadScene(sceneName: "Game");
            isDead = false;
        }*/

    

    void PlayerDamage(int damage)
    {
        print(damage);
        playerHealth = playerHealth - 4;
        print(playerHealth);

        if (playerHealth <= 0 )
        {
            anim.SetBool("die", true);
            
            print("dead by enemy" );
        }

    }




    
        
        
        
    
                

        

        
    
}
