using UnityEngine;

public class Ending : Stage
{
    public GameObject ScoreDisplay;
    public override void StageStart()
    {
        base.StageStart();
        ScoreDisplay = GameObject.Find("ScoreDisplay");
        VRControllerManager.Instance.Init();
        //ScoreDisplay.SetActive(false);
        HealthBarManager.Instance.IsPlayerInvin = true;
    }

    public override void StageUpdate()
    {
        
    }
    
    public override void StageEnd()
    {
        base.StageEnd();
        HealthBarManager.Instance.IsPlayerInvin = false;
        //ScoreDisplay.SetActive(true);
    }
}