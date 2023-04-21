using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public Material bgMaterial;
    public float scrollSpeed;

    private void Update()
    {
        Vector2 direction = Vector2.up;
        bgMaterial.mainTextureOffset += direction * scrollSpeed * Time.deltaTime;
    }
}
