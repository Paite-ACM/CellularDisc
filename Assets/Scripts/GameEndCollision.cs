using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEndCollision : MonoBehaviour
{
    public GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Sets gameHasEnded to true
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            gameManager.gameHasEnded = true;
        }
        
    }
}
