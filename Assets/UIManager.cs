using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerText;

    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject restartPanel;

    private void Awake() {
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
    }

    private void OnDestroy() {
        GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
    }

    private void Update() {
        _timerText.SetText(TimeManager.instance.timer.ToString("#.00"));
    }

    private void GameManagerOnGameStateChanged(GameState state) {
        startButton.SetActive(state == GameState.WAIT);

        _timerText.gameObject.SetActive(state == GameState.START);

        restartPanel.SetActive(state == GameState.GAMEOVER);
    }

    public void StartButtonClicked() {
        GameManager.instance.UpdateGameState(GameState.START);
    }

    public void RestartButtonClicked() {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

}
