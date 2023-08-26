using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingWindow : MonoBehaviour
{
    private bool _isOpen;
    [SerializeField] private GameObject _settingWindowObj;
    [SerializeField] private Slider _bgmSlider;
    [SerializeField] private Slider _sfxSlider;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Open();
        }
    }

    public void Open()
    {
        _isOpen = !_isOpen;
        _settingWindowObj.SetActive(_isOpen);
    }

    public void ChangeBgmVolume()
    {
        SoundManager.Instance.SetVolumeBGM(_bgmSlider.value);
    }
    
    public void ChangeSfxVolume()
    {
        SoundManager.Instance.SetVolumeSFX(_sfxSlider.value);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
