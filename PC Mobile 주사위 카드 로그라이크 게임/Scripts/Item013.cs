using UnityEngine;

public class Item013 : Item
{
    public override void Active()
    {
        IBattleable PlayerBattleable = BattleManager.Instance.PlayerBattleable;
        IBattleable EnemyBattleable = BattleManager.Instance.EnemyBattleable;

        EnemyBattleable.ToDamage(Mathf.FloorToInt(PlayerBattleable.DefensivePower.FinalStatus * 2));
    }
}
