using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private TextMeshProUGUI _timerText;

    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject restartPanel;
    [SerializeField] private GameObject battlePanel;
    [SerializeField] private RectTransform battlePanelRect;

    [SerializeField] private Button attackButton;
    [SerializeField] private Button escapeButton;

    private void Awake() {
        instance = this;
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
    }

    private void OnDestroy() {
        GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
    }

    private void Update() {
        _timerText.SetText(TimeManager.instance.timer.ToString("0"));
    }

    private void GameManagerOnGameStateChanged(GameState state) {
        startButton.SetActive(state == GameState.WAIT);
        _timerText.gameObject.SetActive(state == GameState.START || state == GameState.BATTLE);
        restartPanel.SetActive(state == GameState.GAMEOVER);


        if(state == GameState.BATTLE) {
            GameManager.instance.ZoomInCamera();
            battlePanel.SetActive(true);
            battlePanelRect.DOAnchorPosY(0f, 1f)
                           .From(new Vector2(0f, -1100f))
                           .SetEase(Ease.OutQuint);
        }
    }

    public void BattlePanelExit() {
        battlePanelRect.DOAnchorPosY(-1100f, 2f)
                       .From(new Vector2(0f, 0f))
                       .SetEase(Ease.InQuint)
                       .OnComplete(() => battlePanel.SetActive(false));
    }


    public void StartButtonClicked() {
        GameManager.instance.UpdateGameState(GameState.START);
    }

    public void RestartButtonClicked() {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void EnableDisableBattleButton(bool isRolling) {
        attackButton.interactable = isRolling == false;
        escapeButton.interactable = isRolling == false;
    }
}
