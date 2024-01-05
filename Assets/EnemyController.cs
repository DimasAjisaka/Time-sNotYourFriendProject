using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private bool _isAlive;

    public void TakeDamage(bool isAlive) {
        _isAlive = isAlive;
        if (_isAlive == false) {
            Destroy(gameObject);
        }
    }
}
