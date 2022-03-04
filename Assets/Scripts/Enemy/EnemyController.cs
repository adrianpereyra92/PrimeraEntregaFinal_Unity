using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float speedEnemy = 3f;
    [SerializeField] private float minimumDistance = 1f;
    [SerializeField] private bool goBack = false;
    [SerializeField] private float rotationSpeed = 3f;
    [SerializeField] private int currentIndex = 0;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float minimumDistanceToPlayer = 1f;
    [SerializeField] private float minimimDistanceToNotAtackPlayer = 30f;
    [SerializeField] private SelectMovement movement;

    void Start()
    {
        
    }

    void Update()
    {
       SetEnemyMovement();
    }
    
    private void WaypointsMovement()
    {
        Vector3 deltaVector = waypoints[currentIndex].position - transform.position;
        Vector3 direction = deltaVector.normalized;

        transform.forward = Vector3.Lerp(transform.forward, direction, rotationSpeed * Time.deltaTime);
        transform.position += transform.forward * speedEnemy * Time.deltaTime;

        //transform.LookAt(waypoints[currentIndex]);

        if (deltaVector.magnitude <= minimumDistance)
        {
            if (currentIndex >= waypoints.Length - 1)
            {
                goBack = true;
            }
            else if (currentIndex <= 0)
            {
                goBack = false;
            }

            if (goBack)
            {
                currentIndex--;
            }
            else
            {
                currentIndex++;
            }
        }
    }

    private void MoveToPlayer()
    {
        Vector3 direccion = (playerTransform.position - transform.position);
        if (direccion.magnitude > 2)
        {
            transform.position += speedEnemy * direccion.normalized * Time.deltaTime;
        }
    }
    private enum SelectMovement
    {
        Waypoint,
        FollowPlayer,
    }

    private void SwitchMovement(SelectMovement movement)
    {
        switch (movement)
        {
            case SelectMovement.Waypoint:
            WaypointsMovement();
            break;

            case SelectMovement.FollowPlayer:
            MoveToPlayer();
            break;

            default:
            Debug.Log("No es un caso posible");
            break;
        }

        
    }
    private void SetEnemyMovement()
    {
        if ((playerTransform.position - transform.position).magnitude <= minimimDistanceToNotAtackPlayer)
        {
            SwitchMovement(SelectMovement.FollowPlayer);
        }
        if ((playerTransform.position - transform.position).magnitude >= minimimDistanceToNotAtackPlayer)
        {
            SwitchMovement(SelectMovement.Waypoint);
        }
    }
    
}

