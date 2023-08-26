using UnityEngine;

public class LSM_Stage3 : Stage
{
    private Golem _golem;
    private DissolveMat _golemDissolveMat;
    public override void StageStart()
    {
        base.StageStart();
        EnemyInit();
        Invoke("GolemSpawn", 5);
        SoundManager.Instance.StopBGM();
        SoundManager.Instance.BGMChange("hollywood-trailer-SBA-346722074-preview", 0.5f);
    }

    public override void StageEnd()
    {
        base.StageEnd();
        RemoveEnemy();
    }

    /// <summary>
    /// 골렘 찾아주고 초기화 해준다
    /// </summary>
    private void EnemyInit()
    {
        _golem = FindObjectOfType<Golem>();
        _golemDissolveMat = _golem.GetComponentInChildren<DissolveMat>();
        _golemDissolveMat.SetDissolveHeightMin();
        _golemDissolveMat.State = DissolveMat.DissolveState.Hide;
        _golem.gameObject.SetActive(false);
    }

    private void GolemSpawn()
    {
        _golemDissolveMat.StartCreateDissolve();
        _golem.gameObject.SetActive(true);
    }

    private void RemoveEnemy()
    {
        _golem.HideWeak();
        _golemDissolveMat.StartDestroyDissolve();
    }
}