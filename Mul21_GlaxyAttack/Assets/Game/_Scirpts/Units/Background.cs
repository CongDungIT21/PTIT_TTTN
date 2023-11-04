using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [Range(-1f, 1f)]
    [SerializeField]
    float speed = 0.5f;
    [SerializeField]
    Material mat_bg;

    private float offset;

    private void LateUpdate()
    {
        offset += (Time.deltaTime * speed) / 10f;
        mat_bg.SetTextureOffset(mat_bg.mainTexture.name, new Vector2(0, offset));
    }
}
