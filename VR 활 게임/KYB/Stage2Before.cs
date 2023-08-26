using UnityEngine;

public class Stage2Before : Stage
{
    public override void StageStart()
    {
        base.StageStart();
        SoundManager.Instance.StopBGM();
    }
}