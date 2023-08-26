using System;
using UnityEngine;

public class RotateObj : MonoBehaviour
{
    public bool IsRotateRight;
    public float Speed;

    private void Update()
    {
        this.transform.Rotate(IsRotateRight ? Vector3.back : Vector3.forward, Speed * Time.deltaTime);
    }
}