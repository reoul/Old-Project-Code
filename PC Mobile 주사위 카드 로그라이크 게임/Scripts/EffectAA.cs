using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectAA : MonoBehaviour
{
    static void ApplyEffect()
    {
        BattleManager.Instance.PlayerBattleable.OffensivePower.ItemStatus += 10;
        BattleManager.Instance.PlayerBattleable.OwnerObj.GetComponent<Player>().ValueUpdater.AddVal(10, ValueUpdater.valType.pow);
    }
}
