using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    /// <summary>
    /// 화살이 날아갈 방향
    /// </summary>
    private Vector3 _direction = Vector3.zero;

    public float Speed;

    public void Init(Vector3 positon, Vector3 direction)
    {
        this.transform.position = positon;
        this.transform.forward = direction;
        _direction = direction;
    }

    private void Awake()
    {
        Destroy(this.gameObject, 10);
    }

    void Update()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * Speed;
        //transform.position += transform.forward * Speed * Time.deltaTime;
    }
    
    
    
    
}
