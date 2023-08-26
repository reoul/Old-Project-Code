using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(TreeSpiritState))]
public class TreeSpirit : Enemy
{
    public void Awake()
    {
        _stateMachine = GetComponent<StateMachine>();
        //RandomWeak();
    }

    private void Update()
    {
        _stateMachine.StateUpdate();
    }

    public void ChangeState(TreeSpiritState.StateType state)
    {
        _stateMachine.ChangeState(_stateMachine.StateDictionary[(int)state]);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Arrow"))
        {
            ScoreSystem.Score += 100;
            //_stateMachine.ChangeState(_stateMachine.StateDictionary[(int)TreeSpiritState.StateType.Hit]);
            EffectManager.Instance.CreateEffect(this.transform.position + new Vector3(0, transform.lossyScale.y * 1.7f,0));
            FindObjectOfType<EnemySpawner>().Delete(this.gameObject);
        }
    }

    /*private void RandomWeak()
    {
        _curWeak = weakPoints[Random.Range(0, weakPoints.Length)];
        _curWeak.gameObject.SetActive(true);
    }*/

    /// <summary>
    /// 약점을 변경한다
    /// </summary>
    /*public void ChangeWeakPoint()
    {
        int rand;
        do
        {
            rand = Random.Range(0, weakPoints.Length);
        } while (_curWeak == weakPoints[rand]);
        _curWeak.gameObject.SetActive(false);
        weakPoints[rand].gameObject.SetActive(true);
        _curWeak = weakPoints[rand];
    }*/
}