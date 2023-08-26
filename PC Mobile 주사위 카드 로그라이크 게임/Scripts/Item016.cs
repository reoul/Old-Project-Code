using UnityEngine;

public class Item016 : Item
{
    public override void Active()
    {
        IBattleable PlayerBattleable = BattleManager.Instance.PlayerBattleable;
        IBattleable EnemyBattleable = BattleManager.Instance.EnemyBattleable;

        int tmpPow = Mathf.FloorToInt(EnemyBattleable.OffensivePower.FinalStatus * 0.1f);

        PlayerBattleable.OffensivePower.ItemStatus += tmpPow;
        PlayerBattleable.OwnerObj.GetComponent<Player>().ValueUpdater.AddVal(tmpPow, ValueUpdater.valType.pow, false);
    }
}
