using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class OverlayBase : MonoBehaviour
{
    /// <summary> 오버레이를 보여주기 위해 마우스를 오브젝트에 올려두는 시간 </summary>
    private const float PUT_MOUSE_TIME = 1f;
    private float _overTime = 0;
    private bool _isShowOverlay = false;
    
    /// <summary> 오브젝트에 마우스를 올려놨을 때 </summary>
    protected virtual void OnMouseEnter()
    {
        _overTime = 0;
        _isShowOverlay = false;
        //Logger.Log("오버레이 시작");
    }

    /// <summary> 오브젝트에 마우스를 계속 올려놨을 때 </summary>
    protected virtual void OnMouseOver()
    {
        _overTime += Time.unscaledDeltaTime;
        if (!_isShowOverlay && _overTime >= PUT_MOUSE_TIME)   // 마우스를 일정 시간동안 올려놨을 때 오버레이를 보여줌
        {
            _isShowOverlay = true;
            ShowOverlay();
        }
    }

    /// <summary>
    /// 오브젝트에서 마우스가 벗어날 때
    /// </summary>
    protected virtual void OnMouseExit()
    {
        if (_isShowOverlay)     // 만약 오버레이를 보여줬다면 사라지게 함
        {
            HideOverlay();
        }
    }

    /// <summary> 설명창을 보여줌 </summary>
    protected abstract void ShowOverlay();
    /// <summary> 설명창을 숨겨줌 </summary>
    protected abstract void HideOverlay();
}
