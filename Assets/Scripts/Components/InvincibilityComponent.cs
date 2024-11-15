using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(HitboxComponent))]
public class InvincibilityComponent : MonoBehaviour
{
    [SerializeField] private int blinkingCount = 7;
    [SerializeField] private float blinkInterval = 0.1f;
    [SerializeField] private Material blinkMaterial;

    [SerializeField]
    private SpriteRenderer spriteRenderer;
    private Material originalMaterial;
    public bool isInvincible = false;

    void Awake()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        originalMaterial = spriteRenderer.material;
    }

    private Coroutine flashRoutine;

    public void Flash()
    {
        if (isInvincible)
            return;

        if (flashRoutine != null)
        {
            StopCoroutine(flashRoutine);
        }

        flashRoutine = StartCoroutine(FlashRoutine());
    }

    IEnumerator FlashRoutine()
    {
        isInvincible = true;

        for (int i = 0; i < blinkingCount; i++)
        {
            spriteRenderer.material = blinkMaterial;
            yield return new WaitForSeconds(blinkInterval);
            
            spriteRenderer.material = originalMaterial;
            yield return new WaitForSeconds(blinkInterval);
        }
        
        isInvincible = false;
        flashRoutine = null;
    }

}
