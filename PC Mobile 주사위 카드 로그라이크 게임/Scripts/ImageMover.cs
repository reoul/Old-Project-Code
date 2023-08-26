using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageMover : MonoBehaviour
{
    private float _firstPosY;
    // Start is called before the first frame update
    void Start()
    {
        _firstPosY = transform.localPosition.y;
    }

    void Update()
    {
        transform.localPosition = new Vector3(0, _firstPosY + Mathf.Cos(Time.time * 8) * 4, 0);
    }
}
