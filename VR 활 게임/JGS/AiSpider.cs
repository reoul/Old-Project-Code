using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiSpider : MonoBehaviour
{
    private NavMeshAgent _agent;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }


    public void SetTarget(Transform target)
    {
        _agent.SetDestination(target.position);
        if (_agent.isStopped)
        {
            _agent.Resume();
        }
    }

    public void DisableTarget()
    {
        if (!_agent.isStopped)
        {
            _agent.Stop();
        }
    }
}
