using System;
using UnityEngine;

public class AutoAim : MonoBehaviour
{
    public bool IsAutoAnim { get; private set; }
    public GameObject Target { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (IsAutoAnim)
            {
                var bowControllerPos = VRControllerManager.Instance.BowController.transform.position;
                float oldTargetDistance = Vector3.Distance(Target.transform.position, bowControllerPos);
                float newTargetDistance = Vector3.Distance(other.transform.position, bowControllerPos);
                // 새로운 타겟이 더 가깝다면
                if (newTargetDistance < oldTargetDistance)
                {
                    Target = other.gameObject;
                }
            }
            else
            {
                IsAutoAnim = true;
                Target = other.gameObject;
            }
        }
    }

    void Update()
    {
        if(VRControllerManager.Instance.IsCharging)
        {
            this.transform.position = VRControllerManager.Instance.BowController.transform.position;
            var direction = VRControllerManager.Instance.BowController.transform.position - VRControllerManager.Instance.ArrowController.transform.position;
            this.transform.forward = direction;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == Target)
        {
            Target = null;
            IsAutoAnim = false;
        }
    }
}