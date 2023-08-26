using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageTextMover : MonoBehaviour
{
    private float _runningTime;
    private TMP_Text _text;
    private Vector3 _targetPos, _startPos;

    private float _upSpeed = 3, _disappearSpeed = 2; //default = 1

    private void Update()
    {
        if(_text.color.a <= 0)
        {
            this.gameObject.SetActive(false);
        }
        _runningTime += Time.deltaTime;

        transform.position = Vector3.Lerp(transform.position, _targetPos, 0.3f * Time.deltaTime * _upSpeed) + new Vector3(Mathf.Cos(_runningTime * 10) * Time.deltaTime, 0,0);
        Color tmpColor = _text.color;
        tmpColor.a -= 0.35f * Time.deltaTime * _disappearSpeed;
        _text.color = tmpColor;

    }

    public void Enable(int damage, Vector3 startPos)
    {
        this.gameObject.SetActive(true); 
        _text = transform.GetComponent<TMP_Text>();
        _text.text = $"-{damage.ToString()}";
        _startPos = startPos;
        _targetPos = _startPos + Vector3.forward * 3;
        transform.position = _startPos;
        Color tmpColor = _text.color;
        tmpColor.a = 1;
        _text.color = tmpColor;
        _runningTime = 0;
    }

}
