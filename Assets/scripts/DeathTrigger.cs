using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    
    public GameObject triggerArea;
    Vector2 startPos;


    private void Start()
    {
        
        startPos = transform.position;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Death")
        {


            Die();

        }

        void Die()
        {
            Respawn();
        }

        void Respawn()
        {
            transform.position = startPos;
        }
           
            

            
                
            
        


        

            



            
        
    }
}

