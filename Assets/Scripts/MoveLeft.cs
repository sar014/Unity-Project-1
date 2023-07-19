using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private float speed = 27;

    //Reference to PlayerController Script
    private PlayerController playercontrollerScript;

    private float LeftBound = -15;
    // Start is called before the first frame update
    void Start()
    {
        //Finding the Player GameObject and getting its PlayerController Component (Script)
        playercontrollerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Using the reference created above use the gameOver var from PlaerController Script
        if(playercontrollerScript.gameOver == false)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }

        //Destroying the clones of the obstacle created 
        if(transform.position.x < LeftBound && CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
