using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelIndicator : MonoBehaviour
{
    public string levelName;
    private TextMeshProUGUI levelText;

    private void Awake()
    {
        levelText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        UpdateLevelText();
    }

    private void UpdateLevelText()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        levelText.text = "Floor " + currentSceneIndex + " : " + levelName;
    }
}
