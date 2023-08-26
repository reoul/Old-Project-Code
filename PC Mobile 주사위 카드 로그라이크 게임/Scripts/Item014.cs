using UnityEngine;

public class Item014 : Item
{
    public override void Active()
    {
        IBattleable PlayerBattleable = BattleManager.Instance.PlayerBattleable;
        IBattleable EnemyBattleable = BattleManager.Instance.EnemyBattleable;

        PlayerBattleable.MaxHp += Mathf.FloorToInt(EnemyBattleable.MaxHp * 0.05f);
        PlayerBattleable.Hp += Mathf.FloorToInt(EnemyBattleable.MaxHp * 0.05f);

        PlayerBattleable.InfoWindow.UpdateHpBar(PlayerBattleable.Hp, PlayerBattleable.MaxHp);
    }
}
