
using System.Collections.Generic;
using UnityEngine;

public class WendigoState : StateMachine
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
        private DissolveMat[] _dissolveMats;
        private int _index;
        private float _percentSum;

        public SpawnState(GameObject gameObject) : base(gameObject)
        {
        }

        public override void StateStart()
        {
            _dissolveMats = _gameObject.GetComponentsInChildren<DissolveMat>();
            foreach (var dissolveMat in _dissolveMats)
            {
                dissolveMat.SetDissolveHeightMin();
                dissolveMat.StartCreateDissolve();
            }
        }

        public override void StateUpdate()
        {
            _percentSum = 0;
            for (_index = 0; _index < _dissolveMats.Length; _index++)
            {
                _percentSum += _dissolveMats[_index].Percent;
            }
            if (_index <= _percentSum)
            {
                _stateMachine.ChangeState(_stateMachine.StateDictionary[(int) StateType.Run]);
            }
        }
    }

    private class RunState : State
    {
        private readonly Transform _wendigoTransform;

        public RunState(GameObject gameObject) : base(gameObject)
        {
            _wendigoTransform = gameObject.transform;
        }

        public override void StateStart()
        {
            _gameObject.GetComponent<Animator>().SetTrigger("Run");
        }

        public override void StateUpdate()
        {
            float moveSpeed = _gameObject.GetComponent<Enemy>().MoveSpeed;
            _wendigoTransform.forward = Vector3.zero - _wendigoTransform.position;
            _wendigoTransform.position += _wendigoTransform.forward * moveSpeed * Time.deltaTime;

            if (Vector3.Distance(_wendigoTransform.position, Vector3.zero) < 5)
            {
                //ScoreSystem.Score -= _gameObject.GetComponent<Wendigo>().damage;
                _stateMachine.ChangeState(_stateMachine.StateDictionary[(int) StateType.Death]);
            }
        }
    }

    private class HitState : State
    {
        private DissolveMat[] _dissolveMats;
        private int _index;
        private float _percentSum;
        public HitState(GameObject gameObject) : base(gameObject)
        {
        }

        public override void StateStart()
        {
            _gameObject.GetComponent<Animator>().SetTrigger("Hit");
            _dissolveMats = _gameObject.GetComponentsInChildren<DissolveMat>();
            foreach (var dissolveMat in _dissolveMats)
            {
                dissolveMat.StartDestroyDissolve();
            }
        }

        public override void StateUpdate()
        {
            _percentSum = 0;
            for (_index = 0; _index < _dissolveMats.Length; _index++)
            {
                _percentSum += _dissolveMats[_index].Percent;
            }
            if (_index <= _percentSum)
            {
                FindObjectOfType<EnemySpawner>().Delete(_gameObject);
            }
        }
    }

    private class DeathState : State
    {
        private DissolveMat[] _dissolveMats;
        private int _index;
        private float _percentSum;
        
        public DeathState(GameObject gameObject) : base(gameObject)
        {
        }

        public override void StateStart()
        {
            _gameObject.GetComponent<Animator>().SetTrigger("Attack");
            _dissolveMats = _gameObject.GetComponentsInChildren<DissolveMat>();
            foreach (var dissolveMat in _dissolveMats)
            {
                dissolveMat.StartDestroyDissolve(1.4f);
            }
        }

        public override void StateUpdate()
        {
            _percentSum = 0;
            for (_index = 0; _index < _dissolveMats.Length; _index++)
            {
                _percentSum += _dissolveMats[_index].Percent;
            }
            if (0 >= _percentSum)
            {
                FindObjectOfType<EnemySpawner>().Delete(_gameObject);
            }
        }
    }
}