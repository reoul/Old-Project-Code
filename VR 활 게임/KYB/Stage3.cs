using UnityEngine;

public class Stage3 : Stage
{
    private Golem _golem;
    private DissolveMat[] _golemDissolveMats;
    public override void StageStart()
    {
        base.StageStart();
        EnemyInit();
        Invoke("GolemSpawn", 5);
        FindObjectOfType<ScoreDisplay>().switchDisplay();
        StartCoroutine(StageManager.Instance.TimerCoroutine(LimitTime));
        HealthBarManager.Instance.ActiveBossHP(true);
        SoundManager.Instance.StopBGM();
        SoundManager.Instance.BGMChange("Beside Me - Patrick Patrikios", 0.2f);
    }

    public override void StageEnd()
    {
        base.StageEnd();
        PlayerFloor.Instance.StopAllAttack();
        RemoveEnemy();
        HealthBarManager.Instance.ActiveBossHP(false);
    }

    /// <summary>
    /// 골렘 찾아주고 초기화 해준다
    /// </summary>
    private void EnemyInit()
    {
        _golem = FindObjectOfType<Golem>();
        _golemDissolveMats = _golem.GetComponentsInChildren<DissolveMat>();
        foreach (var dissolveMat in _golemDissolveMats)
        {
            dissolveMat.SetDissolveHeightMin();
            dissolveMat.State = DissolveMat.DissolveState.Hide;
        }
        _golem.gameObject.SetActive(false);
    }

    private void GolemSpawn()
    {
        foreach (var dissolveMat in _golemDissolveMats)
        {
            dissolveMat.StartCreateDissolve();
        }
        _golem.gameObject.SetActive(true);
        _golem.GetComponent<Golem>().Init();
    }

    private void RemoveEnemy()
    {
        foreach (var stone in FindObjectsOfType<Projectile_Stone>())
        {
            Destroy(stone.gameObject);
        }
        foreach (var dissolveMat in _golemDissolveMats)
        {
            dissolveMat.StartDestroyDissolve();
        }
        _golem.HideWeak();
    }
}