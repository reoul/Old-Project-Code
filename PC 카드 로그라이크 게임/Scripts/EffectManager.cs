using System;
using System.Collections;
using UnityEngine;

public enum EffectObjType
{
    Hit,
    Shield,
    Heal
}

public class EffectManager : Singleton<EffectManager>
{
    public GameObject hitObj;
    public GameObject sheldObj;
    public GameObject healObj;

    private void Awake()
    {
        CheckExistInstanceAndDestroy(this);
    }

    /// <summary> 이펙트 타입에 따른 오브젝트 반환 </summary>
    /// <param name="type">이펙트 타입</param>
    /// <returns>이펙트 오브젝트</returns>
    private GameObject GetEffectObj(EffectObjType type)
    {
        switch (type)
        {
            case EffectObjType.Hit:
                return hitObj;
            case EffectObjType.Shield:
                return sheldObj;
            case EffectObjType.Heal:
                return healObj;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }

    /// <summary> 이펙트 오브젝트 생성 </summary>
    /// <param name="type">타입</param>
    /// <param name="pos">위치</param>
    /// <param name="delay">생성 딜레이 시간</param>
    /// <param name="destoryTime">생성되고 삭제까지 남은 시간</param>
    /// <param name="cnt">생성 개수</param>
    public void CreateEffectObj(EffectObjType type, Vector3 pos, float delay = 0, float destoryTime = 1, int cnt = 1)
    {
        StartCoroutine(DelayAndMultipleCreate(type, pos, delay, destoryTime, cnt));
    }
    
    /// <summary> 이펙트 오브젝트 생성 </summary>
    /// <param name="type">타입</param>
    /// <param name="pos">위치</param>
    /// <param name="delay">생성 딜레이 시간</param>
    /// <param name="destoryTime">생성되고 삭제까지 남은 시간</param>
    /// <param name="cnt">생성 개수</param>
    /// <param name="multipleDelay">생성 간의 딜레이 시간</param>
    /// <returns></returns>
    private IEnumerator DelayAndMultipleCreate(EffectObjType type, Vector3 pos, float delay = 0, float destoryTime = 1,
        int cnt = 1, float multipleDelay = 0.07f)
    {
        yield return new WaitForSeconds(delay);
        for (int i = 0; i < cnt; i++)
        {
            CreateObjAndDestroy(type, pos, destoryTime);
            yield return new WaitForSeconds(multipleDelay);
        }
    }
    
    /// <summary>
    /// 이펙트 생성 그리고 삭제
    /// </summary>
    /// <param name="type">타입</param>
    /// <param name="pos">위치</param>
    /// <param name="destoryTime">생성되고 삭제까지 남은 시간</param>
    private void CreateObjAndDestroy(EffectObjType type, Vector3 pos, float destoryTime = 1)
    {
        var effectObj = Instantiate(GetEffectObj(type), pos, Quaternion.identity);
        effectObj.GetComponent<EffectObj>().Init(type);
        Destroy(effectObj, destoryTime);
    }
}
