using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState {
    WAIT, START, BATTLE, GAMEOVER
}
public class GameManager : MonoBehaviour {
    public static GameManager instance;
    public PlayerController playerController;

    public GameState state;
    public static event Action<GameState> OnGameStateChanged;

    void Awake() {
        instance = this;
    }

    void Start() {
        UpdateGameState(GameState.WAIT);
    }

    public void UpdateGameState(GameState newState) {
        state = newState;

        switch (newState) {
            case GameState.WAIT:
                break;

            case GameState.START:
                break;

            case GameState.BATTLE:
                AudioManager.instance.PlaySFX("PlayerAsk");
                break;

            case GameState.GAMEOVER:
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnGameStateChanged?.Invoke(newState);
    }

    public void RollDiceBattle() {
        int playerDice = UnityEngine.Random.Range(1, 7);
        int enemyDice = UnityEngine.Random.Range(1, 7);

        if(playerDice > enemyDice) {
            playerController.KillEnemy();
            UpdateGameState(GameState.START);

            //add player time based on dice
            TimeManager.instance.timer += (playerDice - enemyDice) * 2;
            Debug.Log("+Time = " + (playerDice - enemyDice) * 2);

            AudioManager.instance.PlaySFX("PlayerWin");
        } else {
            Debug.Log("playerlos");
            UpdateGameState(GameState.START);
            TimeManager.instance.timer -= (enemyDice - playerDice) * 2;
            Debug.Log("-Time = " + (enemyDice - playerDice) * 2);

            AudioManager.instance.PlaySFX("PlayerLose");

        }
    }
}



