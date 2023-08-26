using System;
using UnityEditor;
using UnityEngine;

public enum EventCardEffectType
{
    [InspectorName("공격력 증가")] AddOffensivePower,
    [InspectorName("공격력 감소")] SubOffensivePower,

    [InspectorName("관통 데미지 증가")] AddPiercingDamage,
    [InspectorName("관통 데미지 감소")] SubPiercingDamage,

    [InspectorName("최대 체력 증가")] AddMaxHp,
    [InspectorName("최대 체력 감소")] SubMaxHp,

    [InspectorName("체력 증가")] AddHp,
    [InspectorName("체력 감소")] SubHp,

    [InspectorName("방어력 증가")] AddDefensivePower,
    [InspectorName("방어력 감소")] SubDefensivePower,

    [InspectorName("효과 없음")] NoEffect,
    
    [InspectorName("돈 증가")] AddMoney,
    [InspectorName("돈 감소")] SubMoney,
    
    [InspectorName("돈 제곱 증가")] AddGoldSquared,
    [InspectorName("돈 제곱 감소")] SubGoldSquared,

    [InspectorName("체력 제곱 증가")] AddHpSquared,
    [InspectorName("체력 제곱 감소")] SubHpSquared,
}

public enum EventCardType
{
    [InspectorName("12, 34, 56")] Two,
    [InspectorName("123, 456")] Three,
    [InspectorName("N")] Six
}

public enum EventRatingType
{
    [InspectorName("나쁜")] Bad,
    [InspectorName("레어")] Rare,
    [InspectorName("에픽")] Epic,
    [InspectorName("레전드리")] Legendary
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(EventCardEffectInfo))]
public class EventCardEffectInfoDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // IndentLevel => visual sudio tab 공간 띄우기랑 비슷하다고 보면됨.
        int oldIndentLevel = EditorGUI.indentLevel;
        label = EditorGUI.BeginProperty(position, label, property);
        Rect contentPosition = position;

        //Inspector ▼ <= 모양의 여닫기 하는 방법
        if (property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, label))
        {
            if (position.height > 20f)
            {
                position.height = 16f;
                EditorGUI.indentLevel += 1;
                contentPosition = EditorGUI.IndentedRect(position);
                contentPosition.y += 18f;
            }

            EditorGUI.indentLevel = 3;
            EditorGUILayout.BeginVertical();
            //각 변수 한개씩 다 해주어야함. 원하는 위치에 하고 싶을 경우 EditorGUI.PropertyField() 사용
            //Rect는 position 를 변형하여 사용 해주면됨
            EditorGUILayout.PropertyField(property.FindPropertyRelative("EventCardEffectType"), new GUIContent("타입"));
            if (property.FindPropertyRelative("EventCardEffectType").enumValueIndex != (int) EventCardEffectType.NoEffect) // 효과 없음이면 값 안뜨게
            {
                EditorGUILayout.PropertyField(property.FindPropertyRelative("Num"), new GUIContent("값"));
            }

            EditorGUILayout.Space();
            EditorGUILayout.Space();

            //EditorGUI.indentLevel = indent;
            EditorGUI.indentLevel = oldIndentLevel;
            EditorGUILayout.EndVertical();
        }
    }
}
#endif

[Serializable]
public struct EventCardEffectInfo
{
    public EventCardEffectType EventCardEffectType;
    public uint Num;
}


#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(EventCardInfo))]
public class EventCardInfoDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        const string effectDescriptionStr = "설명";

        // IndentLevel => visual sudio tab 공간 띄우기랑 비슷하다고 보면됨.
        int oldIndentLevel = EditorGUI.indentLevel;
        label = EditorGUI.BeginProperty(position, label, property);
        Rect contentPosition = position;

        //Inspector ▼ <= 모양의 여닫기 하는 방법
        if (property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, label))
        {
            if (position.height > 20f)
            {
                position.height = 16f;
                EditorGUI.indentLevel += 1;
                contentPosition = EditorGUI.IndentedRect(position);
                contentPosition.y += 18f;
            }

            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 1;
            EditorGUILayout.BeginVertical();
            //각 변수 한개씩 다 해주어야함. 원하는 위치에 하고 싶을 경우 EditorGUI.PropertyField() 사용
            //Rect는 position 를 변형하여 사용 해주면됨
            EditorGUILayout.PropertyField(property.FindPropertyRelative("Title"), new GUIContent("카드 이름"));
            EditorGUILayout.PropertyField(property.FindPropertyRelative("Type"), new GUIContent("카드 타입"));
            switch ((EventCardType) property.FindPropertyRelative("Type").enumValueIndex)
            {
                case EventCardType.Two:
                    EditorGUI.indentLevel = 1;
                    EditorGUILayout.Space();
                    EditorGUILayout.LabelField("[12] 효과");

                    EditorGUI.indentLevel = 2;
                    EditorGUILayout.PropertyField(property.FindPropertyRelative("Context1"), new GUIContent(effectDescriptionStr));
                    DrawList(property.FindPropertyRelative("CardEffectInfos1"));

                    EditorGUI.indentLevel = 1;
                    EditorGUILayout.Space();
                    EditorGUILayout.LabelField("[34] 효과");

                    EditorGUI.indentLevel = 2;
                    EditorGUILayout.PropertyField(property.FindPropertyRelative("Context2"), new GUIContent(effectDescriptionStr));
                    DrawList(property.FindPropertyRelative("CardEffectInfos2"));

                    EditorGUI.indentLevel = 1;
                    EditorGUILayout.Space();
                    EditorGUILayout.LabelField("[56] 효과");

                    EditorGUI.indentLevel = 2;
                    EditorGUILayout.PropertyField(property.FindPropertyRelative("Context3"), new GUIContent(effectDescriptionStr));
                    DrawList(property.FindPropertyRelative("CardEffectInfos3"));
                    EditorGUI.indentLevel = 1;
                    break;

                case EventCardType.Three:
                    EditorGUI.indentLevel = 1;
                    EditorGUILayout.Space();
                    EditorGUILayout.LabelField("[123] 효과");

                    EditorGUI.indentLevel = 2;
                    EditorGUILayout.PropertyField(property.FindPropertyRelative("Context1"), new GUIContent(effectDescriptionStr));
                    DrawList(property.FindPropertyRelative("CardEffectInfos1"));

                    EditorGUI.indentLevel = 1;
                    EditorGUILayout.Space();
                    EditorGUILayout.LabelField("[456] 효과");

                    EditorGUI.indentLevel = 2;
                    EditorGUILayout.PropertyField(property.FindPropertyRelative("Context2"), new GUIContent(effectDescriptionStr));
                    DrawList(property.FindPropertyRelative("CardEffectInfos2"));

                    EditorGUI.indentLevel = 1;
                    break;

                case EventCardType.Six:
                    EditorGUI.indentLevel = 1;
                    EditorGUILayout.Space();
                    EditorGUILayout.LabelField("[N] 효과");

                    EditorGUI.indentLevel = 2;
                    EditorGUILayout.PropertyField(property.FindPropertyRelative("Context6"), new GUIContent(effectDescriptionStr));
                    EditorGUILayout.PropertyField(property.FindPropertyRelative("EventCardEffectType6"), new GUIContent("타입"));

                    EditorGUI.indentLevel = 1;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }


            EditorGUILayout.Space();
            EditorGUILayout.Space();

            //EditorGUI.indentLevel = indent;
            EditorGUI.indentLevel = oldIndentLevel;

            EditorGUILayout.EndVertical();
        }
    }

    private void DrawList(SerializedProperty list)
    {
        //리스트 갯수 표시
        EditorGUILayout.PropertyField(list.FindPropertyRelative("Array.size"), new GUIContent("효과 개수"));
        int Count = list.arraySize;
        for (int i = 0; i < Count; ++i)
        {
            SerializedProperty property = list.GetArrayElementAtIndex(i);
            SerializedProperty enumProperty = property.FindPropertyRelative("EventCardEffectType");
            SerializedProperty numProperty = property.FindPropertyRelative("Num");

            string labelName = $"효과{i + 1} : {enumProperty.enumNames[enumProperty.enumValueIndex]}";
            if (enumProperty.enumValueIndex != (int) EventCardEffectType.NoEffect) // 효과 없음이면 값 안뜨게 설정
            {
                labelName += $"  [{numProperty.intValue.ToString()}]";
            }

            EditorGUILayout.PropertyField(property, new GUIContent(labelName));
        }
    }
}
#endif

[Serializable]
public struct EventCardInfo
{
    public string Title;
    public EventCardType Type;

    public string Context1;
    public EventCardEffectInfo[] CardEffectInfos1;

    public string Context2;
    public EventCardEffectInfo[] CardEffectInfos2;

    public string Context3;
    public EventCardEffectInfo[] CardEffectInfos3;

    // N 타입을 위한 전용 변수
    public string Context6;
    public EventCardEffectType EventCardEffectType6;
}


#if UNITY_EDITOR
[CustomEditor(typeof(EventStageInfo))]
public class EventStageInfoInspector : Editor
{
    private SerializedProperty _property;
    private EventStageInfo _eventStageInfo;

    private void OnEnable()
    {
        _property = serializedObject.FindProperty("EventCardInfos");
        _eventStageInfo = target as EventStageInfo;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("Title"), new GUIContent("이벤트 제목"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("Description"), new GUIContent("이벤트 설명"));

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        GuiLine();
        EditorGUILayout.Space();

        EditorGUILayout.LabelField("이벤트 카드 리스트");
        DrawList(_property); // 사용 카드 리스트 인스펙터에 그려줌

        EditorGUILayout.Space();
        GuiLine();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        
        EditorGUILayout.PropertyField(serializedObject.FindProperty("ratingType"), new GUIContent("등급 타입"));

        serializedObject.ApplyModifiedProperties();

        if (GUI.changed)
        {
            EditorUtility.SetDirty(_eventStageInfo);
        }
    }

    private void DrawList(SerializedProperty list)
    {
        //리스트 갯수 표시
        EditorGUILayout.PropertyField(list.FindPropertyRelative("Array.size"), new GUIContent("이벤트 카드 개수"));
        int Count = list.arraySize;
        for (int i = 0; i < Count; ++i)
        {
            string labelName = $"카드{i + 1} : {list.GetArrayElementAtIndex(i).displayName}";
            EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i), new GUIContent(labelName));
        }
    }

    private void GuiLine(int height = 1)
    {
        Rect rect = EditorGUILayout.GetControlRect(false, height);

        rect.height = height;

        EditorGUI.DrawRect(rect, new Color(0.5f, 0.5f, 0.5f, 1));
    }
}
#endif

[Serializable]
[CreateAssetMenu(fileName = "EventStageInfo", menuName = "StageInfo/EventStageInfo", order = int.MaxValue)]
public class EventStageInfo : ScriptableObject
{
    public string Title;
    [TextArea(5, 15)] public string Description;

    public EventCardInfo[] EventCardInfos;
    
    public EventRatingType ratingType;
}
