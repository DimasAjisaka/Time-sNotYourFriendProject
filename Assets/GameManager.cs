using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState {
    WAIT, START, BATTLE, GAMEOVER
}
public class GameManager : MonoBehaviour {
    public static GameManager instance;
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
                break;

            case GameState.GAMEOVER:
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnGameStateChanged?.Invoke(newState);
    }
}