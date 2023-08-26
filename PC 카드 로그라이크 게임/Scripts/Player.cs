﻿using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Inst;

    public HpBar hpbar;

    private void Awake()
    {
        Inst = this;
    }

    public void AddShield(int shield)
    {
        EffectManager.Inst.CreateEffectObj(EffectObjType.Shield, transform.position + new Vector3(0, 2, -15), 0, 0.7f);
        hpbar.AddShield(shield);
    }

    public void Damage(int damage) //플레이어가 공격 당할때 호출
    {
        hpbar.Damage(damage);
    }

    public void Dead()
    {
        SoundManager.Inst.Play(BATTLESOUND.GameFaild);
        GetComponent<Animator>().SetTrigger("Dead");
        hpbar.UpdateHp();
        TurnManager.Inst.isFinish = true;
    }

    public void DeadAnimationFinish()
    {
        GameManager.Inst.GameOver();
        Destroy(hpbar.gameObject);
        Destroy(gameObject);
    }
}
