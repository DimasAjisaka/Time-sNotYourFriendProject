using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    [SerializeField] float timeDamage = 10f;

    private void OnTriggerEnter2D(Collider2D collision) {
        TimeManager.instance.timer -= timeDamage;
    }
}
