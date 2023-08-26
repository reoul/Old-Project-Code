using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_Attack : StateMachineBehaviour
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
        if (attackTime >= 0.3f && attackCheck == true)
        {
            ScoreSystem.Score -= DataManager.Instance.Data.SkeletonDamage;
            SoundManager.Instance.PlaySoundThird("SkeletonAttack", 1f);
            HealthBarManager.Instance.DistractPlayerDamage();
            attackCheck = false;
        }
    }
}
