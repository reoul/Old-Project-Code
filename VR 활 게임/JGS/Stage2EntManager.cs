using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Stage2EntManager : MonoBehaviour
{
    [SerializeField] private Transform[] _ents;

    private float _attackDelay;
    private float _lastAttackTime;

    /// <summary>
    /// 시간차 공격하는 딜레이 시간
    /// </summary>
    private float _attackTimeLagDelay;

    private int _lastAttackPatten;
    private int _weakAttackCnt = 0;
    private int _weakAttackBreakCnt = 3;
    private float _weakAttackBreakTime = 3;

    private void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            _ents[i].GetComponent<JGS_Ent>().targetFloor = i;
        }

        _attackDelay = DataManager.Instance.Data.EntAttackDelay;
        _weakAttackBreakCnt = DataManager.Instance.Data.GolemWeakAttackBreakCnt;
        _weakAttackBreakTime = DataManager.Instance.Data.GolemWeakAttackBreakTime;
        _attackTimeLagDelay = _attackDelay * 0.4f;
        _lastAttackPatten = -1;
    }

    private void OnEnable()
    {
        _lastAttackTime = Time.time;
        RandomWeak();
        Init();
    }


    public void Init()
    {
        _weakAttackCnt = 0;
        _lastAttackPatten = -1;
    }

    private void Update()
    {
        if (StageManager.Instance.CurStage.IsFinish)
        {
            return;
        }

        if (Time.time - _lastAttackTime > _attackDelay)
        {
            _lastAttackTime = Time.time;
            SetTarget();
        }
    }

    private void SetTarget()
    {
        int[] rands = new int[3];
        do
        {
            rands[0] = Random.Range(0, 4);
        } while (rands[0] == _lastAttackPatten);

        _lastAttackPatten = rands[0];
        Assert.IsTrue(_ents.Length == 3);
        switch (rands[0])
        {
            // 돌 던지기
            case 0:
                rands[0] = Random.Range(0, _ents.Length);
                _ents[rands[0]].GetComponent<JGS_EntState>().Attack();
                break;
            // 돌 두개 던지기
            case 1:
                rands[0] = Random.Range(0, _ents.Length);
                do
                {
                    rands[1] = Random.Range(0, _ents.Length);
                } while (rands[0] == rands[1]);

                _ents[rands[0]].GetComponent<JGS_EntState>().Attack();
                _ents[rands[1]].GetComponent<JGS_EntState>().Attack();
                break;
            // 시간차 2개 던지기
            case 2:
                rands[0] = Random.Range(0, _ents.Length);
                do
                {
                    rands[1] = Random.Range(0, _ents.Length);
                } while (rands[0] == rands[1]);

                _ents[rands[0]].GetComponent<JGS_EntState>().Attack();
                StartCoroutine(_ents[rands[1]].GetComponent<JGS_EntState>().AttackDelayCoroutine(_attackTimeLagDelay));
                _lastAttackTime += _attackTimeLagDelay;
                break;
            // 시간차 3개 던지기
            case 3:
                rands[0] = Random.Range(0, _ents.Length);
                do
                {
                    rands[1] = Random.Range(0, _ents.Length);
                } while (rands[0] == rands[1]);

                rands[2] = _ents.Length - (rands[0] + rands[1]);

                _ents[rands[0]].GetComponent<JGS_EntState>().Attack();
                StartCoroutine(_ents[rands[1]].GetComponent<JGS_EntState>().AttackDelayCoroutine(_attackTimeLagDelay));
                StartCoroutine(_ents[rands[2]].GetComponent<JGS_EntState>()
                                              .AttackDelayCoroutine(_attackTimeLagDelay * 2));
                _lastAttackTime += _attackTimeLagDelay * 2;
                break;
        }
    }

    private Transform _curWeak;
    [SerializeField] private Transform[] _weakPoints;
    [SerializeField] private float _weakAlphaSpeed;

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

        _curWeak.gameObject.SetActive(false);
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

    public void HideWeak()
    {
        _curWeak.gameObject.SetActive(false);
    }
}