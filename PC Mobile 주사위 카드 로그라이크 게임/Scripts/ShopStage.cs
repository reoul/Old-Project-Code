using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShopStage : Stage
{
    public List<ItemInfo> RareItemInfoList { get; set; }
    public List<ItemInfo> EpicItemInfoList { get; set; }
    public List<ItemInfo> LegendaryItemInfoList { get; set; }
    public ItemCard[] ItemCards;

    public override void StageEnter()
    {
        Logger.Log("상점 스테이지 입장 로직 시작");
        
        int[] itemRatings = new int[3];
        int[] itemIndexs = new int[3];

        do
        {
            for (int i = 0; i < 3; ++i)
            {
                int rand = Random.Range(0, 100);
                if (rand < 60) // 레어
                {
                    itemRatings[i] = 1000;
                    itemIndexs[i] = Random.Range(0, RareItemInfoList.Count);
                }
                else if (rand < 90) // 에픽
                {
                    itemRatings[i] = 2000;
                    itemIndexs[i] = Random.Range(0, EpicItemInfoList.Count);
                }
                else // 레전더리
                {
                    itemRatings[i] = 3000;
                    itemIndexs[i] = Random.Range(0, LegendaryItemInfoList.Count);
                }
            }
        } while (itemRatings[0] + itemIndexs[0] == itemRatings[2] + itemIndexs[2] ||
                 itemRatings[0] + itemIndexs[0] == itemRatings[1] + itemIndexs[1] ||
                 itemRatings[1] + itemIndexs[1] == itemRatings[2] + itemIndexs[2]);

        List<ItemInfo> itemInfos;
        for (int i = 0; i < 3; ++i)
        {
            switch (itemRatings[i])
            {
                case 1000:
                    itemInfos = RareItemInfoList;
                    break;
                case 2000:
                    itemInfos = EpicItemInfoList;
                    break;
                case 3000:
                    itemInfos = LegendaryItemInfoList;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            ItemCards[i].SetInfo(itemInfos[itemIndexs[i]]);
            Logger.Log($"{i}번째 아이템({itemIndexs[i]}) {itemInfos[itemIndexs[i]]} 으로 설정됨");
        }

        Logger.Log("상점 스테이지 입장 로직 종료");
    }

    public override void StageUpdate()
    {
    }

    public override void StageExit()
    {
        Logger.Log("상점 스테이지 퇴장 로직 시작");
        Logger.Log("상점 스테이지 퇴장 로직 종료");
    }
}
