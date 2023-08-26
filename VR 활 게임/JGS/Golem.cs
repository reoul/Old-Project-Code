using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Golem : Enemy
{
    [SerializeField] private Transform _shootPosTransfrom;
    [SerializeField] private GameObject _projectile;

    public DissolveMat WeaponDissolveMat;

    private Transform _target;

    private int _targetFloor;
    private int _weakAttackCnt = 0;
    private int _weakAttackBreakCnt = 3;
    private float _weakAttackBreakTime = 3;

    private float _time;
    private bool _isMoveForward = false;
    public bool AttackFollowPlayer = false;

    [SerializeField] private float _weakAlphaSpeed;

    private void Awake()
    {
        _stateMachine = GetComponent<StateMachine>();
    }

    private void Start()
    {
        _target = GameObject.Find("[CameraRig]").transform;
        //RandomWeak();
        foreach (var weakPoint in GetComponentsInChildren<WeakPoint>(true))
        {
            weakPoint.changeEvent += ChangeWeakPoint;
            weakPoint.Score = DataManager.Instance.Data.GolemScore;
        }
        maxHealth = DataManager.Instance.Data.GolemMaxHealth;
        damage = DataManager.Instance.Data.GolemDamage;
        _weakAttackBreakCnt = DataManager.Instance.Data.EntWeakAttackBreakCnt;
        _weakAttackBreakTime = DataManager.Instance.Data.EntWeakAttackBreakTime;
        AttackFollowPlayer = DataManager.Instance.Data.GolemAttackFollowPlayer;
    }

    public void Init()
    {
        _weakAttackCnt = 0;
        RandomWeak();
        ChangeState(GolemState.StateType.Spawn);
    }

    private void Update()
    {
        _stateMachine.StateUpdate();
    }

    public void ChangeState(GolemState.StateType state)
    {
        _stateMachine.ChangeState(_stateMachine.StateDictionary[(int)state]);
    }

    private Transform _curWeak;
    [SerializeField] private Transform[] _weakPoints;

    private void RandomWeak()
    {
        _curWeak = _weakPoints[Random.Range(0, _weakPoints.Length)];
        _curWeak.gameObject.SetActive(true);
        _curWeak.GetComponent<WeakPoint>().Show(3);
    }

    /// <summary>
    /// 약점을 변경한다
    /// </summary>
    public void ChangeWeakPoint()
    {
        int rand;
        do
        {
            rand = Random.Range(0, _weakPoints.Length);
        } while (_curWeak == _weakPoints[rand]);
        if(StageManager.Instance.CurStage.IsFinish)
        {
            return;
        }
        _curWeak.gameObject.SetActive(false);
        HealthBarManager.Instance.DistractDamage();
        _curWeak = _weakPoints[rand];
        _weakAttackCnt = ++_weakAttackCnt % _weakAttackBreakCnt;
        StartCoroutine(ShowWeakPoint(_weakAttackCnt == 0 ? _weakAttackBreakTime : 0));
    }

    private IEnumerator ShowWeakPoint(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        _curWeak.gameObject.SetActive(true);
        _curWeak.GetComponent<WeakPoint>().Show(_weakAlphaSpeed);
    }

    private bool _isTargeted;

    private GameObject _stone;

    private void SpawnStone()
    {
        if (AttackFollowPlayer)
        {
            int playerCurFloor = PlayerFloor.Instance.PlayerCurFloor == 0 ? 0 : 1;
            _targetFloor = playerCurFloor + Random.Range(0, 2);
        }
        else
        {
            _targetFloor = Random.Range(0, 3);
        }
        
        _stone = GameObject.Instantiate(_projectile);
        _stone.transform.localPosition = Vector3.zero;
        _stone.GetComponent<Projectile_Stone>()._rootTransform = _shootPosTransfrom;
        _stone.GetComponent<Projectile_Stone>()._targetPos = PlayerFloor.Instance.attackTrans[_targetFloor].position;
        StartCoroutine(PlayerFloor.Instance.StartAttack(_targetFloor, 0));
    }

    public void ThrowStone()
    {
        _stone.GetComponent<Projectile_Stone>().isThrow = true;
        _stone.GetComponent<Projectile_Stone>().isThrownSound = true;
        _stone.GetComponent<Projectile_Stone>().stage3_Stone = true;
        _stone.GetComponent<Projectile_Stone>().targetFloor = _targetFloor;
    }

    public void HideWeak()
    {
        StopAllCoroutines();
        foreach (Transform weak in _weakPoints)
        {
            weak.gameObject.SetActive(false);
        }
    }
}