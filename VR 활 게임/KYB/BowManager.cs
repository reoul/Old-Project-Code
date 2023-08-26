using System;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Collections;

public class BowManager : Singleton<BowManager>
{
    private LineRenderer _arrowTrajectoryLineRenderer;
    private LineRenderer _bowStringLineRenderer;
    private LineRenderer _bowStringLineRenderer2;
    public GameObject BowObj;
    public Transform BowAttackTransform;
    private Transform _bowTopTransform;
    private Transform _bowBottomTransform;
    public ParticleSystem ChargingParticleSystem;
    public GameObject CancelEffect;
    public GameObject StopEffect;
    public float chargingSpeed;
    private bool _isFirstAimStartTarget = true;

    private void Awake()
    {
        _arrowTrajectoryLineRenderer = GameObject.Find("ArrowTrajectoryLine").GetComponent<LineRenderer>();
        _bowStringLineRenderer = GameObject.Find("BowStringLine").GetComponent<LineRenderer>();
        _bowStringLineRenderer2 = GameObject.Find("BowStringLine2").GetComponent<LineRenderer>();

        _arrowTrajectoryLineRenderer.SetPosAllZero();
        _bowTopTransform = BowObj.transform.GetChild(0).GetChild(0);
        _bowBottomTransform = BowObj.transform.GetChild(0).GetChild(1);
        BowAttackTransform = BowObj.transform.GetChild(0).GetChild(2);
    }

    private void Update()
    {
        ShowArrowTrajectory();
        ShowBowString();
        VibrationBow();
        ShowChargingEffect();
        CheckAnimeStartTarget();
    }

    public void Init()
    {
        _isFirstAimStartTarget = true;
    }

    /// <summary>
    /// 화살의 궤적을 보여준다
    /// </summary>
    private void ShowArrowTrajectory()
    {
        // 차징하고 있지 않을 때
        if (!VRControllerManager.Instance.IsCharging)
        {
            _arrowTrajectoryLineRenderer.SetPosAllZero();
            return;
        }

        _arrowTrajectoryLineRenderer.material.color = !VRControllerManager.Instance.IsChargingFinish
            ? new Color(1, 0, 0, 0.2f)
            : new Color(0, 1, 0, 0.2f);

        _arrowTrajectoryLineRenderer.SetPosition(0, BowAttackTransform.position);
        _arrowTrajectoryLineRenderer.SetPosition(1, VRControllerManager.Instance.Direction * 1000);
    }

    /// <summary>
    /// 활 시위를 보여준다
    /// </summary>
    private void ShowBowString()
    {
        // 차징하고 있지 않을 때
        if (!VRControllerManager.Instance.IsCharging)
        {
            _bowStringLineRenderer.SetPosAllZero();
            _bowStringLineRenderer2.SetPosAllZero();
            return;
        }

        var arrowControllerPos = VRControllerManager.Instance.ArrowController.CenterTransform.transform.position;
        _bowStringLineRenderer.SetPosition(0, _bowTopTransform.position);
        _bowStringLineRenderer.SetPosition(1, arrowControllerPos);
        _bowStringLineRenderer2.SetPosition(0, arrowControllerPos);
        _bowStringLineRenderer2.SetPosition(1, _bowBottomTransform.position);
    }

    /// <summary> 차징 중이면 활이 떨리게 표현 </summary>
    private void VibrationBow()
    {
        float vibration = VRControllerManager.Instance.ChargingPercent * 0.002f;
        float x = Random.Range(-vibration, vibration);
        float y = Random.Range(-vibration, vibration);
        float z = Random.Range(-vibration, vibration);
        BowObj.transform.GetChild(0).transform.localPosition = new Vector3(x, y, z);
    }

    private void ShowChargingEffect()
    {
        if (VRControllerManager.Instance.IsCharging)
        {
            if (!ChargingParticleSystem.gameObject.activeInHierarchy)
            {
                ChargingParticleSystem.gameObject.SetActive(true);
            }

            ChargingParticleSystem.startLifetime = 1 - VRControllerManager.Instance.ChargingPercent * 0.65f;
        }
        else
        {
            ChargingParticleSystem.gameObject.SetActive(false);
        }
    }

    private void CheckAnimeStartTarget()
    {
        if (!_isFirstAimStartTarget)
        {
            return;
        }

        if (VRControllerManager.Instance.IsCharging)
        {
            RaycastHit[] hits = new RaycastHit[] { };
            hits = Physics.RaycastAll(BowAttackTransform.position, VRControllerManager.Instance.Direction, 1000);
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.name.Equals("NextStageObj"))
                {
                    NarrationManager.Instance.IsCheckFlag = false;
                    _isFirstAimStartTarget = false;
                    break;
                }
            }
        }
    }
}
