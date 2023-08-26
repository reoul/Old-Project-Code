using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EventStage : Stage
{
    [SerializeField] private TMP_Text _title, _description, _ratingText;
    
    public override void StageEnter()
    {
        Logger.Log("이벤트 스테이지 입장 로직 시작");
        Logger.Log("이벤트 스테이지 입장 로직 종료");
    }

    public override void StageExit()
    {
        Logger.Log("이벤트 스테이지 퇴장 로직 시작");
        Logger.Log("이벤트 스테이지 퇴장 로직 종료");
    }

    public override void StageUpdate()
    {
    }

    public void InitEvent(string title, string description, EventRatingType ratingType)
    {
        Logger.Log($"이벤트 스테이지 [{title} : {description}] 으로 설정");
        _title.text = title;
        _description.text = description;
        switch (ratingType)
        {
            case EventRatingType.Bad:
                _ratingText.text = "<color=white>노말</color>";
                break;
            case EventRatingType.Rare:
                _ratingText.text = "<#5CC6DB>레어</color>";
                break;
            case EventRatingType.Epic:
                _ratingText.text = "<#C682EF>에픽</color>";
                break;
            case EventRatingType.Legendary:
                _ratingText.text = "<#EFAD47>레전더리</color>";
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(ratingType), ratingType, null);
        }
    }
}
