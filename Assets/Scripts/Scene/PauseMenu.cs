using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    public static bool GameIsPaused = false;

    public GameObject PausePanel;
    public GameObject SettingPanel;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            AudioManager.instance.PlaySFX("Click");
            if (GameIsPaused) {
                Resume();
                SettingPanel.SetActive(false);
            } else {
                Pause();
            }
        }
    }

    public void Resume() {
        PausePanel.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause() {
        PausePanel.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void OpenSetting() {
        SettingPanel.SetActive(true);
    }

    public void CloseSetting() {
        SettingPanel.SetActive(false);
    }
}
