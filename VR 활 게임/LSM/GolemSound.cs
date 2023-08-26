using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemSound : MonoBehaviour
{
    
    public void GolemRoar()
    {
        SoundManager.Instance.PlaySoundThird("G_Howling", 1f);
    }

    public void GolemDeath()
    {
        SoundManager.Instance.PlaySoundThird("G_Death", 1f);
    }

    public void GolemJumpAttack()
    {
        SoundManager.Instance.PlaySoundThird("G_Attack_2", 1f);
    }

    public void GolemStoneAttack()
    {
        SoundManager.Instance.PlaySoundThird("G_Attack_1", 1f);
    }

    public void GolemHit()
    {
        SoundManager.Instance.PlaySoundThird("G_Hit_2", 1f);
    }

    public void JumpAttackImpact()
    {
        SoundManager.Instance.PlaySoundThird("G_JumpAttack_Impact", 1f);
    }

    public void StoneImpact()
    {
        SoundManager.Instance.PlaySoundThird("G_Rock_Impact", 1f);
    }
}
