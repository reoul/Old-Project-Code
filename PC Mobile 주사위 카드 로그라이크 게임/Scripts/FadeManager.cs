using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FadeManager : Singleton<FadeManager>
{
    /// <summary> 밝은 화면에서 서서히 어두어지기 시작할 때 발동 </summary>
    public UnityEvent FadeOutStartEvent { get; set; }
    
    /// <summary> 밝은 화면이 완전히 어두어졌을 때 발동 </summary>
    public UnityEvent FadeOutFinishEvent { get; set; }
    
    /// <summary> 어두운 화면에서 서서히 밝아지기 시작할 때 발동 </summary>
    public UnityEvent FadeInStartEvent { get; set; }
    
    /// <summary> 어두운 화면이 완전히 밝아졌을 때 발동 </summary>
    public UnityEvent FadeInFinishEvent { get; set; }

    [SerializeField] private float _fadeTime;
    [SerializeField] private Image _fadeImage;
    [SerializeField] private BoxCollider _boxCollider;


    public GameObject IntroObj;
    
    public bool IsFading { get; private set; }

    private void Awake()
    {
        FadeOutStartEvent = new UnityEvent();
        FadeOutFinishEvent = new UnityEvent();
        FadeInStartEvent = new UnityEvent();
        FadeInFinishEvent = new UnityEvent();
    }

    /// <summary> 밝은 화면에서 서서히 어두어짐 </summary>
    public void StartFadeOut()
    {
        StartCoroutine(FadeOutCoroutine());
    }

    /// <summary> 어두운 화면에서 서서히 밝아짐 </summary>
    public void StartFadeIn()
    {
        StartCoroutine(FadeInCoroutine());
    }

    public void NextStage()
    {
        FadeOutFinishEvent.AddListener(() =>
        {
            IntroObj.SetActive(false);
        });
        StartFadeOut();
        StageManager.Instance.SetFadeEvent(StageManager.Instance.NextStageType);
    }

    /// <summary> 밝은 화면에서 서서히 어두어짐 </summary>
    private IEnumerator FadeOutCoroutine()
    {
        Debug.Assert(_fadeImage != null);

        IsFading = true;

        FadeOutStartEvent.Invoke();
        FadeOutStartEvent.RemoveAllListeners();

        _fadeImage.enabled = true;
        _boxCollider.enabled = true;

        float time = 0;
        while (time < _fadeTime)
        {
            time += Time.deltaTime;
            float percent = time / _fadeTime;
            _fadeImage.color = new Color(0, 0, 0, percent);
            yield return new WaitForEndOfFrame();
        }

        _fadeImage.color = Color.black;

        FadeOutFinishEvent.Invoke();
        FadeOutFinishEvent.RemoveAllListeners();

        StartCoroutine(FadeInCoroutine());
    }

    /// <summary> 어두운 화면에서 서서히 밝아짐 </summary>
    private IEnumerator FadeInCoroutine()
    {
        Debug.Assert(_fadeImage != null);

        FadeInStartEvent.Invoke();
        FadeInStartEvent.RemoveAllListeners();

        float time = _fadeTime;
        while (time > 0)
        {
            time -= Time.deltaTime;
            float percent = time / _fadeTime;
            _fadeImage.color = new Color(0, 0, 0, percent);
            yield return new WaitForEndOfFrame();
        }

        _fadeImage.color = Color.clear;

        FadeInFinishEvent.Invoke();
        FadeInFinishEvent.RemoveAllListeners();
        
        _fadeImage.enabled = false;
        _boxCollider.enabled = false;

        IsFading = false;
    }
}
