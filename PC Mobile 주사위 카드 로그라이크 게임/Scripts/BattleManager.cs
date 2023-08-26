using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class BattleManager : Singleton<BattleManager>
{
    [SerializeField] private Player _player;
    public IBattleable PlayerBattleable { get; private set; }
    public IBattleable EnemyBattleable { get; private set; }

    public static bool IsDoubleDamage;

    private void Awake()
    {
        Debug.Assert(_player != null);
        
        PlayerBattleable = _player.GetComponent<IBattleable>();
        _player.Init();
        IsDoubleDamage = false;
        Time.timeScale = 1;
    }

    /// <summary> 적 설정 </summary>
    /// <param name="battleable">적 battleable</param>
    public void SetEnemy(IBattleable battleable)
    {
        EnemyBattleable = battleable;
    }

    /// <summary> 전투 시작 </summary>
    public void StartBattle()
    {
        StartCoroutine(BattleCoroutine());
    }

    private IEnumerator BattleCoroutine()
    {
        float attackTime = AnimationTime.Duration(AnimationType.Attack, PlayerBattleable.Animator);
        WaitForSeconds waitPlayerAttackTime = new WaitForSeconds(attackTime * 0.8f);
        WaitForSeconds waitPlayerFinishAnim = new WaitForSeconds(attackTime * 0.2f);
        
        attackTime = AnimationTime.Duration(AnimationType.Attack, EnemyBattleable.Animator);
        WaitForSeconds waitEnemyAttackTime = new WaitForSeconds(attackTime * 0.9f);
        WaitForSeconds waitEnemyFinishAnim = new WaitForSeconds(attackTime * 0.1f);
        
        while (true)
        {
            // 플레이어 공격
            Logger.Log("플레이어 공격 시작");

            PlayerBattleable.StartAttackAnimation();

            yield return waitPlayerAttackTime;
            
            PlayerBattleable.Attack();

            yield return waitPlayerFinishAnim;
            
            PlayerBattleable.FinishAttackAnimation();

            Logger.Log("플레이어 공격 끝");

            if (EnemyBattleable.Hp == 0)
            {
                Logger.Log("적 체력 0");
                break;
            }
            
            // 적 공격
            Logger.Log("적 공격 시작");
            EnemyBattleable.StartAttackAnimation();

            yield return waitEnemyAttackTime;
            
            EnemyBattleable.Attack();

            yield return waitEnemyFinishAnim;
            
            EnemyBattleable.FinishAttackAnimation();

            Logger.Log("적 공격 끝");

            if (PlayerBattleable.Hp == 0)
            {
                Logger.Log("플레이어 체력 0");
                break;
            }
        }
    }
}
