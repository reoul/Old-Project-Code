using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

/// <summary>
/// VR 컨트롤러 핸드 타입
/// </summary>
public enum HandType
{
    Left,
    Right,
    LeftRight
}

public class VRController : MonoBehaviour
{
    public SteamVR_Input_Sources HandType; //모두 사용, 왼손, 오른손
    public SteamVR_Action_Boolean GrabAction;
    public SteamVR_Action_Vibration HapticAction;
    public Transform CenterTransform { get; private set; }

    public MeshRenderer[] _meshRenderers;

    public GameObject SysBtn { get; private set; } = null;
    public GameObject TrackPad { get; private set; } = null;

    public LineRenderer Linerend;

    private bool _isStart = false;

    void Start()
    {
        StartCoroutine(FindChildMeshCoroutine());
    }

    private void Update()
    {
        CheckRay();
    }

    void CheckRay()
    {
        if (!_isStart && SysBtn != null && TrackPad != null)
        {
            RaycastHit[] hits = Physics.RaycastAll(CenterTransform.position,
                TrackPad.transform.position - SysBtn.transform.position, 10000);
            
            int cnt = 0;
            foreach (var hit in hits)
            {
                IRayInteractive rayInteractive;
                if (hit.collider.TryGetComponent<IRayInteractive>(out rayInteractive))
                {
                    Linerend.SetPosition(0, CenterTransform.position);
                    Linerend.SetPosition(1, (TrackPad.transform.position - SysBtn.transform.position) * 1000);
                    cnt++;
                    
                    if (GetTriggerDown())
                    {
                        rayInteractive.RayInteractive();
                        _isStart = true;
                    }

                    break;
                }
            }

            if (cnt == 0)
            {
                Linerend.SetPosAllZero();
            }
        }
    }

    private void FindChildMesh()
    {
        _meshRenderers = this.transform.GetChild(0).GetComponentsInChildren<MeshRenderer>();
    }

    private IEnumerator FindChildMeshCoroutine()
    {
        while (true)
        {
            if (_meshRenderers.Length != 0)
            {
                CenterTransform = transform.GetChild(0).Find("sys_button").GetChild(0);
                SysBtn = CenterTransform.gameObject;
                TrackPad = transform.GetChild(0).Find("trackpad").GetChild(0).gameObject;
                var collider = this.gameObject.AddComponent<BoxCollider>();
                collider.size = Vector3.one * 0.1f;
                collider.isTrigger = true;
                break;
            }

            FindChildMesh();
            yield return new WaitForEndOfFrame();
        }
    }

    /// <summary> 진동 </summary>
    /// <param name="frequency">진동 크기(0~60)</param>
    public void Vibration(int frequency)
    {
        frequency = Mathf.Clamp(frequency, 0, 60);
        Pulse(0.1f, frequency, 1, HandType);
    }

    /// <summary> 진동 </summary>
    /// <param name="duration">진동 지속 시간</param>
    /// <param name="frequency">진동 크기 0~320</param>
    /// <param name="amplitude">촉각 작용의 강도 0-1</param>
    /// <param name="source"></param>
    private void Pulse(float duration, float frequency, float amplitude, SteamVR_Input_Sources source)
    {
        HapticAction.Execute(0, duration, frequency, amplitude, source);
    }

    /// <summary>
    /// 트리거 버튼을 지속적으로 누르고 있는 상태
    /// </summary>
    /// <returns>트리거 상태</returns>
    public bool GetTrigger()
    {
        return GrabAction.GetState(HandType);
    }

    /// <summary>
    /// 트리거 버튼이 한번 누른 상태
    /// </summary>
    /// <returns>트리거 상태</returns>
    public bool GetTriggerDown()
    {
        return GrabAction.GetStateDown(HandType);
    }

    /// <summary>
    /// 트리거 버튼이 한번 눌렀다가 땐 상태
    /// </summary>
    /// <returns>트리거 상태</returns>
    public bool GetTriggerUp()
    {
        return GrabAction.GetStateUp(HandType);
    }

    /// <summary>
    /// 컨트롤러 mesh 끄기
    /// </summary>
    public void MeshOff()
    {
        MeshOnOFF(false);
    }

    /// <summary>
    /// 컨트롤러 mesh 켜기
    /// </summary>
    public void MeshON()
    {
        MeshOnOFF(true);
    }

    /// <summary>
    /// 컨트롤러 mesh 켜고 끄기
    /// </summary>
    /// <param name="isOn"></param>
    private void MeshOnOFF(bool isOn)
    {
        foreach (var meshRenderer in _meshRenderers)
        {
            meshRenderer.enabled = isOn;
        }
    }
}
