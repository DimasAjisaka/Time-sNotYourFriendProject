using UnityEngine;

public class Key : MonoBehaviour
{
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
            AudioManager.instance.PlayUnlockLevel("GetKey");
            Debug.Log("Key Picked");
            Destroy(gameObject);
        }
    }
}