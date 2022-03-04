using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{

    [SerializeField] private int playerHealth = 100;
    [SerializeField] private float playerSpeed = 3f;
    [SerializeField] private string playerName = "Juan Carlos";
    private float playerAxieX;
    private CharacterController ccPlayer;
    [SerializeField] private GameObject bulletPrefab;
    private bool canJump;
    [SerializeField] private float speedJump = 1f;
    private Vector3 velocidadDeSalto;
    [SerializeField] private float gravity = -5f;
    [SerializeField] private float altura = 2f;
    
    [SerializeField] private Animator playerAnimator;
    void Start()
    {
        ccPlayer = GetComponent<CharacterController>();
        playerAnimator.SetBool("isRun", true);
        
    }

    // Update is called once per frame
    void Update()
    {
        MovementController();
        RotatePlayer();
        JumpPlayer();
        Gravity();
        Debug.Log(ccPlayer.isGrounded);
    }
    
    public void MovePlayer(Vector3 direction)
    {
        ccPlayer.Move(playerSpeed * Time.deltaTime * transform.TransformDirection(direction));
    }
    
    private void MovementController()
    {
        if (Input.GetKey(KeyCode.W))
        {
            MovePlayer(Vector3.right);
            playerAnimator.SetBool("isRun", true);            
        }
        if (Input.GetKey(KeyCode.S))
        {
            MovePlayer(Vector3.left);
            playerAnimator.SetBool("isWalkBack", true);
        }
        if (Input.GetKey(KeyCode.A))
        {
            MovePlayer(Vector3.forward);
        }
        if (Input.GetKey(KeyCode.D))
        {
            MovePlayer(Vector3.back);
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            playerAnimator.SetBool("isRun", false);
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            playerAnimator.SetBool("isWalkBack", false);
        }
        
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            playerAnimator.SetBool("isShoot", true);
        }
    
            
    }

    void RotatePlayer()
    {
        playerAxieX += Input.GetAxis("Mouse X");
        Quaternion angle = Quaternion.Euler(0f, playerAxieX * playerSpeed, 0f);
        transform.localRotation = angle;
    }
    private void JumpPlayer()
    {
        if (Input.GetKeyDown(KeyCode.Space) && ccPlayer.isGrounded)
        {
            //transform.Translate(Vector3.up * speedJump);
            velocidadDeSalto.y = Mathf.Sqrt(-altura * gravity);
        }
    }
    private void Gravity()
    {
        ccPlayer.Move(velocidadDeSalto * Time.deltaTime);
        velocidadDeSalto.y += gravity * Time.deltaTime;
    }

    public void SetJumpStatus(bool status){
        canJump = status;
        //playerAnimator.SetBool("isJump", !status);
    }
}
