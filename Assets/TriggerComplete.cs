using System.Collections;
using UnityEngine;
using TMPro;

public class TriggerComplete : MonoBehaviour
{
    public TextMeshProUGUI finishText;
    public string isiText;
    public float displayDuration = 3f;
    public float letterDelay = 0.1f;

    private void Awake()
    {
        if (finishText == null)
        {
            finishText = GetComponentInChildren<TextMeshProUGUI>();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(ShowText());
        }
    }

    IEnumerator ShowText()
    {
        if (finishText != null)
        {
            finishText.text = "";

            finishText.gameObject.SetActive(true);

            foreach (char letter in isiText)
            {
                finishText.text += letter;
                yield return new WaitForSeconds(letterDelay);
            }

            yield return new WaitForSeconds(displayDuration - (letterDelay * "YourCompletionText".Length));

            finishText.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("Finish Text is null. Please check references.");
        }
    }
}
