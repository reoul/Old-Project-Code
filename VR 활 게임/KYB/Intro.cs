using System;
using UnityEngine;
using UnityEngine.UI;

public class Intro : Stage
{
    private void Start()
    {
        Invoke("StageStart", 0.5f);
    }

    public override void StageStart()
    {
        base.StageStart();
        HealthBarManager.Instance.IsPlayerInvin = true;
        SoundManager.Instance.BGMChange("Vital Whales - Unicorn Heads", 0.5f);
        //StageManager.Instance.NextStageObj.SetActive(true);
        StageManager.Instance.GameExitObj.SetActive(true);
        ScoreSystem.Init();
        PlayerFloor.Instance.Init();
        NarrationManager.Instance.Init();
        GameObject.Find("ScoreDisplay").transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        GameObject.Find("ScoreDisplay").transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "SCORE : 0 / 0";
        GameObject.Find("ScoreDisplay").transform.GetChild(0).GetChild(1).GetComponent<Text>().text = "TIME : 0 S";
        BowManager.Instance.Init();
        HealthBarManager.Instance.Init();
        VRControllerManager.Instance.Init();
    }

    public override void StageEnd()
    {
        HealthBarManager.Instance.IsPlayerInvin = false;
    }
}