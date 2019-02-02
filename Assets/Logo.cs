using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Logo : MonoBehaviour
{
    [SerializeField] private Sprite[] logoSprites;
    private SpriteRenderer spriteRenderer;
    private Delay logoDelay;
    private int spriteIndex = 0;

    private void Start()
    {
        logoDelay = new Delay(1f);
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(logoDelay.CallDelay())
        {
            spriteRenderer.sprite = logoSprites[spriteIndex++];
        }
    }
}
