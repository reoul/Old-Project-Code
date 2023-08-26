using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IBattleable
{
    GameObject OwnerObj { get; }

    /// <summary> 최대 체력 </summary>
    int MaxHp { get; set; }

    /// <summary> 체력 </summary>
    int Hp { get; set; }

    /// <summary> 공격력 </summary>
    Status OffensivePower { get; set; }

    /// <summary> 방어력 </summary>
    Status DefensivePower { get; set; }

    /// <summary> 관통 데미지 </summary>
    Status PiercingDamage { get; set; }

    /// <summary> 가장 최근 입힌 데미지 </summary>
    int LastAttackDamage { get; set; }

    /// <summary> 정보창 </summary>
    public InfoWindow InfoWindow { get; set; }
    
    public Animator Animator { get; }

    /// <summary> 공격 </summary>
    void Attack();

    /// <summary> 데미지를 준다 </summary>
    /// <param name="damage">데미지</param>
    void ToDamage(int damage);

    /// <summary> 관통 데미지를 준다 </summary>
    /// <param name="piercingDamage">관통 데미지</param>
    void ToPiercingDamage(int piercingDamage);

    /// <summary> 방어력 설정 </summary>
    void SetDefensivePower(int defensivePower);

    /// <summary> 체력을 회복한다 </summary>
    /// <param name="heal">힐</param>
    void ToHeal(int heal);

    /// <summary> Attack 애니메이션 시작 </summary>
    void StartAttackAnimation();

    /// <summary> Hit 애니메이션 시작 </summary>
    void StartHitAnimation();

    /// <summary> Dead 애니메이션 시작 </summary>
    void StartDeadAnimation();

    /// <summary> 공격 애니메이션이 끝났을 때 </summary>
    void FinishAttackAnimation();

    /// <summary> 공격이 끝났을 때 발동할 이벤트 </summary>
    UnityEvent FinishAttackEvent { get; set; }

    /// <summary> 피격 당했을 때 이벤트 </summary>
    UnityEvent HitEvent { get; set; }
}
