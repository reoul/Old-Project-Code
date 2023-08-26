using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Stage : MonoBehaviour
{
    /// <summary> 스테이지 입장 시 호출 </summary>
    public abstract void StageEnter();
    
    /// <summary> 스테이지 있는 동안 매 frame마다 호출 </summary>
    public abstract void StageUpdate();
    
    /// <summary> 스테이지 퇴장 시 호출 </summary>
    public abstract void StageExit();
}
