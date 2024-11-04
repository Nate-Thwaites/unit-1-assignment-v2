using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTriggerScript : MonoBehaviour
{
    // Start is called before the first frame update

    public bool playerInsideTriggerArea;

    private void Start()
    {
        playerInsideTriggerArea = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInsideTriggerArea = true;
            print("true");
        }
    }
}
