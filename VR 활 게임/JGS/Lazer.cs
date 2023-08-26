using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour
{

    void Start()
    {
        Destroy(this.gameObject, 3f);
    }

    void Update()
    {
        this.transform.position -= this.transform.up;
        if(this.transform.localScale.y < 10)
        {
            this.transform.localScale += new Vector3(0, 1, 0);
        }
    }
}
