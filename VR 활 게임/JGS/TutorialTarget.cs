using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTarget : Enemy
{
    private Vector3 _startPos;

    private void OnEnable()
    {
        _startPos = this.transform.position;
    }

    private void Update()
    {
        this.transform.position = new Vector3(_startPos.x, _startPos.y + (Mathf.Sin(Time.time) * 0.1f),_startPos.z);
    }
}
