using UnityEngine;

public class Key : MonoBehaviour
{
    private PlayerController playerController;
    private Animator doorAni;
    private void Awake()
    {
        playerController = FindAnyObjectByType<PlayerController>();
        doorAni = FindAnyObjectByType<Door>().GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            doorAni.SetBool("isOpen", true);
            playerController.isKeyPicked = true;
            AudioManager.instance.PlayUnlockLevel("GetKey");
            Debug.Log("Key Picked");
            Destroy(gameObject);
        }
    }
}