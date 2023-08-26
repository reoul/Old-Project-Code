using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

public class FixBar : MonoBehaviour
{
    public GameObject StartTrainingObj;
    
    private bool _isShow;
    private bool _isHide;
    private float _alpha = 0;

    void Update()
    {
        if (_isShow)
        {
            UpdateShow();
        }
        else if (_isHide)
        {
            UpdateHide();
        }
    }

    public void StartShow()
    {
        _alpha = 0;
        _isShow = true;
    }

    public void StartHide()
    {
        _alpha = 1;
        _isHide = true;
    }

    private void UpdateShow()
    {
        _alpha += Time.deltaTime;
        foreach (var image in GetComponentsInChildren<Image>())
        {
            image.color = new Color(0, 1, 1, _alpha);
        }
        foreach (var text in GetComponentsInChildren<Text>())
        {
            text.color = new Color(0, 1, 1, _alpha);
        }

        if (_alpha >= 1)
        {
            _isShow = false;
        }
    }

    private void UpdateHide()
    {
        _alpha -= Time.deltaTime * 2;
        foreach (var image in GetComponents<Image>())
        {
            image.color = new Color(0, 1, 1, _alpha);
        }
        foreach (var text in GetComponents<Text>())
        {
            text.color = new Color(0, 1, 1, _alpha);
        }

        if (_alpha <= 0)
        {
            _isHide = false;
            StartTrainingObj.SetActive(true);
            this.gameObject.SetActive(false);
            NarrationManager.Instance.IsNarrationStart = true;
        }
    }

}
