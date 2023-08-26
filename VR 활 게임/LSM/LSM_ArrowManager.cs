using System;
using UnityEngine;

public class LSM_ArrowManager : Singleton<LSM_ArrowManager>
{
    public GameObject ArrowPrefab;
    public void Shot(Vector3 positon, Vector3 direction)
    {
        var arrow = GameObject.Instantiate(ArrowPrefab);
        arrow.GetComponent<Arrow>().Init(positon, direction);
    }
}
