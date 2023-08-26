public class Item004 : Item
{
    public override void Active()
    {
        IBattleable PlayerBattleable = BattleManager.Instance.PlayerBattleable;

        PlayerBattleable.Hp -= 3;
        PlayerBattleable.OffensivePower.DefaultStatus += 6;
        PlayerBattleable.InfoWindow.UpdateHpBar(PlayerBattleable.Hp, PlayerBattleable.MaxHp);
        PlayerBattleable.OwnerObj.GetComponent<Player>().ValueUpdater.AddVal(6, ValueUpdater.valType.pow, false);
    }
}
