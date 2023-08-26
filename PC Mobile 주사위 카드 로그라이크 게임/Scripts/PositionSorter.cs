using System.Collections.Generic;
using UnityEngine;

public struct PositionSorterInfo
{
    public float CardWidth { get; set; }
    public float CardHeight { get; set; }
    public float CardPaddingX { get; set; }
    public float CardPaddingY { get; set; }
    public float DicePaddingX { get; set; }
}

public static class PositionSorter
{
    public static List<Vector3> SortCard(int cardCount, float colummMaxCount, PositionSorterInfo sorterInfo)
    {
        Vector3 currentVector3 = Vector3.zero;

        int repeat = (int) Mathf.Ceil(cardCount / colummMaxCount); //반복할 횟수

        currentVector3 += new Vector3(0, ((sorterInfo.CardPaddingY + sorterInfo.CardHeight) * (repeat - 1)) / 2f, 0);

        List<Vector3> positionList = new List<Vector3>(); //반환할 좌표들을 담을 리스트

        for (int i = 0; i < repeat; i++)
        {
            if (i < repeat - 1 || cardCount % colummMaxCount == 0)
            {
                currentVector3.x = -((sorterInfo.CardPaddingX + sorterInfo.CardWidth) * colummMaxCount - 1) / 2f;
            }
            else
            {
                currentVector3.x = -((sorterInfo.CardPaddingX + sorterInfo.CardWidth) * ((cardCount % colummMaxCount) - 1)) / 2f;
            }

            for (int j = 0; j < colummMaxCount; j++)
            {
                positionList.Add(currentVector3);
                currentVector3.x += sorterInfo.CardPaddingX + sorterInfo.CardWidth;
            }

            currentVector3.y -= sorterInfo.CardPaddingY + sorterInfo.CardHeight;
        }

        return positionList;
    }

    public static List<Vector3> SortDice(int diceCount, PositionSorterInfo sorterInfo)
    {
        Vector3 currentVector3 = Vector3.zero;
        currentVector3 -= new Vector3((sorterInfo.DicePaddingX * (diceCount - 1)) / 2f, 0, 0);

        List<Vector3> positionList = new List<Vector3>(); //반환할 좌표들을 담을 리스트

        for (int i = 0; i < diceCount; i++)
        {
            positionList.Add(currentVector3);
            currentVector3 += new Vector3(sorterInfo.DicePaddingX, 0, 0);
        }

        return positionList;
    }
}
