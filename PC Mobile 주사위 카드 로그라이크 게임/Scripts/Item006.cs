public class Item006 : Item
{
    public override void Active()
    {
        IBattleable PlayerBattleable = BattleManager.Instance.PlayerBattleable;

        PlayerBattleable.Hp += 2;
        PlayerBattleable.OffensivePower.DefaultStatus += 2;
        PlayerBattleable.InfoWindow.UpdateHpBar(PlayerBattleable.Hp, PlayerBattleable.MaxHp);
        PlayerBattleable.OwnerObj.GetComponent<Player>().ValueUpdater.AddVal(6, ValueUpdater.valType.pow, false);
    }
}
