using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private bool isMoving = false;
    private Vector3 origPos, targetPos;
    private float timeToMove = 0.2f;
    public LayerMask obstacleLayer; // Layer for obstacles

    private void Start() {
    }

    private void Update() {
        GetPlayerInput();
    }

    void GetPlayerInput() {
        if(GameManager.instance.state == GameState.START) {
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
        RaycastHit2D hit = Physics2D.Raycast(origPos, direction, 1f, obstacleLayer);
        if (hit.collider != null) {
            // There is an obstacle, don't move
            isMoving = false;
            yield break;
        }

        while (elapsedTime < timeToMove) {
            transform.position = Vector3.Lerp(origPos, targetPos, (elapsedTime / timeToMove));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;

        isMoving = false;
    }
}
