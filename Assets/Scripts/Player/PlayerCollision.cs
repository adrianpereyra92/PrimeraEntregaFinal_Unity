using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{ 
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision other)
    {
        /*Debug.Log(name + " COLISION CON " + other.gameObject.name);
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("GAME OVER");
        }
        */
        if (other.gameObject.CompareTag("Ground"))
        {
            gameObject.GetComponent<PlayerCharacter>().SetJumpStatus(true);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        Debug.Log(name + " EXIT COLISION CON " + other.gameObject.name);
        if (other.gameObject.CompareTag("Ground"))
        {
            gameObject.GetComponent<PlayerCharacter>().SetJumpStatus(false);
        }
    }
    /*
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(name + " Tigger con" + other.gameObject.name);
        Destroy(other.gameObject);
    }
    */
}
