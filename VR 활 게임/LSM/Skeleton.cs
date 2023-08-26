using System;
using UnityEngine;

[RequireComponent(typeof(SkeletonState))]
public class Skeleton : Enemy, IHitable
{
    private void Awake()
    {
        _stateMachine = this.GetComponent<StateMachine>();
    }

    private void Start()
    {
        damage = DataManager.Instance.Data.SkeletonDamage;
        score = DataManager.Instance.Data.SkeletonScore;
    }

    private void Update()
    {
        _stateMachine.StateUpdate();
    }

    public void ChangeState(SkeletonState.StateType state)
    {
        _stateMachine.ChangeState(_stateMachine.StateDictionary[(int)state]);
    }

    public void HitEvent()
    {
        ScoreSystem.Score += score;
        ChangeState(SkeletonState.StateType.Hit);
        EffectManager.Instance.CreateEffect(this.transform.position + new Vector3(0, transform.lossyScale.y * 1.7f,0));
        FindObjectOfType<EnemySpawner>().Delete(this.gameObject);
        SoundManager.Instance.PlaySoundFourth("Wendigo_Dead2", 0.9f);
    }
}