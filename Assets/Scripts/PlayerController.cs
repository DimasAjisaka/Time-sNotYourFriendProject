using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public bool isMoving = false;
    private Vector3 origPos, targetPos;
    [SerializeField] private float timeToMove = 0.2f;
    [SerializeField] private float pushBoxTime = 0.2f;
    public LayerMask envLayer; // Layer for obstacles
    public bool isKeyPicked = false;

    private Collider2D currentEnemy;
    private void Start() {
        AudioManager.instance.PlayBGM("Battle");
    }

    private void Update() {
        GetPlayerInput();
    }

    void GetPlayerInput() {
        if (GameManager.instance.state == GameState.START) {
            if (Input.GetKeyDown(KeyCode.W) && !isMoving)
                StartCoroutine(MovePlayer(Vector3.up));
            if (Input.GetKeyDown(KeyCode.A) && !isMoving)
                StartCoroutine(MovePlayer(Vector3.left));
            if (Input.GetKeyDown(KeyCode.S) && !isMoving)
                StartCoroutine(MovePlayer(Vector3.down));
            if (Input.GetKeyDown(KeyCode.D) && !isMoving)
                StartCoroutine(MovePlayer(Vector3.right));
        }
    }

    private IEnumerator MovePlayer(Vector3 direction) {
        isMoving = true;

        float elapsedTime = 0;

        origPos = transform.position;
        targetPos = origPos + direction;

        // Raycast to check for obstacles
        RaycastHit2D hit = Physics2D.Raycast(origPos, direction, 1f, envLayer);
        if (hit.collider != null) {
            // There is an environment, don't move
            HandleObstacle(hit.collider.gameObject, direction);
            yield break;
        }
        AudioManager.instance.PlayEnviFeedback("PlayerStep");
        while (elapsedTime < timeToMove) {
            transform.position = Vector3.Lerp(origPos, targetPos, (elapsedTime / timeToMove));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPos;
        isMoving = false;
    }

    private void HandleObstacle(GameObject obstacle, Vector3 direction) {
        if (obstacle.CompareTag("Wall")) {
            // The player cannot move through the wall
            isMoving = false;
            Debug.Log("Cannot move through the wall!");
        }
        if (obstacle.CompareTag("Enemy")) {
            // The player cannot move through the wall
            isMoving = false;
            Debug.Log("Cannot move through the enemy!");
        } else if (obstacle.CompareTag("Obstacle")) {
            // Push the obstacle if colliding with an obstacle
            StartCoroutine(PushObstacle(obstacle, direction));
        }
    }

    private IEnumerator PushObstacle(GameObject obstacle, Vector3 direction) {
        Debug.Log(direction);
        AudioManager.instance.PlayEnviFeedback("Kick");
        Vector3 originalPosition = obstacle.transform.position;
        Vector3 targetPosition = originalPosition + direction; // Adjust direction as needed
        float pushTime = pushBoxTime; // Adjust as needed
        float pushElapsedTime = 0;
        obstacle.GetComponent<BoxCollider2D>().enabled = false;
        // Raycast to check for obstacles in the push direction
        RaycastHit2D hit = Physics2D.Raycast(originalPosition, direction, 1f, envLayer);

        obstacle.GetComponent<BoxCollider2D>().enabled = true;
        if (hit.collider != null) {
            Debug.Log(hit.collider.gameObject.name);
            // There is an obstacle in the push direction, stop pushing
            isMoving = false;
            yield break;
        }

        while (pushElapsedTime < pushTime) {
            obstacle.transform.position = Vector3.Lerp(originalPosition, targetPosition, (pushElapsedTime / pushTime));
            pushElapsedTime += Time.deltaTime;
            yield return null;
        }

        obstacle.transform.position = targetPosition;
        isMoving = false;
    }



    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Enemy")) {
            currentEnemy = collision;
            GameManager.instance.UpdateGameState(GameState.BATTLE);
        } else if (collision.gameObject.CompareTag("Collectable")) {
            TimeManager.instance.timer += collision.gameObject.GetComponent<Collectable>().collectableValue;
            Destroy(collision.gameObject);
        }
    }

    public void KillEnemy() {
        Destroy(currentEnemy.gameObject);
    }
}
