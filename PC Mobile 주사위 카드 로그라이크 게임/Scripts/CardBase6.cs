using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary> 주사위 눈금 1 ~ 6 발동 카드 </summary>
public sealed class CardBase6 : Card
{
    public string Name { get; set; }
    public override string GetName() => Name;

    /// <summary> 모든 주사위 눈금 발동 효과 설명 </summary>
    public string Description { get; set; }

    public override string GetDescription() => Description;

    public EventCardEffectType EffectType { get; set; }
    
    [SerializeField] private TMP_Text _descriptionText;
    
    public override void SetContentText()
    {
        _descriptionText.text = Description;
    }

    public override void Use(Dice dice)
    {
        string applyDescription = ApplyEffect(EffectType, (int) dice.Number);
        Logger.Log($"카드 사용 완료, 이름 : {Name}, 주사위 눈금 : {(int) dice.Number}, 사용된 효과 : {GetDescription()}");
        Logger.Log($"카드 사용 효과 결과\n{applyDescription}");

        Logger.Log("카드 삭제 애니메이션 시작");
        StartDestroyAnimation(); // 카드 삭제
    }

    public override void DiceHint(Dice dice, Color color)
    {
        _descriptionText.color = color;

        if (color == dice.DefaultCardColor)
        {
            _descriptionText.text = Description;
        }
        else
        {
            string tmp = Description.Replace("N", Convert.ToString((int)dice.Number));
            _descriptionText.text = ChangeColorStr(tmp);
        }
    }
}
