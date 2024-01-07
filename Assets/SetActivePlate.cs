using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActivePlate : MonoBehaviour
{
    [SerializeField] private GameObject gameObjectActivate;

    private void OnTriggerEnter2D(Collider2D collision) {
        gameObjectActivate.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision) {
        gameObjectActivate.SetActive(false);
    }
}
