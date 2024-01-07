using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] private GameObject gateToOpen;

    private void OnTriggerEnter2D(Collider2D collision) {
            gateToOpen.SetActive(false);
        AudioManager.instance.PlayEnviFeedback("Plate");
    }

    private void OnTriggerExit2D(Collider2D collision) {
            gateToOpen.SetActive(true);
    }
}
