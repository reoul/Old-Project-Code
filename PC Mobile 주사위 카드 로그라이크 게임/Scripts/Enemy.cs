using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public sealed class Enemy : MonoBehaviour, IBattleable
{
    public GameObject OwnerObj => this.gameObject;
    public int MaxHp { get; set; }
    public int Hp { get; set; }
    public Status OffensivePower { get; set; }
    public Status DefensivePower { get; set; }
    public Status PiercingDamage { get; set; }
    public int LastAttackDamage { get; set; }

    public InfoWindow InfoWindow { get; set; }

    public UnityEvent FinishAttackEvent { get; set; }
    public UnityEvent HitEvent { get; set; }
    
    private Animator _animator;
    public Animator Animator => _animator;

    private static readonly int AttackHash = Animator.StringToHash("Attack");
    private static readonly int HitHash = Animator.StringToHash("Hit");
    private static readonly int DeathHash = Animator.StringToHash("Death");

    private void Awake()
    {
        OffensivePower = new Status();
        DefensivePower = new Status();
        PiercingDamage = new Status();

        FinishAttackEvent = new UnityEvent();
        HitEvent = new UnityEvent();

        Hp = MaxHp;
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        InfoWindow.UpdateHpBar(Hp, MaxHp);
        InfoWindow.UpdateOffensivePowerText(OffensivePower.FinalStatus);
        InfoWindow.UpdateDefensivePowerText(DefensivePower.FinalStatus);
        InfoWindow.UpdatePiercingDamageText(PiercingDamage.FinalStatus);
    }

    public void Attack()
    {
        Logger.Log("적 Attack() 시작");

        IBattleable player = BattleManager.Instance.PlayerBattleable;

        int damage = OffensivePower.FinalStatus * (BattleManager.IsDoubleDamage ? 2 : 1);
        LastAttackDamage = damage;
        player.ToDamage(damage);
        player.ToPiercingDamage(PiercingDamage.FinalStatus);

        int effectiveDamage = Mathf.Max(damage - player.DefensivePower.FinalStatus, 0) + PiercingDamage.FinalStatus;   // 유효 데미지
        DamageCounter.Instance.DamageCount(player.OwnerObj.transform.position, effectiveDamage);

        Logger.Log("플레이어 피격 이벤트 시작");

        player.HitEvent.Invoke();

        Logger.Log("플레이어 피격 이벤트 종료");

        if (player.Hp != 0)
        {
            player.StartHitAnimation();
        }
        else
        {
            player.StartDeadAnimation();
        }

        Logger.Log("적 Attack() 종료");

        SoundManager.Instance.PlaySound("AttackSound");
    }
    
    public void ToDamage(int damage)
    {
        damage = damage >= DefensivePower.FinalStatus ? damage - DefensivePower.FinalStatus : 0;
        Hp = Hp - damage > 0 ? Hp - damage : 0;

        InfoWindow.UpdateHpBar(Hp, MaxHp);
        Logger.Log($"적 {name}에게 데미지 {damage} 입힘. 현재 체력 : {Hp.ToString()}", gameObject);
    }

    public void ToPiercingDamage(int piercingDamage)
    {
        Hp = Hp - piercingDamage > 0 ? Hp - piercingDamage : 0;

        InfoWindow.UpdateHpBar(Hp, MaxHp);
        Logger.Log($"적 {name}에게 관통 데미지 {piercingDamage} 입힘. 현재 체력 : {Hp.ToString()}", gameObject);
    }

    public void SetDefensivePower(int defensivePower)
    {
        DefensivePower.DefaultStatus = defensivePower;
        InfoWindow.UpdateDefensivePowerText(DefensivePower.FinalStatus);
        Logger.Log($"적 {name}의 방어력 {DefensivePower.FinalStatus.ToString()}로 설정됨", gameObject);
    }

    public void ToHeal(int heal)
    {
        Hp = Hp + heal < MaxHp ? Hp + heal : MaxHp;

        InfoWindow.UpdateHpBar(Hp, MaxHp);
        Logger.Log($"적 {name}에게 {heal} 힐. 현재 체력 : {Hp.ToString()}", gameObject);
    }

    public void StartAttackAnimation()
    {
        Logger.Assert(_animator != null);

        _animator.SetTrigger(AttackHash);

        Logger.Log("적 Attack Trigger On");
    }

    public void StartHitAnimation()
    {
        Logger.Assert(_animator != null);
        
        if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            _animator.SetTrigger(HitHash);
        }
        
        Logger.Log("적 Hit Trigger On");
    }

    public void StartDeadAnimation()
    {
        Logger.Assert(_animator != null);

        _animator.SetTrigger(DeathHash);
        
        float deathAnimTime = AnimationTime.Duration(AnimationType.Death, _animator);
        Invoke(nameof(FinishDeathAnimation), deathAnimTime);
        
        Logger.Log("적 Death Trigger On");
    }

    public void FinishAttackAnimation()
    {
        // 공격 후 이벤트 발동
        Logger.Log("적 공격 후 이벤트 시작");

        FinishAttackEvent.Invoke();

        Logger.Log("적 공격 후 이벤트 종료");
    }
    
    /// <summary> Death 애니메이션 끝날 때 </summary>
    public void FinishDeathAnimation()
    {
        Logger.Log("적 Death 애니메이션 끝 시작");

        Destroy(this.gameObject);
        FindObjectOfType<BattleStage>().IsPlayerWin = true;
        StageManager.Instance.NextStage();

        Logger.Log("적 Death 애니메이션 끝 종료");
    }
}
