using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BackgroundScolling : MonoBehaviour
{
    [SerializeField]
    Transform bg_1;
    [SerializeField]
    Transform bg_2;

    [Range(1f, 10f)]
    public float _Speed = 1.5f;

    private bool _Scolling = false;
    private float ScreenHeight;

    private void Start()
    {
        ScreenHeight = bg_1.GetComponent<SpriteRenderer>().bounds.size.y;
        _Scolling = true;
    }
    private void LateUpdate()
    {
        if(_Scolling)
        {
            Vector3 offsetY = new Vector3(0, _Speed * Time.deltaTime, 0);
            bg_1.position -= offsetY;
            bg_2.position -= offsetY;

            if (bg_1.position.y < -ScreenHeight)
            {
                bg_1.position += new Vector3(0, 2f * ScreenHeight, 0);
            }

            if (bg_2.position.y < -ScreenHeight)
            {
                bg_2.position += new Vector3(0, 2f * ScreenHeight, 0);
            }
        }
    }
}
