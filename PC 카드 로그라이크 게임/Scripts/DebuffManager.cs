﻿using System;
using UnityEngine;

public class DebuffManager : Singleton<DebuffManager>
{
    public DEBUFF_TYPE debuff_type;

    public int turnDamage;
    public int AddForceTurn; // 몬스터 공격력 증가 턴 카운트
    private bool _isAddForce; // 힘 증가 디버프 체크

    public string DebuffString
    {
        get
        {
            switch (debuff_type)
            {
                case DEBUFF_TYPE.Debuff1:
                    return "매턴마다 데미지 1이상 못 넣었을때 플레이어에게 데미지를 2만큼 입힙니다";
                case DEBUFF_TYPE.Debuff2:
                    return "매턴마다 데미지 1이상 못 넣었을때 몬스터의 체력이 2만큼 회복됩니다";
                case DEBUFF_TYPE.Debuff3:
                    return "매턴마다 데미지 1이상 못 넣었을때 몬스터의 다음 약점숫자를 알 수 없게 됩니다";
                case DEBUFF_TYPE.Debuff4:
                    return "매턴마다 데미지 1이상 못 넣었을때 몬스터의 다음 패턴을 알 수 없게 됩니다";
                case DEBUFF_TYPE.Debuff5:
                    return "2턴마다 몬스터의 데미지가 1만큼 증가합니다";
                case DEBUFF_TYPE.Debuff6:
                    return "몬스터가 플레이어에게 넣은 피해만큼 회복합니다";
                case DEBUFF_TYPE.Debuff7:
                    return "매턴마다 몬스터의 방어도가 3씩 쌓입니다";
                case DEBUFF_TYPE.Tutorial:
                    return "아무런 저주가 없습니다";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    private void Awake()
    {
        CheckExistInstanceAndDestroy(this);
    }

    /// <summary> 디버프를 체크해 적용한다 </summary>
    public void CheckDebuff()
    {
        AddForceTurn += _isAddForce ? 1 : 0;
        if (turnDamage == 0)
        {
            switch (debuff_type)
            {
                case DEBUFF_TYPE.Debuff1: //플레이어 데미지
                    Player.Inst.hpbar.Damage(2);
                    break;
                case DEBUFF_TYPE.Debuff2: //몬스터 체력 2 회복
                    EnemyManager.Inst.enemys[0].hpbar.Heal(2);
                    break;
                case DEBUFF_TYPE.Debuff3: //다음 약점 카드 숨김
                    EnemyManager.Inst.enemys[0].isWeaknessHidden = true;
                    break;
                case DEBUFF_TYPE.Debuff4: //다음 몬스터 패턴 숨김
                    EnemyManager.Inst.enemys[0].isPattenHidden = true;
                    break;
            }
        }

        if (AddForceTurn == 2) //2턴마다 공격력 상승
        {
            AddForceTurn = 0;
            EnemyManager.Inst.enemys[0].force++;
        }

        turnDamage = 0;
    }

    /// <summary> 저주 선택시 걸린 디버프를 적용한다 </summary>
    public void ApplyDebuff()
    {
        Init();
        switch (debuff_type)
        {
            case DEBUFF_TYPE.Debuff5: //2턴마다 공격력 상승
                _isAddForce = true;
                break;
            case DEBUFF_TYPE.Debuff6: //플레이어에게 넣은 피해만큼 몬스터 회복
                EnemyManager.Inst.enemys[0].isVampire = true;
                break;
            case DEBUFF_TYPE.Debuff7: //매턴마다 방어도가 3씩 쌓인다
                EnemyManager.Inst.enemys[0].hpbar.SetTurnStartShield(3);
                break;
        }
    }

    private void Init()
    {
        turnDamage = 0;
        AddForceTurn = 0;
        _isAddForce = false;
    }
}
