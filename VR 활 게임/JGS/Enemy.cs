using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    protected StateMachine _stateMachine;
    public StateMachine StateMachine => _stateMachine;
    public float maxHealth { get; protected set; }
    public float currentHealth { get; protected set; }
    public int damage;
    public int score;

    [SerializeField] private float _moveSpeed;
    public float MoveSpeed
    {
        get { return _moveSpeed; }
        set { _moveSpeed = value; }
    }
    
    //피격 판정
    public override void Damage(float damage)
    {
        OnDamage();
    }

    private void OnDamage() { }

    //UnityEngine

    private void OnEnable()
    {
        gameObject.layer = 10;
        currentHealth = maxHealth;
    }
}

