using System.Collections;
using UnityEngine;

public class EnemyGridPatrol : MonoBehaviour
{
    public Vector3[] patrolPoints;
    public float moveSpeed = 3f;
    public float gridCellSize = 1f;

    private int currentPatrolIndex = 0;
    private Vector3 targetPosition;

    public PlayerController playerController;

    void Start()
    {
        if (patrolPoints.Length < 2)
        {
            Debug.LogError("Error: Patrol Points should be at least 2 for enemy patrol.");
            enabled = false;
            return;
        }

        transform.position = GetNearestPatrolPoint();
        targetPosition = GetNextPatrolPoint();

        if (playerController == null)
        {
            Debug.LogError("Error: PlayerMovement script reference is not set.");
            enabled = false;
            return;
        }
    }

    void Update()
    {
        if (playerController.isMoving)
        {
            MoveOneGridCell();
        }
    }

    void MoveOneGridCell()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * gridCellSize * Time.deltaTime);

 
        if (Vector3.Distance(transform.position, targetPosition) <= 0.1f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;

            targetPosition = GetNextPatrolPoint();

            StartCoroutine(WaitBeforeNextPatrol(1f));
        }
    }

    IEnumerator WaitBeforeNextPatrol(float delay)
    {
        yield return new WaitForSeconds(delay);
    }

    Vector3 GetNearestPatrolPoint()
    {
        return patrolPoints[currentPatrolIndex];
    }

    Vector3 GetNextPatrolPoint()
    {
        return patrolPoints[(currentPatrolIndex + 1) % patrolPoints.Length];
    }
}
