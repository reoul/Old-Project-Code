using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class JGS_EntState : StateMachine
{
    public enum StateType
    {
        Idle,
        Spawn,
        Run,
        Hit,
        Attack,
        Death
    }
    Vector3 pos;
    private void Awake()
    {
        InitStateDictionary();
        pos = this.transform.position;
    }

    public override void InitStateDictionary()
    {
        StateDictionary = new Dictionary<int, State>();
        StateDictionary.Add((int)StateType.Idle, new IdleState(gameObject));
        StateDictionary.Add((int)StateType.Spawn, new SpawnState(gameObject));
        StateDictionary.Add((int)StateType.Run, new RunState(gameObject));
        StateDictionary.Add((int)StateType.Hit, new HitState(gameObject));
        StateDictionary.Add((int)StateType.Attack, new AttackState(gameObject));
        StateDictionary.Add((int)StateType.Death, new DeathState(gameObject));
        ChangeState(StateDictionary[(int)StateType.Idle]);
    }

    private void LateUpdate()
    {
        this.transform.position = pos;
    }

    public void Attack()
    {
        ChangeState(StateDictionary[(int)StateType.Attack]);
    }

    public IEnumerator AttackDelayCoroutine(float delay = 0)
    {
        yield return new WaitForSeconds(delay);
        if(StageManager.Instance.CurStage.IsFinish == false)
        {
            Attack();
        }
    }

    public void Idle()
    {
        ChangeState(StateDictionary[(int)StateType.Idle]);
    }

    //public bool IsIdle()
    //{
    //    if(_curState == StateDictionary[(int)StateType.Idle])
    //    {
    //        return true;
    //    }
    //    return false;
    //}

    private class IdleState : State
    {
        public IdleState(GameObject gameObject) : base(gameObject)
        {
        }

        public override void StateStart()
        {
            _gameObject.transform.LookAt(Vector3.zero);
        }

        public override void StateUpdate()
        {
        }
    }

    private class SpawnState : State
    {
        private DissolveMat _dissolveMat;

        public SpawnState(GameObject gameObject) : base(gameObject)
        {
        }

        public override void StateStart()
        {
            _dissolveMat = _gameObject.GetComponentInChildren<DissolveMat>();
            _dissolveMat.SetDissolveHeightMin();
            _dissolveMat.StartCreateDissolve();
        }

        public override void StateUpdate()
        {
            if (_dissolveMat.Percent >= 1)
            {
                _stateMachine.ChangeState(_stateMachine.StateDictionary[(int)StateType.Idle]);
            }
        }
    }

    private class RunState : State
    {
        private readonly Transform _treeSpiritTransform;

        public RunState(GameObject gameObject) : base(gameObject)
        {
        }

        public override void StateStart()
        {
        }

        public override void StateUpdate()
        {
        }
    }

    private class HitState : State
    {
        public HitState(GameObject gameObject) : base(gameObject)
        {
        }

        public override void StateStart()
        {
            _gameObject.GetComponent<Animator>().SetTrigger("Hit");
            _gameObject.GetComponentInChildren<DissolveMat>().StartDestroyDissolve();
        }

        public override void StateUpdate()
        {
            if (_gameObject.GetComponentInChildren<DissolveMat>().Percent <= 0)
            {
                FindObjectOfType<EnemySpawner>().Delete(_gameObject);
            }
        }
    }

    private class AttackState : State
    {

        public AttackState(GameObject gameObject) : base(gameObject)
        {
        }

        public override void StateStart()
        {
            Attack();
        }

        public override void StateUpdate()
        {

        }

        private void Attack()
        {
            _gameObject.GetComponent<Animator>().SetTrigger("Attack");
        }
    }

    private class DeathState : State
    {
        public DeathState(GameObject gameObject) : base(gameObject)
        {
        }

        public override void StateStart()
        {

        }

        public override void StateUpdate()
        {

        }
    }
}