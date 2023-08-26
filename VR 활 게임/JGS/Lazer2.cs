using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer2 : MonoBehaviour
{
    private Transform _lazer;


    void Start() 
    {
        Destroy(this.gameObject, 2);
        _lazer = this.transform.GetChild(0);
    }
     
    void Update()
    {
        if (_lazer.localScale.y < 30)
        {
            _lazer.position -= _lazer.transform.up * 0.3f;
            _lazer.localScale += new Vector3(0, 0.3f, 0);
        }
        else
        {
            this.transform.rotation *= Quaternion.Euler(0, 0.5f, 0);
        }
    }


}