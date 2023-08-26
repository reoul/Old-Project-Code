using UnityEngine;

public class Item008 : Item
{
    public override void Active()
    {
        IBattleable PlayerBattleable = BattleManager.Instance.PlayerBattleable;

        int tmpDef = Mathf.FloorToInt(PlayerBattleable.OwnerObj.GetComponent<Player>().Money / 50f) * 2;

        PlayerBattleable.DefensivePower.ItemStatus += tmpDef;

        PlayerBattleable.OwnerObj.GetComponent<Player>().ValueUpdater.AddVal(tmpDef, ValueUpdater.valType.def, false);
    }
}
