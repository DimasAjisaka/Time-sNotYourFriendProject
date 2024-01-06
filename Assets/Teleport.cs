using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] private Transform destinationPortal;
    private GameObject player;
    public static bool isTeleporting = false;


    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player") && isTeleporting == false) {
            isTeleporting = true;
            StartCoroutine(teleport());
        }
    }

    IEnumerator teleport() {
        yield return new WaitForSeconds(0.25f);
        player.transform.position = destinationPortal.transform.position;
    }
}
