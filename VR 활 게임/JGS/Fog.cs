using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fog : MonoBehaviour
{
    private bool _isGameStart;
    private float _time;
    
    // Use this for initialization
    private void Start ()
    {
        _isGameStart = false;
        
        RenderSettings.fog = true;
        RenderSettings.fogColor = Color.black;
        RenderSettings.fogMode = FogMode.Linear;
        RenderSettings.fogStartDistance = 0;
        RenderSettings.fogEndDistance = 5;

    }

    private void Update () {
        if (_isGameStart)
        {
            _time += Time.deltaTime * 0.2f;
            RenderSettings.fogEndDistance = Mathf.Lerp(RenderSettings.fogEndDistance, 300, _time);
            if(_time >= 1)
            {
                this.gameObject.SetActive(false);
            }
        }
    }

    public void GameStart()
    {
        _isGameStart = true;
    }
}
