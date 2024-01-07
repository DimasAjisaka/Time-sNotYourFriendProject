using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState {
    WAIT, START, BATTLE, GAMEOVER
}
public class GameManager : MonoBehaviour {
    public enum SceneActive { MainMenu, Battle }

    [SerializeField] private GameObject enemyShuffle;
    [SerializeField] private GameObject enemyBatu;
    [SerializeField] private GameObject enemyGunting;
    [SerializeField] private GameObject enemyKertas;



    public static GameManager instance;
    public PlayerController playerController;
    public UIManager uiManager;
    public float diceRollingTime = 1f;

    public float winTime = 10f;
    public float loseTime = 10f;

    public GameState state;
    public static event Action<GameState> OnGameStateChanged;

    [SerializeField] private SceneActive scene;

    private GameObject enemyChoiceContainer;

    private bool isRollingDice = false;

    void Awake() {
        instance = this;
    }

    void Start() {
        UpdateGameState(GameState.WAIT);

        if (scene == SceneActive.MainMenu) {
            AudioManager.instance.bgmSource.Stop();
            AudioManager.instance.PlayBGM("MainMenu");
        } else {
            AudioManager.instance.bgmSource.Stop();
            AudioManager.instance.PlayBGM("Battle");
        }
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
                TimeManager.instance.CameraShakeDamage();

            } else {
                StartCoroutine(RollingDice(0));
                yield break;
            }
            ZoomOutCamera();
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
                TimeManager.instance.CameraShakeDamage();

            } else {
                StartCoroutine(RollingDice(1));
                yield break;
            }
            ZoomOutCamera();
        }
        isRollingDice = false;
        uiManager.EnableDisableBattleButton(isRollingDice);
    }

    public void ZoomInCamera() {
        Camera.main.DOOrthoSize(4.5f, 0.3f);
    }

    public void ZoomOutCamera() {
        Camera.main.DOOrthoSize(5f, 0.3f);
    }

    public void RockPaperScissorsBattle(int choice) {
        //0 = batu, 1 = gunting, 2 = kertas

        var enemyChoice = UnityEngine.Random.Range(0, 3);
        if(choice == 0) {
            if(enemyChoice == 0) {
                enemyShuffle.SetActive(false);
                enemyBatu.SetActive(true);

                playerController.KnockPlayer();
                TimeManager.instance.CameraShakeDamage();

            } else if(enemyChoice == 1) {
                enemyShuffle.SetActive(false);
                enemyGunting.SetActive(true);

                TimeManager.instance.timer += winTime;
                playerController.KillEnemy();
                AudioManager.instance.PlayPlayerVoice("PlayerWin");

            } else if (enemyChoice == 2) {
                enemyShuffle.SetActive(false);
                enemyKertas.SetActive(true);

                TimeManager.instance.timer -= loseTime;
                playerController.KnockPlayer();
                TimeManager.instance.CameraShakeDamage();
                AudioManager.instance.PlayPlayerVoice("PlayerLose");

            }

            UpdateGameState(GameState.START);
            uiManager.BattlePanelExit();
            Invoke("TurnOffGBK", 2f);

        } else if (choice == 1) {
            if (enemyChoice == 0) {
                enemyShuffle.SetActive(false);
                enemyBatu.SetActive(true);

                TimeManager.instance.timer -= loseTime;
                playerController.KnockPlayer();
                TimeManager.instance.CameraShakeDamage();
                AudioManager.instance.PlayPlayerVoice("PlayerLose");

            } else if (enemyChoice == 1) {
                enemyShuffle.SetActive(false);
                enemyGunting.SetActive(true);

                playerController.KnockPlayer();
                TimeManager.instance.CameraShakeDamage();

            } else if (enemyChoice == 2) {
                enemyShuffle.SetActive(false);
                enemyKertas.SetActive(true);

                TimeManager.instance.timer += winTime;
                playerController.KillEnemy();
                AudioManager.instance.PlayPlayerVoice("PlayerWin");

            }


            UpdateGameState(GameState.START);
            uiManager.BattlePanelExit();
            Invoke("TurnOffGBK", 2f);


        }
        else if (choice == 2) {
            if (enemyChoice == 0) {
                enemyShuffle.SetActive(false);
                enemyBatu.SetActive(true);

                TimeManager.instance.timer += winTime;
                playerController.KillEnemy();
                AudioManager.instance.PlayPlayerVoice("PlayerWin");

            } else if (enemyChoice == 1) {
                enemyShuffle.SetActive(false);
                enemyGunting.SetActive(true);

                TimeManager.instance.timer -= loseTime;
                playerController.KnockPlayer();
                TimeManager.instance.CameraShakeDamage();
                AudioManager.instance.PlayPlayerVoice("PlayerLose");

            } else if (enemyChoice == 2) {
                enemyShuffle.SetActive(false);
                enemyKertas.SetActive(true);

                playerController.KnockPlayer();
                TimeManager.instance.CameraShakeDamage();

            }
            UpdateGameState(GameState.START);
            uiManager.BattlePanelExit();
            Invoke("TurnOffGBK", 2f);
        }
    }

    private void TurnOffGBK() {
        enemyShuffle.SetActive(true);

        enemyBatu.SetActive(false);
        enemyGunting.SetActive(false);
        enemyKertas.SetActive(false);

    }
}


