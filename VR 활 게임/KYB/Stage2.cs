using UnityEngine;

public class Stage2 : Stage
{
    private JGS_Ent[] _ent;
    private DissolveMat[] _entDissolveMat;
    public override void StageStart()
    {
        base.StageStart();
        EnemyInit();
        Invoke("EntSpawn", 5);
        StartCoroutine(StageManager.Instance.TimerCoroutine(LimitTime));
        SoundManager.Instance.StopBGM();
        SoundManager.Instance.BGMChange("Stage2_BGM", 0.2f);
    }

    public override void StageEnd()
    {
        base.StageEnd();
        PlayerFloor.Instance.StopAllAttack();
        RemoveEnemy();
    }

    /// <summary>
    /// 골렘 찾아주고 초기화 해준다
    /// </summary>
    private void EnemyInit()
    {
        _ent = new JGS_Ent[3];
        _entDissolveMat = new DissolveMat[3];
        _ent = FindObjectsOfType<JGS_Ent>();
        for(int i = 0; i < 3; i++)
        {
            _entDissolveMat[i] = _ent[i].GetComponentInChildren<DissolveMat>();
            _entDissolveMat[i].SetDissolveHeightMin();
            _entDissolveMat[i].State = DissolveMat.DissolveState.Hide;
            _ent[i].gameObject.SetActive(false);
        }
    }

    private void EntSpawn()
    {
        for (int i = 0; i < 3; i++)
        {
            _entDissolveMat[i].StartCreateDissolve();
            _ent[i].gameObject.SetActive(true);
        }

    }

    private void RemoveEnemy()
    {
        Stage2EntManager _entManager = FindObjectOfType<Stage2EntManager>();
        _entManager.HideWeak();
        for (int i = 0; i < 3; i++)
        {
            _ent[i].ChangeState(JGS_EntState.StateType.Idle);
            _entDissolveMat[i].StartDestroyDissolve();
        }
    }
}