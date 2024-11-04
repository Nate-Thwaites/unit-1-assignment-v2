using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class respawnscript : MonoBehaviour
{
    // Start is called before the first frame update
    public bool playerRespawn;
    

    private void Start()
    {
        playerRespawn = false;
        PlayerRespawn();

    }

    void PlayerRespawn()
    {
        if (Input.GetKey("space"))
        {
            SceneManager.LoadScene("Game");
        }
    }











    
}
