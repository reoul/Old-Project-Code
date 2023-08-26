using UnityEngine;

public class Item007 : Item
{
    public override void Active()
    {
        IBattleable PlayerBattleable = BattleManager.Instance.PlayerBattleable;

        int tmpPow = Mathf.FloorToInt((float)PlayerBattleable.OwnerObj.GetComponent<Player>().Money / 50f) * 2;

        PlayerBattleable.OffensivePower.ItemStatus += tmpPow;

        PlayerBattleable.OwnerObj.GetComponent<Player>().ValueUpdater.AddVal(tmpPow, ValueUpdater.valType.pow, false);
    }
}
