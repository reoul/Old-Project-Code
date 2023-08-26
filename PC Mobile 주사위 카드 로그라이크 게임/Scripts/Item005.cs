public class Item005 : Item
{
    public override void Active()
    {
        IBattleable PlayerBattleable = BattleManager.Instance.PlayerBattleable;

        PlayerBattleable.Hp += 20;
        PlayerBattleable.OffensivePower.DefaultStatus += 10;
        PlayerBattleable.InfoWindow.UpdateHpBar(PlayerBattleable.Hp, PlayerBattleable.MaxHp);
        PlayerBattleable.OwnerObj.GetComponent<Player>().ValueUpdater.AddVal(6, ValueUpdater.valType.pow, false);
    }
}
