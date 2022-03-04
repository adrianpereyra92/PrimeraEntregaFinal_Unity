using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] int playerHealth = 3;
    [SerializeField] float playerSpeed = 5f;
    [SerializeField] string playerName = "Juan Carlos";
    float playerAxieX;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MovementController();
        RotatePlayer();
    }

    private void MovementController()
    {
        if (Input.GetKey(KeyCode.W))
        {
            MovePlayer(Vector3.forward);
        }
        if (Input.GetKey(KeyCode.S))
        {
            MovePlayer(Vector3.back);
        }
        if (Input.GetKey(KeyCode.A))
        {
            MovePlayer(Vector3.left);
        }
        if (Input.GetKey(KeyCode.D))
        {
            MovePlayer(Vector3.right);
        }
    }
    private void RotatePlayer()
    {
        playerAxieX += Input.GetAxis("Mouse X");
        Quaternion angle = Quaternion.Euler(0f, playerAxieX * playerSpeed, 0f);
        transform.localRotation = angle;
    }
    private void MovePlayer(Vector3 direction)
    {
        transform.Translate(playerSpeed * Time.deltaTime * direction);
    }
}
