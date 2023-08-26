using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakPoint : MonoBehaviour, IHitable
{
    private Transform _target;
    private Enemy _parant;
    private SpriteRenderer[] _spriteRenderers;
    public delegate void ChangeWeak();
    public ChangeWeak changeEvent;
    public int Score { get; set; }

    /// <summary>
    /// 이미지의 alpha 값이 증가하는지
    /// </summary>
    private bool _isInc;

    /// <summary>
    /// alpha 값 증가 타이머
    /// </summary>
    private float _time;

    /// <summary>
    /// alpha 목표 시간
    /// </summary>
    private float _finishAlphaTime;

    /// <summary>
    /// 약점 이미지 alpha 퍼센트
    /// </summary>
    private float _alphaPercent => _time / _finishAlphaTime;

    private void Awake()
    {
        _spriteRenderers = GetComponentsInChildren<SpriteRenderer>(true);
    }

    private void Start()
    {
        _parant = transform.GetComponentInParent<Golem>();
        _target = GameObject.Find("Camera").transform;
    }

    private void Update()
    {
        transform.LookAt(_target);
        UpdateImageAlpha();
    }

    public void Show(float time)
    {
        SetImageAllColor(Color.clear);
        _isInc = true;
        _time = 0;
        _finishAlphaTime = time;
    }

    private void UpdateImageAlpha()
    {
        if ((_alphaPercent == 1 && _isInc) || (_alphaPercent == 0 && !_isInc))
        {
            return;
        }

        _time += (_isInc ? 1 : -1) * Time.deltaTime;
        _time = Mathf.Clamp(_time, 0, _finishAlphaTime);
        var color = new Color(1, 1, 1, Mathf.Lerp(0, 0.6f, _alphaPercent));
        SetImageAllColor(color);
    }

    private void SetImageAllColor(Color color)
    {
        foreach (var spriteRenderer in _spriteRenderers)
        {
            spriteRenderer.color = color;
        }
    }
  
    public void HitEvent()
    {
        //_parant.ChangeWeakPoint();
        if(StageManager.Instance.CurStage.IsFinish)
        {
            return;
        }
        changeEvent?.Invoke();
        ScoreSystem.Score += Score;
        SoundManager.Instance.PlaySoundFourth("StartButton_Hit", 0.5f);
    }


}