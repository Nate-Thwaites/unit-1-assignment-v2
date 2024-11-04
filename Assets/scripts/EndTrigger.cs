using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public bool levelDone;
    public GameObject triggerArea;

    private void Start()
    {
        levelDone = false;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {



            print("level end");
            levelDone = true;
        }

        if (levelDone == true)
        {
            SceneManager.LoadScene("End Screen");


            if (Input.GetKeyDown("space"))
            {
                SceneManager.LoadScene("Game");
                levelDone = false;
                print("restart");
            }


        }










    }
}

