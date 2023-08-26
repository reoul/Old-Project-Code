using UnityEngine;

public class Item010 : Item
{
    public override void Active()
    {
        IBattleable PlayerBattleable = BattleManager.Instance.PlayerBattleable;
        PlayerBattleable.ToHeal(1);
    }
}
