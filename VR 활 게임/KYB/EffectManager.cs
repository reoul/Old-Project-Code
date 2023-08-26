using System;
using UnityEngine;

public class EffectManager : Singleton<EffectManager>
{
    public GameObject ExplosionPrefab;

    public void CreateEffect(Vector3 pos)
    {
        var effectObj = GameObject.Instantiate(ExplosionPrefab, pos, Quaternion.identity);
        Destroy(effectObj, 4);
    }
}