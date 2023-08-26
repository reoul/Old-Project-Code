using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : Singleton<SpawnerManager>
{
    public Queue<Transform> usedSpawnTransQueue = new Queue<Transform>();
    public Queue<Transform> unusedSpawnTransQueue = new Queue<Transform>();
    
    private float _spawnDelay;
    private float _currentTime;
    private float _delayTime;

    private int _maxSpawnCount = 5;

    public int CurrentSpawnCount { get; set; }

    private void Start()
    {
        _spawnDelay = DataManager.Instance.Data.WendigoSpawnDelay;
        _maxSpawnCount = DataManager.Instance.Data.WendigoMaxSpawnCount;
    }

    private void Spawn()
    {
        if(unusedSpawnTransQueue.Count <= 0)
        {
            List<Transform> shuffleList = new List<Transform>();
            for (int i = 0; i < usedSpawnTransQueue.Count;)
            {
                shuffleList.Add(usedSpawnTransQueue.Dequeue());
            }
            Utility.ShuffleList(shuffleList);
            foreach (Transform spawner in shuffleList)
            {
                unusedSpawnTransQueue.Enqueue(spawner);
            }
        }

        Transform unUsedTrans = unusedSpawnTransQueue.Dequeue();
        FindObjectOfType<EnemySpawner>().Spawn(unUsedTrans.position);
        CurrentSpawnCount++;
        usedSpawnTransQueue.Enqueue(unUsedTrans);
    }

    public void SpawnerAwake()
    {
        CurrentSpawnCount = 0;
        _spawnDelay = 0.5f;
        _currentTime = 0;
        var spawnTransforms = GetComponentsInChildren<Transform>(true);
        for (int i = 1; i < spawnTransforms.Length; i++)
        {
            unusedSpawnTransQueue.Enqueue(spawnTransforms[i]);
        }
        
        List<Transform> shuffleList = new List<Transform>();
        for (int i = 0; i < unusedSpawnTransQueue.Count;)
        {
            shuffleList.Add(unusedSpawnTransQueue.Dequeue());
        }
        shuffleList.Swap(shuffleList.Count * 3);
        foreach (Transform spawner in shuffleList)
        {
            unusedSpawnTransQueue.Enqueue(spawner);
        }
        GetComponent<EnemySpawner>().Init();
    }

    public void SpawnerUpdate()
    {
        _delayTime += Time.deltaTime;
        if(_delayTime < 5)
        {
            return;
        }
        if (StageManager.Instance.CurStage.IsFinish)
        {
            return;
        }
        _currentTime += Time.deltaTime;
        if ((_currentTime > _spawnDelay) && (CurrentSpawnCount < _maxSpawnCount))
        {
            _currentTime = 0;
            Spawn();
        }
    }
}
