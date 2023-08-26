using UnityEngine;

public class Item009 : Item
{
    public override void Active()
    {
        if (0.05f >= Random.Range(0f, 1f))
        {
            IBattleable EnemyBattleable = BattleManager.Instance.EnemyBattleable;

            EnemyBattleable.ToPiercingDamage(9999);
        }
    }
}
