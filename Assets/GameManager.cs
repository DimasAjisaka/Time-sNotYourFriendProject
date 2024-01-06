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
    public UIManager uiManager;
    public float diceRollingTime = 1f;

    public GameState state;
    public static event Action<GameState> OnGameStateChanged;

    private bool isRollingDice = false;

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
                AudioManager.instance.PlayPlayerVoice("PlayerAsk");
                break;

            case GameState.GAMEOVER:
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnGameStateChanged?.Invoke(newState);
    }

    public void RollDiceBattle() {
        StartCoroutine(RollingDice(0));
    }

    public void RollDiceEscape() {
        StartCoroutine(RollingDice(1));
    }

    IEnumerator RollingDice(int state) {
        isRollingDice = true;
        uiManager.EnableDisableBattleButton(isRollingDice);
        Debug.Log("coroutinecalled");
        //battle
        if (state == 0) {
            yield return new WaitForSeconds(diceRollingTime);
            int enemyDice = UnityEngine.Random.Range(1, 7);
            Debug.Log("enemy dice = " + enemyDice);

            yield return new WaitForSeconds(diceRollingTime);
            int playerDice = UnityEngine.Random.Range(1, 7);
            Debug.Log("player dice = " + playerDice);

            yield return new WaitForSeconds(0.5f);
            if (playerDice > enemyDice) {
                playerController.KillEnemy();
                UpdateGameState(GameState.START);

                //add player time based on dice
                TimeManager.instance.timer += (playerDice - enemyDice) * 2;
                Debug.Log("+Time = " + (playerDice - enemyDice) * 2);

                AudioManager.instance.PlayPlayerVoice("PlayerWin");
            } else if (playerDice < enemyDice) {
                Debug.Log("playerlos");
                UpdateGameState(GameState.START);
                TimeManager.instance.timer -= (enemyDice - playerDice) * 2;
                Debug.Log("-Time = " + (enemyDice - playerDice) * 2);

                AudioManager.instance.PlayPlayerVoice("PlayerLose");
                playerController.KnockPlayer();
            } else {
                StartCoroutine(RollingDice(0));
                yield break;
            }
        } 
        //escape
        else {
            yield return new WaitForSeconds(diceRollingTime);
            int enemyDice = UnityEngine.Random.Range(1, 7);
            Debug.Log("enemy dice = " + enemyDice);

            yield return new WaitForSeconds(diceRollingTime);
            int playerDice = UnityEngine.Random.Range(1, 7);
            Debug.Log("player dice = " + playerDice);

            yield return new WaitForSeconds(0.5f);
            if (playerDice > enemyDice) {
                playerController.KillEnemy();
                UpdateGameState(GameState.START);

                //add player time based on dice
                TimeManager.instance.timer += (playerDice - enemyDice) * 2;
                Debug.Log("+Time = " + (playerDice - enemyDice) * 2);

                AudioManager.instance.PlayPlayerVoice("PlayerWin");
            } else if (playerDice < enemyDice) {
                Debug.Log("playerlos");
                UpdateGameState(GameState.START);
                TimeManager.instance.timer -= (enemyDice - playerDice) * 2;
                Debug.Log("-Time = " + (enemyDice - playerDice) * 2);

                AudioManager.instance.PlayPlayerVoice("PlayerLose");
                playerController.KnockPlayer();
            } else {
                StartCoroutine(RollingDice(1));
                yield break;
            }
        }
        isRollingDice = false;
        uiManager.EnableDisableBattleButton(isRollingDice);
    }
}


