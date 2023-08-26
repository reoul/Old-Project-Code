using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GolemState : StateMachine
{
    public enum StateType
    {
        Idle = 0,
        Spawn,
        JumpAttack,
        SwingAttack,
        ThrowAttack
    }

    /// <summary>
    /// 보스 처음 위치
    /// </summary>
    public Vector3 BasePos { get; private set; }

    /// <summary>
    /// 보스 이동 위치
    /// </summary>
    public Vector3 DestPos { get; private set; }

    public bool CanMove { get; private set; }

    public bool IsMoveForward { get; private set; }

    private int _targetFloor;

    private void Awake()
    {
        InitStateDictionary();
    }

    private void Start()
    {
        BasePos = transform.position;
    }

    public override void InitStateDictionary()
    {
        StateDictionary = new Dictionary<int, State>();
        StateDictionary.Add((int) StateType.Idle, new IdleState(gameObject));
        StateDictionary.Add((int) StateType.Spawn, new SpawnState(gameObject));
        StateDictionary.Add((int) StateType.JumpAttack, new JumpAttackState(gameObject));
        StateDictionary.Add((int) StateType.SwingAttack, new SwingAttackState(gameObject));
        StateDictionary.Add((int) StateType.ThrowAttack, new ThrowAttackState(gameObject));
        ChangeState(StateDictionary[(int) StateType.Spawn]);
    }

    public void MoveForward()
    {
        CanMove = true;
        IsMoveForward = true;
    }

    public void MoveBack()
    {
        IsMoveForward = false;
        CanMove = true;
    }

    public void MoveStop()
    {
        CanMove = false;
    }

    public void ChangeStateIdle()
    {
        ChangeState(StateDictionary[(int) StateType.Idle]);
    }

    public void DissolveHideWeapon()
    {
        GetComponent<Golem>().WeaponDissolveMat.StartDestroyDissolve();
    }

    public void DissolveShowWeapon()
    {
        if (StageManager.Instance.CurStage.IsFinish)
        {
            return;
        }
        GetComponent<Golem>().WeaponDissolveMat.StartCreateDissolve();
    }

    public void SetJumpAttackFloor()
    {
        if (GetComponent<Golem>().AttackFollowPlayer)
        {
            int playerCurFloor = PlayerFloor.Instance.PlayerCurFloor == 0 ? 0 : 1;
            _targetFloor = playerCurFloor + Random.Range(0, 2);
        }
        else
        {
            _targetFloor = Random.Range(0, 3);
        }

        DestPos = PlayerFloor.Instance.attackTrans[_targetFloor].position + new Vector3(0, 0, 7);
        StartCoroutine(PlayerFloor.Instance.StartAttack(_targetFloor, 0));
    }

    public void SetSwingFloor()
    {
        _targetFloor = Random.RandomRange(0, 2);
        DestPos =
            (PlayerFloor.Instance.attackTrans[_targetFloor].position +
             PlayerFloor.Instance.attackTrans[_targetFloor + 1].position) * 0.5f +
            new Vector3(_targetFloor == 1 ? 1 : -1.5f, 0, 7);
        StartCoroutine(PlayerFloor.Instance.StartAttack(_targetFloor, 0));
        StartCoroutine(PlayerFloor.Instance.StartAttack(_targetFloor + 1, 0));
    }

    public void StopFloor()
    {
        for (int i = 0; i < 3; i++)
        {
            PlayerFloor.Instance.StopAttack(i);
        }
    }

    private class IdleState : State
    {
        private float _time;
        private int _lastAttackState = -1;

        public IdleState(GameObject gameObject) : base(gameObject)
        {
        }

        public override void StateStart()
        {
            _time = 0;
        }

        public override void StateUpdate()
        {
            if (StageManager.Instance.CurStage.IsFinish)
            {
                return;
            }
            _time += Time.deltaTime;
            if (_time >= 1)
            {
                int rand;
                do
                {
                    rand = Random.Range(0, 3);
                } while (rand == _lastAttackState);
                _lastAttackState = rand;
                _stateMachine.ChangeState(_stateMachine.StateDictionary[(int) StateType.JumpAttack + rand]);
            }
        }
    }

    private class SpawnState : State
    {
        public SpawnState(GameObject gameObject) : base(gameObject)
        {
        }

        public override void StateStart()
        {
            base.StateStart();
            _gameObject.GetComponent<Animator>().SetTrigger("Idle");
        }

        public override void StateUpdate()
        {
        }
    }

    private class JumpAttackState : State
    {
        private float _time;
        private GolemState _golemState;

        public JumpAttackState(GameObject gameObject) : base(gameObject)
        {
            _golemState = gameObject.GetComponent<GolemState>();
        }

        public override void StateStart()
        {
            _gameObject.GetComponent<Animator>().SetTrigger("JumpAttack");
            _time = 0;
        }

        public override void StateUpdate()
        {
            if (_golemState.CanMove)
            {
                _time += (_golemState.IsMoveForward ? 1.4f : -1.5f) * Time.deltaTime;
                _gameObject.transform.position = Vector3.Lerp(_golemState.BasePos, _golemState.DestPos, _time);
            }
        }
    }

    private class SwingAttackState : State
    {
        private float _time;
        private GolemState _golemState;

        public SwingAttackState(GameObject gameObject) : base(gameObject)
        {
            _golemState = gameObject.GetComponent<GolemState>();
        }

        public override void StateStart()
        {
            _gameObject.GetComponent<Animator>().SetTrigger("SwingAttack");
            _time = 0;
        }

        public override void StateUpdate()
        {
            if (_golemState.CanMove)
            {
                _time += (_golemState.IsMoveForward ? 0.91f : -1.5f) * Time.deltaTime;
                _gameObject.transform.position = Vector3.Lerp(_golemState.BasePos, _golemState.DestPos, _time);
            }
        }
    }

    private class ThrowAttackState : State
    {
        public ThrowAttackState(GameObject gameObject) : base(gameObject)
        {
        }

        public override void StateStart()
        {
            _gameObject.GetComponent<Animator>().SetTrigger("ThrowAttack");
        }

        public override void StateUpdate()
        {
        }
    }
}