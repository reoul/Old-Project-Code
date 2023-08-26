using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wendigo_Attack : StateMachineBehaviour
{
    float attackTime = 0;
    bool attackCheck;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        attackCheck = true;
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        attackTime += Time.deltaTime;
        if (attackTime >= 1f && attackCheck == true)
        {
            ScoreSystem.Score -= DataManager.Instance.Data.WendigoDamage;
            SoundManager.Instance.PlaySoundThird("Wendigo_Attack", 1f);
            HealthBarManager.Instance.DistractPlayerDamage();
            attackCheck = false;
        }
    }
}
