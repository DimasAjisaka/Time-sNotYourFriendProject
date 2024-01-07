using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    private PlayerController playerController;
    [SerializeField] private int indexNextLevel = 1;
    [SerializeField] private string nextLevelUnlockString;
    private void Awake()
    {
        playerController = FindAnyObjectByType<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            if (playerController.isKeyPicked == true)
            {
                OpenDoor();
            }
            else
            {
                Debug.Log("Door Locked");
            }
        }
    }

    private void OpenDoor()
    {
        SceneManager.LoadScene(indexNextLevel);
        NextLevelUnlock();
    }

    private void NextLevelUnlock() {
        PlayerPrefs.SetInt(nextLevelUnlockString, 1); // Unlock level 2
        PlayerPrefs.Save();
    }
}
