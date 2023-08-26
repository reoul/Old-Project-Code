using UnityEngine;

public class Item001 : Item
{
    public override void Active()
    {
        IBattleable playerBattleable = BattleManager.Instance.PlayerBattleable;

        playerBattleable.ToHeal(Mathf.FloorToInt(playerBattleable.LastAttackDamage * 0.1f));
    }
}
