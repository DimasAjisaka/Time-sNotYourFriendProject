using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {
    public Button[] levelButtons;

    private void Start() {
        // Loop through each level button and add a listener
        for (int i = 0; i < levelButtons.Length; i++) {
            int levelIndex = i + 1; // Levels are usually 1-indexed

            if (PlayerPrefs.GetInt("Level" + levelIndex.ToString()) == 1 || levelIndex == 1) {
                // If the level is unlocked or it's the first level, allow the button to be clicked
                levelButtons[i].interactable = true;

                int level = levelIndex; // Store the level index in a local variable to avoid closure issues

                // Add a listener to load the selected level
                levelButtons[i].onClick.AddListener(() => LoadLevel(level));
            } else {
                // If the level is locked, disable the button
                levelButtons[i].interactable = false;
            }
        }
    }

    void LoadLevel(int levelIndex) {
        // Load the selected level
        SceneManager.LoadScene("Level" + levelIndex.ToString());
    }
}
