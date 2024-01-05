using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;
    public float timer;


    private void Awake() {
        instance = this;
    }
    private void Update() {
        if (GameManager.instance.state == GameState.START || GameManager.instance.state == GameState.BATTLE) {
            timer -= Time.deltaTime;
            if(timer < 0) {
                GameManager.instance.UpdateGameState(GameState.GAMEOVER);
                AudioManager.instance.PlaySFX("PlayerDeath");
            }
        }
    }

}
