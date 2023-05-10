using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    PlayerController playerController;
    public bool isCrouchObstacle = false;

    void Start()
    {
        playerController = GameObject.FindObjectOfType<PlayerController>();
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.name == "Player")
        {
            playerController.Die();
        }
    }
    private void OnCollisionEnter(Collision collision) 
    {
        if(collision.gameObject.name == "Player")
        {
            playerController.Die();
        }
    }
}
