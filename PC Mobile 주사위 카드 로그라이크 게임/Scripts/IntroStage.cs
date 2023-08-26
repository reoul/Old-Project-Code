public sealed class IntroStage : Stage
{
    public override void StageEnter()
    {
    }

    public override void StageUpdate()
    {
    }

    public override void StageExit()
    {
    }

    public void StartEasyGame()
    {
        StageManager.Instance.Difficulty = Difficulty.Easy;
        StageManager.Instance.SetRandomStage();
        StageManager.Instance.NextStage();
    }
    
    public void StartNormalGame()
    {
        StageManager.Instance.Difficulty = Difficulty.Normal;
        StageManager.Instance.SetRandomStage();
        StageManager.Instance.NextStage();
    }
    
    public void StartHardGame()
    {
        StageManager.Instance.Difficulty = Difficulty.Hard;
        StageManager.Instance.SetRandomStage();
        StageManager.Instance.NextStage();
    }
}
