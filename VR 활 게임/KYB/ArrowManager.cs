using System;
using UnityEngine;

public class ArrowManager : Singleton<ArrowManager>
{
    [SerializeField] private Transform ArrowTrans;

    public void ShowArrow(bool isShow)
    {
        ArrowTrans.gameObject.SetActive(isShow);
    }

    public void Shot(Vector3 position, Vector3 direction)
    {
        ArrowTrans.gameObject.SetActive(false);
        RaycastHit[] hits  = Physics.RaycastAll(position, direction, 1000);     // 레이를 쏴서
        foreach (RaycastHit hit in hits)
        {
            hit.collider.GetComponent<IHitable>()?.HitEvent();      // 타격이 가능한 오브젝트가 있다면 HitEvent 호출
        }
    }

    private void Update()
    {
        if (VRControllerManager.Instance.IsCharging)
        {
            ArrowTrans.position = VRControllerManager.Instance.ArrowController.CenterTransform.position;
        }
    }
}
