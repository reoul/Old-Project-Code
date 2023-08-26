using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;


/// <summary>
/// 커스텀 에디터로 만든 작업 툴 <br/>
/// 부모 오브젝트에 스크립트를 넣고 버튼을 누르면 자식 오브젝트까지 다 검사하는 작업 툴
/// </summary>
#if UNITY_EDITOR
[CustomEditor(typeof(ChangeMatTool))]
public class ChangeMatToolEditor : Editor
{
    SerializedProperty matProperty;

    private ChangeMatTool _changeMatTool = null;

    void OnEnable ()
    {
        matProperty = serializedObject.FindProperty ("Mat");
        _changeMatTool = (ChangeMatTool) target;
    }

    public override void OnInspectorGUI ()
    {
        serializedObject.Update ();
        EditorGUILayout.PropertyField(matProperty);
        if (GUILayout.Button("Mat 교체"))
        {
            _changeMatTool.ChangeMat();
        }
        if (GUILayout.Button("랜더러 ShadowCast OFF"))
        {
            _changeMatTool.SetRendererShadowCastingModeOff();
        }
        if (GUILayout.Button("박스 콜리더 삭제"))
        {
            _changeMatTool.RemoveBoxCollider();
        }
        

        serializedObject.ApplyModifiedProperties ();
    }
}
#endif

public class ChangeMatTool : MonoBehaviour
{
    public Material Mat;

    /// <summary> 자식의 모든 DissolveMatAll을 찾아 Mat을 바꿔줌 </summary>
    public void ChangeMat()
    {
        foreach (var dissolveMatAll in GetComponentsInChildren<DissolveMatAll>(true))
        {
            dissolveMatAll.ChangeMat(Mat);
        }
    }

    /// <summary> 모든 자식의 Renderer에서 shadowCastingMode를 Off해줌 </summary>
    public void SetRendererShadowCastingModeOff()
    {
        foreach (var Renderer in GetComponentsInChildren<Renderer>(true))
        {
            Renderer.shadowCastingMode = ShadowCastingMode.Off;
        }
    }

    /// <summary> 모든 자식의 BoxCollider를 제거함 </summary>
    public void RemoveBoxCollider()
    {
        foreach (var boxCollider in GetComponentsInChildren<BoxCollider>(true))
        {
            DestroyImmediate(boxCollider);
        }
    }
}
