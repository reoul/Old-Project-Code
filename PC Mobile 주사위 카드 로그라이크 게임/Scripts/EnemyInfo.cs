using System;
using UnityEditor;
using UnityEngine;
public enum EnemyRatingType
{
    [InspectorName("이지")] Easy,
    [InspectorName("노말")] Normal,
    [InspectorName("하드")] Hard
}

#if UNITY_EDITOR
[CustomEditor(typeof(EnemyInfo))]
public class EnemyInfoInspector : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        
        EditorGUILayout.PropertyField(serializedObject.FindProperty("Prefab"), new GUIContent("프리팹"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("Name"), new GUIContent("이름"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("MaxHp"), new GUIContent("최대 체력"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("Hp"), new GUIContent("체력"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("OffensivePower"), new GUIContent("공격력"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("DefensivePower"), new GUIContent("방어력"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("PiercingDamage"), new GUIContent("관통 데미지"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("ratingType"), new GUIContent("등급"));

        serializedObject.ApplyModifiedProperties();
    }
}
#endif

[Serializable]
[CreateAssetMenu(fileName = "EnemyInfo", menuName = "StageInfo/EnemyInfo", order = int.MaxValue)]
public class EnemyInfo : ScriptableObject
{
    public GameObject Prefab;
    public string Name;
    public int MaxHp;
    public int Hp;
    public int OffensivePower;
    public int DefensivePower;
    public int PiercingDamage;
    public EnemyRatingType ratingType;
}
