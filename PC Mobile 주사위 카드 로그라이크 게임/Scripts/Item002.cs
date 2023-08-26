using UnityEngine;

public class Item002 : Item
{
    public override void Active()
    {
        IBattleable playerBattleable = BattleManager.Instance.PlayerBattleable;
        IBattleable EnemyBattleable = BattleManager.Instance.EnemyBattleable;

        EnemyBattleable.ToPiercingDamage(Mathf.FloorToInt(playerBattleable.DefensivePower.FinalStatus * 0.3f));
    }
}
