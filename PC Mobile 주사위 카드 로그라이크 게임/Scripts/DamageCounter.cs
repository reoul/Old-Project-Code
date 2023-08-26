using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCounter : Singleton<DamageCounter>
{
    [SerializeField]
    private GameObject _textPref;

    private Queue<DamageTextMover> _objPool;
    private int _maxCount = 20;

    private void Start()
    {
        InitPool();
    }

    private void InitPool()
    {
        _objPool = new Queue<DamageTextMover>();
        for (int i = 0; i < _maxCount; i++)
        {
            GameObject tmpObj = GameObject.Instantiate(_textPref, this.transform);
            tmpObj.transform.localPosition = Vector3.zero;
            tmpObj.SetActive(false);
            _objPool.Enqueue(tmpObj.GetComponent<DamageTextMover>());
        }
    }

    public void DamageCount(Vector3 pos, int damage)
    {
        DamageTextMover tmpObj = _objPool.Dequeue();
        tmpObj.Enable(damage, pos);
        _objPool.Enqueue(tmpObj);
    }
}
