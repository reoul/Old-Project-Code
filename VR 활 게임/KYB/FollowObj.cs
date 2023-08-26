using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObj : MonoBehaviour
{
    public Transform Obj;
    private void Update()
    {
        this.transform.LookAt(Obj.position);
        transform.localScale = new Vector3(1,1,Vector3.Distance(transform.position, Obj.transform.position) * 1.4f);
        //transform.GetChild(1).localScale.y = 
    }
}
