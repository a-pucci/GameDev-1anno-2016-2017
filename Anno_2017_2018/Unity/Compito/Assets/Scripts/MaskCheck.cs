using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskCheck : MonoBehaviour
{
    Ray ray;
    RaycastHit2D hit;
    Texture2D maskTexture;
    Color pointColor;
    Color black = new Color(0, 0, 0);

    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        hit = Physics2D.Raycast(ray.origin, ray.direction);

        if (hit.collider != null && hit.collider.tag == "Mask")
        {
            maskTexture = hit.collider.gameObject.GetComponent<SpriteRenderer>().sprite.texture;
            pointColor = maskTexture.GetPixel((int)Input.mousePosition.x, (int)Input.mousePosition.y);
            if(pointColor == black)
            {
                Debug.Log(hit.collider.name);
            }
        }
    }
}
