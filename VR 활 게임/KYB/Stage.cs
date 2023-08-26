using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class Stage : MonoBehaviour
{
    protected List<DissolveMatAll> DissolveEnvironments;

    public bool IsFinish { get; set; }
    
    public int LimitTime { get; set; }
    public int GoalScore { get; set; }
    
    /// <summary>
    /// 주변 사물 소환
    /// </summary>
    public void StageSetUP()
    {
        if (DissolveEnvironments == null || DissolveEnvironments.Count == 0)
        {
            return;
        }
        DissolveEnvironments.Swap(DissolveEnvironments.Count * 3);
        float k = 0;
        int cnt = 0;
        foreach (DissolveMatAll dissolve in DissolveEnvironments)
        {
            cnt++;
            if (cnt > 3)
            {
                k += 0.2f;
                cnt = 0;
            }
            dissolve.SetDissolveHeightMin();
            dissolve.StartCreateDissolve(k);
        }
    }

    /// <summary> 스테이지 입장 시 호출 </summary>
    public virtual void StageStart()
    {
        IsFinish = false;
        DissolveEnvironments = new List<DissolveMatAll>();
        foreach (var dissolveMatAll in GetComponentsInChildren<DissolveMatAll>())
        {
            DissolveEnvironments.Add(dissolveMatAll);
        }
    }

    /// <summary> 스테이지 매 프레임마다 호출 </summary>
    public virtual void StageUpdate() { }

    /// <summary> 스테이지 퇴장 시 호출 </summary>
    public virtual void StageEnd()
    {
        IsFinish = true;
        RemoveEnvironment();
    }

    /// <summary> 주변 사물 제거 </summary>
    private void RemoveEnvironment()
    {
        if (DissolveEnvironments == null)
        {
            return;
        }
        
        foreach (DissolveMatAll dissolve in DissolveEnvironments)
        {
            dissolve.StartDestroyDissolve();
        }
    }
}
