using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum ItemEffectInvokeTimeType
{
    [InspectorName("전투 시작 시")] BattleStart,
    [InspectorName("전투 종료 시")] BattleFinish,
    [InspectorName("공격 후")] AttackFinish,
    [InspectorName("획득 시")] GetItem,
    [InspectorName("피격 시")] Hit,
}

public enum ItemEffectType
{
    [InspectorName("체력 회복")] Heal,
    [InspectorName("공격력 증가")] OffensivePower,
    [InspectorName("관통 데미지 증가")] PiercingDamage,
    [InspectorName("방어력 증가")] DefensivePower,
    [InspectorName("최대 체력 증가")] MaxHp,
    [InspectorName("골드 획득")] Gold,
    [InspectorName("데미지 2배, 피해량 2배")] DoubleDamage,
    [InspectorName("커스텀 기능")] Custom,
}

public enum ItemRatingType
{
    [InspectorName("레어")] Rare,
    [InspectorName("에픽")] Epic,
    [InspectorName("레전드리")] Legendary
}

#if UNITY_EDITOR
[CustomEditor(typeof(ItemInfo))]
public class ItemInfoInspector : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(serializedObject.FindProperty("Name"), new GUIContent("이름"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("Description"), new GUIContent("설명"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("EffectInvokeTimeType"), new GUIContent("발동 시점"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("EffectType"), new GUIContent("발동 효과 타입"));

        int effectTypeIndex = serializedObject.FindProperty("EffectType").enumValueIndex;
        if (effectTypeIndex < (int)ItemEffectType.DoubleDamage)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("Num"), new GUIContent("수치"));
        }

        if (effectTypeIndex == (int)ItemEffectType.Custom)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("itemObj"), new GUIContent("아이템 오브젝트"));
        }

        EditorGUILayout.PropertyField(serializedObject.FindProperty("Price"), new GUIContent("가격"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("ratingType"), new GUIContent("등급"));

        serializedObject.ApplyModifiedProperties();
    }
}
#endif

[Serializable]
[CreateAssetMenu(fileName = "ItemInfo", menuName = "StageInfo/ItemInfo", order = int.MaxValue)]
public class ItemInfo : ScriptableObject
{
    public string Name;
    public string Description;
    public ItemEffectInvokeTimeType EffectInvokeTimeType;
    public ItemEffectType EffectType;
    public int Num;
    public int Price;
    public ItemRatingType ratingType;
    public GameObject itemObj;

    public ItemInfo(ItemInfo itemInfo)
    {
        Name = itemInfo.Name;
        Description  = itemInfo.Description;
        EffectInvokeTimeType = itemInfo.EffectInvokeTimeType;
        EffectType = itemInfo.EffectType;
        Num = itemInfo.Num;
        Price = itemInfo.Price;
        ratingType = itemInfo.ratingType;
        itemObj = itemInfo.itemObj;
    }

    public override string ToString()
    {
        return $"[{Name}, {Description}, {EffectInvokeTimeType}, {EffectType}, 수치 : {Num}, 가격 : {Price}, 등급 : {ratingType}, 스크립트 오브젝트 : {(itemObj != null ? "있음" : "없음")}]";
    }
}
