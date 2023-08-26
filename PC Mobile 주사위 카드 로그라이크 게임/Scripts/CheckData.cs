using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
using System.Text.RegularExpressions;
#endif

#if UNITY_EDITOR
public class CheckData : EditorWindow
{
    [MenuItem("Window/CheckData")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow<CheckData>();
    }

    private void OnGUI()
    {
        if (GUILayout.Button("이벤트 데이터 검증"))
        {
            CheckEventData();
        }

        if (GUILayout.Button("아이템 데이터 검증"))
        {
            CheckItemData();
        }
    }

    private void CheckEventData()
    {
        string tmp;
        uint sum = 0;
        uint squared = 1;
        foreach (EventStageInfo eventInfo in Resources.LoadAll<EventStageInfo>("StageInfo/EventInfo"))
        {
            foreach (EventCardInfo cardInfo in eventInfo.EventCardInfos)
            {
                switch (cardInfo.Type)
                {
                    case EventCardType.Two:
                        try
                        {
                            tmp = Regex.Replace(cardInfo.Context1, @"\D", "");

                            sum = 0;
                            squared = 1;
                            for (int i = cardInfo.CardEffectInfos1.Length - 1; i >= 0; --i)
                            {
                                sum += cardInfo.CardEffectInfos1[i].Num * squared;
                                squared *= 10;
                            }

                            if (sum != System.Convert.ToUInt32(tmp))
                            {
                                Debug.LogError($"이벤트 데이터가 다름 : [{eventInfo.Title}]의 [{cardInfo.Title}] 카드 1~2 카드");
                            }

                            sum = 0;

                            tmp = Regex.Replace(cardInfo.Context2, @"\D", "");

                            squared = 1;
                            for (int i = cardInfo.CardEffectInfos2.Length - 1; i >= 0; --i)
                            {
                                sum += cardInfo.CardEffectInfos2[i].Num * squared;
                                squared *= 10;
                            }

                            if (sum != System.Convert.ToUInt32(tmp))
                            {
                                Debug.LogError($"이벤트 데이터가 다름 : [{eventInfo.Title}]의 [{cardInfo.Title}] 카드 3~4 카드");
                            }

                            sum = 0;

                            tmp = Regex.Replace(cardInfo.Context3, @"\D", "");

                            squared = 1;
                            for (int i = cardInfo.CardEffectInfos3.Length - 1; i >= 0; --i)
                            {
                                sum += cardInfo.CardEffectInfos3[i].Num * squared;
                                squared *= 10;
                            }

                            if (sum != System.Convert.ToUInt32(tmp))
                            {
                                Debug.LogError($"이벤트 데이터가 다름 : [{eventInfo.Title}]의 [{cardInfo.Title}] 카드 5~6 카드");
                            }

                            sum = 0;
                        }
                        catch
                        {

                        }
                        

                        break;
                    case EventCardType.Three:
                        try
                        {
                            tmp = Regex.Replace(cardInfo.Context1, @"\D", "");
                            sum = 0;
                            squared = 1;
                            for (int i = cardInfo.CardEffectInfos1.Length - 1; i >= 0; --i)
                            {
                                sum += cardInfo.CardEffectInfos1[i].Num * squared;
                                squared *= 10;
                            }

                            if (sum != System.Convert.ToUInt32(tmp))
                            {
                                Debug.LogError($"이벤트 데이터가 다름 : [{eventInfo.Title}]의 [{cardInfo.Title}] 카드 1~3 카드");
                            }

                            sum = 0;

                            tmp = Regex.Replace(cardInfo.Context2, @"\D", "");

                            squared = 1;
                            for (int i = cardInfo.CardEffectInfos2.Length - 1; i >= 0; --i)
                            {
                                sum += cardInfo.CardEffectInfos2[i].Num * squared;
                                squared *= 10;
                            }

                            if (sum != System.Convert.ToUInt32(tmp))
                            {
                                Debug.LogError($"이벤트 데이터가 다름 : [{eventInfo.Title}]의 [{cardInfo.Title}] 카드 4~6 카드");
                            }

                            sum = 0;
                        }
                        catch
                        {

                        }
                        break;
                    case EventCardType.Six:
                        break;
                    default:
                        break;
                }
                
            }

        }
    }

    private void CheckItemData()
    {
        string tmp;
        foreach (ItemInfo itemInfo in Resources.LoadAll<ItemInfo>("StageInfo/ItemInfo"))
        {
            try
            {
                tmp = Regex.Replace(itemInfo.Description, @"\D", "");

                if (itemInfo.Num != System.Convert.ToUInt32(tmp) && itemInfo.EffectType < ItemEffectType.DoubleDamage)
                {
                    Debug.LogError($"아이템 데이터가 다름 : [{itemInfo.Name}] 아이템 : {itemInfo.Num} != {System.Convert.ToUInt32(tmp)}");
                }
            }
            catch
            {
            }
            
        }
    }
}
#endif
