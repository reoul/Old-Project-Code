using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTurret : MonoBehaviour
{

    [SerializeField] Transform _shootPos;
    [SerializeField] GameObject _lazer;
    [SerializeField] GameObject _lazer2;
    [SerializeField] Transform _startPos;
    [SerializeField] Transform _endPos;
    private int _target;
    private float _cooltime = 10f;
    private float _timeStack;
    private int _level;

    private void Start()
    {
        _timeStack = 0;
        _level = 0;
    }

    private void Update()
    {
        if(_level == 1)
        {
            _timeStack += Time.deltaTime;

            if (_timeStack > _cooltime)
            {
                _target = Random.Range(0, 3);
                StartCoroutine(PlayerFloor.Instance.StartAttack(_target, 0));
                _timeStack = 0;
                StartCoroutine(Attack());
            }
        }
    }

    private void Shoot(Vector3 targetPos)
    {
        GameObject lazer = Instantiate(_lazer);
        lazer.transform.position = _shootPos.transform.position;
        lazer.transform.LookAt(targetPos);
    }
    private void Shoot2()
    {
        GameObject lazer = Instantiate(_lazer2);
        lazer.transform.position = _shootPos.transform.position;
        lazer.transform.LookAt(_startPos);
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(1.5f);
        Shoot(PlayerFloor.Instance.floorTransforms[_target].GetChild(0).position);
        PlayerFloor.Instance.StopAttack(_target);

        yield return true;
    }
}
