using UnityEngine;

public class RobotExplorer : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 3f;                       
    public float obstacleDetectDistance = 1f;      
    public float turnAngle = 90f;                  

    [Header("Goal Settings")]
    public Transform treasureChest;                
    public float reachThreshold = 0.5f;           

    private Vector3 moveDirection;

    void Start()
    {
        moveDirection = transform.forward;
    }

    void Update()
    {
        MoveStraight();
        AvoidObstacles();
        CheckForGoal();
    }

    void MoveStraight()
    {
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
    }

    void AvoidObstacles()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, moveDirection, out hit, obstacleDetectDistance))
        {
            if (hit.collider.CompareTag("Obstacle"))
            {
                moveDirection = Quaternion.Euler(0, turnAngle, 0) * moveDirection;
                transform.rotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            }
        }
    }

    void CheckForGoal()
    {
        if (treasureChest == null) return;

        float dist = Vector3.Distance(transform.position, treasureChest.position);
        if (dist <= reachThreshold)
        {
            Debug.Log("Treasure reached!");
            enabled = false;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, moveDirection.normalized * obstacleDetectDistance);
    }
}
