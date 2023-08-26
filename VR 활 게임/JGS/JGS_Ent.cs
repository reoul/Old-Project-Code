using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JGS_Ent : Enemy
{
    [SerializeField] private Transform _shootPosTransfrom;
    [SerializeField] private GameObject _projectile;

    private Vector3 _startPos;

    public int targetFloor;

    private void Start()
    {
        _stateMachine = this.GetComponent<JGS_EntState>();
        _startPos = transform.position;
        foreach (var item in GetComponentsInChildren<WeakPoint>(true))
        {
            item.changeEvent += ChangeWeak;
            item.Score = DataManager.Instance.Data.EntScore;
        }
        damage = DataManager.Instance.Data.EntDamage;
    }

    private void ChangeWeak()
    {
        this.GetComponentInParent<Stage2EntManager>().ChangeWeakPoint();
    }
    
    public void ChangeState(JGS_EntState.StateType state)
    {
        _stateMachine.ChangeState(_stateMachine.StateDictionary[(int)state]);
    }

    private GameObject _stone;

    private void SpawnStone()
    {
        _stone = GameObject.Instantiate(_projectile);
        _stone.transform.localPosition = Vector3.zero;
        _stone.GetComponent<Projectile_Stone>()._rootTransform = _shootPosTransfrom;
        _stone.GetComponent<Projectile_Stone>()._targetPos = PlayerFloor.Instance.attackTrans[targetFloor].position;
        StartCoroutine(PlayerFloor.Instance.StartAttack(targetFloor, damage));
    }

    public void ThrowStone()
    {
        _stone.GetComponent<Projectile_Stone>().isThrow = true;
        _stone.GetComponent<Projectile_Stone>().isThrownSound = true;
        _stone.GetComponent<Projectile_Stone>().targetFloor = targetFloor;
    }
    
}
