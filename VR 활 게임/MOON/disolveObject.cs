using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disolveObject : MonoBehaviour
{
    [SerializeField] private float noiseStrength = 0.25f;
    [SerializeField] private float objectHeight = 1.0f;

    private Material material;
    private float height_two = -999f;
    private bool iscreate = false;

    private void Awake()
    {
        material = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        if (height_two <= (9.95f + transform.position.y) && !iscreate ) 
        {
            var time = Time.time * Mathf.PI * 0.2f;

            float height = transform.position.y;

            height += Mathf.Sin(time) * objectHeight * 10;
            height_two = height;
            //Debug.Log(height);

            SetHeight(height);
        }
        else
        {
            iscreate = true;
        }
    }

    private void SetHeight(float height)
    {
        material.SetFloat("_CutoffHeight", height);
        material.SetFloat("_NoiseStrength", noiseStrength);
    }
}
