using UnityEngine;

public class LSM_Stage2 : Stage
{
    private JGS_Ent[] _ent;
    private DissolveMat[] _entDissolveMat;
    public override void StageStart()
    {
        base.StageStart();
        EnemyInit();
        Invoke("EntSpawn", 5);
        SoundManager.Instance.StopBGM();
        SoundManager.Instance.BGMChange("Beside Me - Patrick Patrikios", 0.5f);
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
        _ent = new JGS_Ent[3];
        _entDissolveMat = new DissolveMat[3];
        _ent = FindObjectsOfType<JGS_Ent>();
        for(int i = 0; i < 3; i++)
        {
            Debug.Log(_entDissolveMat[i]);
            Debug.Log(_ent[i]);
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
            SoundManager.Instance.PlaySound("magical-futuristic-gate-opening-SBA-300054819-preview", 1f);
        }

    }

    private void RemoveEnemy()
    {
        Stage2EntManager _entManager = FindObjectOfType<Stage2EntManager>();
        _entManager.HideWeak();
        for (int i = 0; i < 3; i++)
        {
            _entDissolveMat[i].StartDestroyDissolve();
        }
    }
}