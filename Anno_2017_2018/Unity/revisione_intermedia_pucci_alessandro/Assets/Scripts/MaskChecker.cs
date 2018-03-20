using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskChecker : MonoBehaviour
{
    public float fadeTime;

    public SpriteRenderer frontRenderer;
    public SpriteRenderer maskRenderer;

    Texture2D maskTexture;
    Color pointColor;
    Color black = new Color(0, 0, 0);

    Color visible = new Color(1, 1, 1, 1);
    Color invisible = new Color(1, 1, 1, 0);

    Color initialColor;

    bool startFadeIn = false;
    bool startFadeOut = false;

    float currentTime = 0;

    private void Start()
    {
        initialColor = frontRenderer.color;
    }

    void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        bool mouseInMask = maskRenderer.bounds.Contains(mousePosition);

        if(mouseInMask)
        {
            if(startFadeIn == true)
            {
                initialColor = frontRenderer.color;
                currentTime = 0;
                startFadeIn = false;
            }

            maskTexture = maskRenderer.sprite.texture;
            pointColor = maskTexture.GetPixel((int)Input.mousePosition.x, (int)Input.mousePosition.y);

            if (pointColor == black)
            {
                if (startFadeOut == true)
                {
                    initialColor = frontRenderer.color;
                    currentTime = 0;
                    startFadeOut = false;
                }
                Fade(invisible);
            }
        }
        else
        {
            Fade(visible);
        }
    }

    private void Fade(Color endColor)
    {
        frontRenderer.color = Color.Lerp(initialColor, endColor, currentTime);

        if(currentTime < 1)
        {
            currentTime += Time.deltaTime / fadeTime;
        }

        if(frontRenderer.color == endColor)
        {
            startFadeIn = true;
            startFadeOut = true;
        }
    }
}
