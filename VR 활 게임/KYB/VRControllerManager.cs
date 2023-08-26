using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

public class VRControllerManager : Singleton<VRControllerManager>
{
    public VRController LeftController { get; private set; }
    public VRController RightController { get; private set; }

    public VRController BowAbleController { get; set; } = null;
    
    /// <summary>
    /// 차징하고 있는지
    /// </summary>
    public bool IsCharging { get; private set; }

    public bool IsMoveStop { get; private set; }

    private List<Vector3> _posList = new List<Vector3>();

    /// <summary>
    /// 차징된 시간
    /// </summary>
    private float _chargingTime = 0;

    /// <summary>
    /// 최대 차징 게이지
    /// </summary>
    private float _maxCharging = 60;

    public float ChargingPercent => Mathf.Clamp01(_chargingTime / _maxCharging);

    /// <summary>
    /// 차징 딜레이 주기 체크하는 변수
    /// </summary>
    private float _checkChargingTime = 0;

    /// <summary>
    /// 차징 속도
    /// </summary>
    private float _chargingSpeed = 360;

    /// <summary>
    /// 차징 딜레이 시간
    /// </summary>
    private float _chargingDelay = 0.08f;

    /// <summary>
    /// 차징 처음 시작할때 두 컨트롤러 최소거리
    /// </summary>
    private float _startChargingDistanceMin = 0.3f;


    /// <summary>
    /// 차징 시 두 컨트롤러 최대 거리
    /// </summary>
    private float _chargingMaxDistance = 0.5f;

    private float _fixLeftTimer = 0;
    private float _fixRightTimer = 0;

    private bool _isFixStartLeftController = false;
    private bool _isFixStartRightController = false;

    private const float FIX_INTERVAL_TIME = 3;

    public Image FixBarImage;
    
    /// <summary>
    /// 차징이 100퍼센트 됬는지 체크
    /// </summary>
    public bool IsChargingFinish
    {
        get { return _chargingTime >= _maxCharging; }
    }

    /// <summary>
    /// 활을 들고 있는 컨트롤러
    /// </summary>
    public VRController BowController { get; set; }

    /// <summary>
    /// 활 시위를 당기고 있는 컨트롤러
    /// </summary>
    public VRController ArrowController
    {
        get { return BowController == LeftController ? RightController : LeftController; }
    }


    /// <summary>
    /// 화살 방향
    /// </summary>
    public Vector3 Direction
    {
        get
        {
            if (BowController == null)
            {
                return Vector3.zero;
            }

            return BowManager.Instance.BowAttackTransform.transform.position -
                   ArrowController.CenterTransform.transform.position;
        }
    }

    /// <summary>
    /// 두 컨트롤러 간의 거리
    /// </summary>
    public float Distance
    {
        get
        {
            if (BowController != null)
            {
                return Vector3.Distance(BowController.CenterTransform.transform.position,
                    ArrowController.CenterTransform.transform.position);
            }

            return 0;
        }
    }

    public bool ClickBow;

    private void Awake()
    {
        FindController();
        IsCharging = false;
    }

    private void Start()
    {
        _startChargingDistanceMin = DataManager.Instance.Data.StartChargingDistanceMin;
        _maxCharging = DataManager.Instance.Data.BowMaxChargingTime;
        _chargingMaxDistance = DataManager.Instance.Data.ChargingMaxDistance;
    }

    public void Init()
    {
        ClickBow = false;
        BowController?.MeshON();
        ArrowController?.MeshON();
        BowManager.Instance.BowObj.SetActive(false);
        ArrowManager.Instance.ShowArrow(false);
        BowController = null;
        IsCharging = false;
        _chargingTime = 0;
    }

    private void Update()
    {
        CheckFixController();
        UpdateFixBar();
        CheckBow();
        CheckCharging();
        ChargingSound();
        ChargingVibration();
        CheckShot();
    }

    private void ChargingSound()
    {
        if (IsCharging)
        {
            SoundManager.Instance.PlaySound("RemakeCharge", 5f);
            SoundManager.Instance.sfxPlayer.GetComponent<AudioSource>().pitch =
                Mathf.Lerp(0f, 0.77f, _chargingTime / _maxCharging);
        }
    }

    /// <summary>
    /// BowController와 ArrowController를 지정해준다
    /// </summary>
    public void CheckBow()
    {
        if (!ClickBow)
        {
            return;
        }
        
        /*if (LeftController.GetTriggerDown())
        {
            // 오른쪽 컨트롤러 트리거를 사용 안할때
            if (!(RightController.GetTrigger() || RightController.GetTriggerDown()))
            {
                SetBowController(LeftController);
            }
        }
        else if (RightController.GetTriggerDown())
        {
            // 왼쪽 컨트롤러 트리거를 사용 안할때
            if (!(LeftController.GetTrigger() || LeftController.GetTriggerDown()))
            {
                SetBowController(RightController);
            }
        }*/

        if (BowAbleController != null)
        {
            if (BowAbleController.GetTriggerDown())
            {
                SetBowController(BowAbleController);
            }
        }
        

        if (BowController != null)
        {
            if (BowController.GetTriggerUp())
            {
                BowController.MeshON();
                ArrowController.MeshON();
                BowManager.Instance.BowObj.SetActive(false);
                ArrowManager.Instance.ShowArrow(false);
                BowController = null;
                IsCharging = false;
                _chargingTime = 0;
            }
        }
    }

    /// <summary>
    /// 차징 시작했는지 확인
    /// </summary>
    private void CheckCharging()
    {
        if (IsCharging)
        {
            return;
        }

        if (BowController != null)
        {
            if (Distance > _startChargingDistanceMin)
            {
                return;
            }

            if (ArrowController.GetTriggerDown())
            {
                IsCharging = true;
                ArrowController.MeshOff();
                ArrowManager.Instance.ShowArrow(true);
            }
        }
    }

    /// <summary>
    /// 화살을 발사하는 입력을 받았는지 체크
    /// </summary>
    private void CheckShot()
    {
        if ((BowController != null) && ArrowController.GetTriggerUp())
        {
            if (_chargingTime >= _maxCharging)
            {
                SoundManager.Instance.sfxPlayer.GetComponent<AudioSource>().Stop();
                ArrowManager.Instance.Shot(BowManager.Instance.BowAttackTransform.position, Direction);
                FindObjectOfType<ArrowAfterImage>().Shot();
                SoundManager.Instance.sfxPlayer.GetComponent<AudioSource>().pitch = 1f;
                SoundManager.Instance.PlaySound("ArrowShot_1", 1f);
            }
            ArrowManager.Instance.ShowArrow(false);
            ArrowController.MeshON();
            IsCharging = false;
            _chargingTime = 0;
        }
    }

    // 반드시 처음에 컨트롤러를 찾아서 해당 변수에 적용시켜줘야한다.
    /// <summary>
    /// VR컨트롤러를 찾는다
    /// </summary>
    private void FindController()
    {
        foreach (var controller in FindObjectsOfType<VRController>())
        {
            if (controller.HandType == SteamVR_Input_Sources.LeftHand)
            {
                LeftController = controller;
                CreatePoint();
            }
            else if (controller.HandType == SteamVR_Input_Sources.RightHand)
            {
                RightController = controller;
            }
        }
    }

    private void CheckStop()
    {
        if (_posList.Count >= 10)
        {
            _posList.RemoveAt(0);
        }

        _posList.Add(Camera.main.transform.position);
        IsMoveStop = IsPosStop();
    }

    private bool IsPosStop()
    {
        Vector3 sum = Vector3.zero;
        foreach (Vector3 pos in _posList)
        {
            sum += pos;
        }

        sum /= _posList.Count;
        if (Mathf.Abs(_posList[0].x - sum.x) < 0.01f)
        {
            if (Mathf.Abs(_posList[0].z - sum.z) < 0.01f)
            {
                return true;
            }
            else
                return false;
        }
        else
            return false;
    }

    private void CreatePoint()
    {
        Vector3 pos = new Vector3(0f, -0.072f, 0.044f);
        var leftGameObj = new GameObject("ControllerPoint");
        leftGameObj.transform.parent = LeftController.transform;
        leftGameObj.transform.localPosition = pos;
        var rightGameObj = new GameObject("ControllerPoint");
        rightGameObj.transform.parent = RightController.transform;
        rightGameObj.transform.localPosition = pos;
    }

    /// <summary>
    /// 진동 주기
    /// </summary>
    /// <param name="handType">컨트롤러 타입</param>
    /// <param name="frequency">진동 크기 (0~60)</param>
    public void Vibration(HandType handType, int frequency)
    {
        switch (handType)
        {
            case HandType.LeftRight:
                LeftController.Vibration(frequency);
                RightController.Vibration(frequency);
                break;
            case HandType.Left:
                LeftController.Vibration(frequency);
                break;
            case HandType.Right:
                RightController.Vibration(frequency);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(handType), handType, null);
        }
    }

    /// <summary>
    /// 차징 진동
    /// </summary>
    private void ChargingVibration()
    {
        if (IsCharging)
        {
            _checkChargingTime += Time.deltaTime;
            if (_checkChargingTime >= _chargingDelay)
            {
                _checkChargingTime -= _chargingDelay;
                ChargingTime();
                StartChargingVibration();
            }
        }
    }

    /// <summary>
    /// 차징 진동
    /// </summary>
    private void StartChargingVibration()
    {
        Vibration(HandType.LeftRight, (int)_chargingTime);
    }

    /// <summary>
    /// 차징 중일때 일정 시간마다 차징 게이지를 늘려줌
    /// </summary>
    private void ChargingTime()
    {
        //if(IsCharging)
        //{
        //    _chargingTime += _chargingSpeed * Time.deltaTime;
        //}
        //else
        //{
        //    _chargingTime -= _chargingSpeed * Time.deltaTime;
        //}

        if (Distance > _chargingMaxDistance)
        {
            _chargingTime += _chargingSpeed * Time.deltaTime;
        }
        else if (Distance >= Mathf.Lerp(0, _chargingMaxDistance, 0.5f))
        {
            _chargingTime += _chargingSpeed * Time.deltaTime;
            _chargingTime = Mathf.Clamp(_chargingTime, 0, Mathf.Lerp(0, _chargingMaxDistance, 0.67f));
        }
        else
        {
            _chargingTime += _chargingSpeed * Time.deltaTime;
            _chargingTime = Mathf.Clamp(_chargingTime, 0, Mathf.Lerp(0, _chargingMaxDistance, 0.34f));
        }

        // 두 컨트롤러 거리가 일정 이상 멀어졌을때(활 쏘는 동작을 했을 때)
        //if (Distance > Mathf.Lerp(0, _maxDistance, 0.7f))
        //{
        //    _chargingTime += _chargingSpeed * Time.deltaTime;
        //}
        //else
        //{
        //    _chargingTime -= _chargingSpeed * Time.deltaTime;
        //}
    }

    /// <summary>
    /// BowController 지정해주고 mesh를 꺼줌
    /// </summary>
    /// <param name="controller"></param>
    private void SetBowController(VRController controller)
    {
        BowController = controller;
        BowController.MeshOff();
        var sysBtnTrans = BowController.SysBtn.transform;
        GameObject bowObj = BowManager.Instance.BowObj;
        bowObj.transform.parent = BowController.transform;
        bowObj.transform.position = Vector3.Lerp(BowController.TrackPad.transform.position, sysBtnTrans.position, 0.5f);
        bowObj.transform.rotation = sysBtnTrans.rotation * Quaternion.Euler(180, 0, 180);
        bowObj.SetActive(true);
    }

    private void CheckFixController()
    {
        if (BowAbleController != null)
        {
            return;
        }
        
        if (LeftController.GetTriggerDown())
        {
            _isFixStartLeftController = true;
        }
        else if (RightController.GetTriggerDown())
        {
            _isFixStartRightController = true;
        }
        else if (LeftController.GetTriggerUp())
        {
            _isFixStartLeftController = false;
            _fixLeftTimer = 0;
        }
        else if (RightController.GetTriggerUp())
        { 
            _isFixStartRightController = false;
            _fixRightTimer = 0;
        }

        if (_isFixStartLeftController)
        {
            _fixLeftTimer += Time.deltaTime;
            if (_fixLeftTimer > FIX_INTERVAL_TIME)
            {
                BowAbleController = LeftController;
                _isFixStartLeftController = false;
                GameObject.Find("GameManager").GetComponent<GameManager>().FixBar.StartHide();
            }
        }

        if (_isFixStartRightController)
        {
            _fixRightTimer += Time.deltaTime;
            if (_fixRightTimer > FIX_INTERVAL_TIME)
            {
                BowAbleController = RightController;
                _isFixStartRightController = false;
                GameObject.Find("GameManager").GetComponent<GameManager>().FixBar.StartHide();
            }
        }
    }

    private void UpdateFixBar()
    {
        if (BowAbleController != null)
        {
            return;
        }

        FixBarImage.fillAmount = (_fixLeftTimer > _fixRightTimer ? _fixLeftTimer : _fixRightTimer) / FIX_INTERVAL_TIME;
    }
}