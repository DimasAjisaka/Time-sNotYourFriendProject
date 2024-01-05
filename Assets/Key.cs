using UnityEngine;

public class Key : MonoBehaviour
{
    public GameObject keyPrefabs;
    private PlayerController playerController;

    private void Awake()
    {
        playerController = FindAnyObjectByType<PlayerController>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            playerController.isKeyPicked = true;
            Debug.Log("Key Picked");
            Destroy(gameObject);
        }
    }
}
