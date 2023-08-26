using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Stage1 : Stage
{
    public SpawnerManager _spawnerManager;

    public override void StageStart()
    {
        base.StageStart();
        _spawnerManager = transform.GetChild(0).GetComponent<SpawnerManager>();
        _spawnerManager.SpawnerAwake();
        SoundManager.Instance.StopBGM();
        SoundManager.Instance.BGMChange("Straight Fuse - French Fuse", 0.3f);
        StartCoroutine(StageManager.Instance.TimerCoroutine(LimitTime));
    }

    public override void StageUpdate()
    {
        base.StageUpdate();
        _spawnerManager.SpawnerUpdate();
    }

    public override void StageEnd()
    {
        base.StageEnd();
        RemoveEnemy();
    }

    void RemoveEnemy()
    {
        var monsters = GetComponentsInChildren<Wendigo>(true);
        foreach (Wendigo monster in monsters)
        {
            monster.MoveSpeed = 0;
            var dissolveMats = monster.GetComponentsInChildren<DissolveMat>();
            foreach (var dissolveMat in dissolveMats)
            {
                dissolveMat.StartDestroyDissolve();
            }
            Destroy(monster.gameObject, 1.1f);
        }

        var unUsedEnemyQueue = FindObjectOfType<EnemySpawner>().unUsedEnemyQueue;
        int cnt = unUsedEnemyQueue.Count;

        for (int i = cnt; i > 0; i--)
        {
            Destroy(unUsedEnemyQueue.Dequeue(), 1.1f);
        }
    }
}