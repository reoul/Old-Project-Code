using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public struct BattleEnemyData
{
    public Enemy enemy;
    public Vector3 position;
    public Vector3 scale;
}

public class BattleStage : Stage
{
    public Transform EnemyCreatePos;

    public InfoWindow EnemyInfoWindow;

    public bool IsPlayerWin { get; set; }

    /// <summary> 전투 시작 시 발동 </summary>
    public UnityEvent StartBattleEvent { get; set; }
    
    /// <summary> 전투 종료 시 발동 </summary>
    public UnityEvent FinishBattleEvent { get; set; }
    
    public override void StageEnter()
    {
        Logger.Log("전투 스테이지 입장 로직 시작");

        IBattleable player = BattleManager.Instance.PlayerBattleable;
        player.OffensivePower.ItemStatus = 0;
        player.PiercingDamage.ItemStatus = 0;
        player.DefensivePower.ItemStatus = 0;
        
        Logger.Log("전투 시작 시 아이템 이벤트 시작");
        
        // 전투 시작 아이템 발동
        StartBattleEvent.Invoke();
        
        Logger.Log("전투 시작 시 아이템 이벤트 종료");
        
        Time.timeScale = BattleAttach.Instance.AttachArray[BattleAttach.Instance.CurAttachIndex];
        IsPlayerWin = false;
        
        Logger.Log("전투 스테이지 입장 로직 종료");
    }

    public override void StageUpdate()
    {
    }

    public override void StageExit()
    {
        Logger.Log("전투 스테이지 퇴장 로직 시작");
        IBattleable player = BattleManager.Instance.PlayerBattleable;
        
        // 전투 종료 아이템 발동
        if (IsPlayerWin)
        {
            Logger.Log("플레이어 승리 이벤트 로직 시작");
            ValueUpdater valueUpdater = player.InfoWindow.GetComponent<ValueUpdater>();
            valueUpdater.AddVal(-player.OffensivePower.ItemStatus, ValueUpdater.valType.pow, false);
            valueUpdater.AddVal(-player.PiercingDamage.ItemStatus, ValueUpdater.valType.piercing, false);
            valueUpdater.AddVal(-player.DefensivePower.ItemStatus, ValueUpdater.valType.def, false);
        
            player.OffensivePower.ItemStatus = 0;
            player.PiercingDamage.ItemStatus = 0;
            player.DefensivePower.ItemStatus = 0;
            Logger.Log("플레이어 스텟 복구 완료");
            
            Logger.Log("전투 종료 후 이벤트 발동 시작");
            FinishBattleEvent.Invoke();
            Logger.Log("전투 종료 후 이벤트 발동 종료");
        }
        
        Time.timeScale = 1;
        IsPlayerWin = false;
        Logger.Log("전투 스테이지 퇴장 로직 종료");
    }
}
