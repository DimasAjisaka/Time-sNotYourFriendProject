using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] private GameObject gateToOpen;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite pushedSprite;
    private Sprite originalSprite;
    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalSprite = spriteRenderer.sprite;
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        spriteRenderer.sprite = pushedSprite;
        gateToOpen.GetComponent<Animator>().SetBool("isActive", true);
        AudioManager.instance.PlayEnviFeedback("Plate");
    }

    private void OnTriggerExit2D(Collider2D collision) {
        spriteRenderer.sprite = originalSprite;
        gateToOpen.GetComponent<Animator>().SetBool("isActive", false);
    }
}
