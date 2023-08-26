using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class TreeSpiritState : StateMachine
{
    public enum StateType
    {
        Idle,
        Spawn,
        Run,
        Hit,
        Death
    }

    private void Awake()
    {
        InitStateDictionary();
    }

    public override void InitStateDictionary()
    {
        StateDictionary = new Dictionary<int, State>();
        StateDictionary.Add((int) StateType.Idle, new IdleState(gameObject));
        StateDictionary.Add((int) StateType.Spawn, new SpawnState(gameObject));
        StateDictionary.Add((int) StateType.Run, new RunState(gameObject));
        StateDictionary.Add((int) StateType.Hit, new HitState(gameObject));
        StateDictionary.Add((int) StateType.Death, new DeathState(gameObject));
        ChangeState(StateDictionary[(int) StateType.Idle]);
    }

    private class IdleState : State
    {
        public IdleState(GameObject gameObject) : base(gameObject)
        {
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
                _stateMachine.ChangeState(_stateMachine.StateDictionary[(int) StateType.Run]);
            }
        }
    }

    private class RunState : State
    {
        private readonly Transform _treeSpiritTransform;

        public RunState(GameObject gameObject) : base(gameObject)
        {
            _treeSpiritTransform = gameObject.transform;
        }

        public override void StateStart()
        {
            _gameObject.GetComponent<Animator>().SetTrigger("Run");
        }

        public override void StateUpdate()
        {
            float moveSpeed = _gameObject.GetComponent<TreeSpirit>().MoveSpeed;
            _treeSpiritTransform.forward = Vector3.zero - _treeSpiritTransform.position;
            _treeSpiritTransform.position += _treeSpiritTransform.forward * moveSpeed * Time.deltaTime;

            if (Vector3.Distance(_treeSpiritTransform.position, Vector3.zero) < 5)
            {
                ScoreSystem.Score -= 100;
                _stateMachine.ChangeState(_stateMachine.StateDictionary[(int) StateType.Death]);
            }
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

    private class DeathState : State
    {
        public DeathState(GameObject gameObject) : base(gameObject)
        {
        }

        public override void StateStart()
        {
            _gameObject.GetComponent<Animator>().SetTrigger("Attack");
            _gameObject.GetComponentInChildren<DissolveMat>().StartDestroyDissolve(1.05f);
        }

        public override void StateUpdate()
        {
            if (_gameObject.GetComponentInChildren<DissolveMat>().Percent <= 0)
            {
                FindObjectOfType<EnemySpawner>().Delete(_gameObject);
            }
        }
    }
}