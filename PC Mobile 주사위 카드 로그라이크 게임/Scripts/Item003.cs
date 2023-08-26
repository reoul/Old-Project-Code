public class Item003 : Item
{
    public override void Active()
    {
        IBattleable EnemyBattleable = BattleManager.Instance.EnemyBattleable;

        if(0.1f >= (float)EnemyBattleable.Hp / (float)EnemyBattleable.MaxHp)
        {
            EnemyBattleable.ToPiercingDamage(9999);
        }
    }
}
