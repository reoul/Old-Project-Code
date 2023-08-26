using System;
using TMPro;
using UnityEngine;

/// <summary> 주사위 눈금 1 ~ 3, 4 ~ 6 발동 카드 </summary>
public sealed class CardBase33 : Card
{
    public string Name { get; set; }
    public override string GetName() => Name;

    /// <summary> 주사위 눈금 1 ~ 3번 발동 효과 설명 </summary>
    public string Description123 { get; set; }

    /// <summary> 주사위 눈금 4 ~ 6번 발동 효과 설명 </summary>
    public string Description456 { get; set; }

    public override string GetDescription() => $"1~3: {Description123}\n" +
                                               $"4~6: {Description456}\n";

    /// <summary> 주사위 눈금 1 ~ 3번 발동 효과 정보 배열 </summary>
    public EventCardEffectInfo[] EffectInfoList123;

    /// <summary> 주사위 눈금 4 ~ 6번 발동 효과 정보 배열 </summary>
    public EventCardEffectInfo[] EffectInfoList456;

    [SerializeField] private TMP_Text _descriptionText123;
    [SerializeField] private TMP_Text _descriptionText456;
    
    public override void SetContentText()
    {
        _descriptionText123.text = $"1~3: {Description123}";
        _descriptionText456.text = $"4~6: {Description456}";
    }
    
    public override void Use(Dice dice)
    {
        string description;
        string tmpStr, applyDescription = "";

        switch (dice.Number)
        {
            case EDiceNumber.One:
            case EDiceNumber.Two:
            case EDiceNumber.Three:
                foreach (EventCardEffectInfo effectInfo in EffectInfoList123)
                {
                    tmpStr = ApplyEffect(effectInfo.EventCardEffectType, (int) effectInfo.Num);
                    applyDescription += $"{tmpStr}";
                }

                description = Description123;
                break;
            case EDiceNumber.Four:
            case EDiceNumber.Five:
            case EDiceNumber.Six:
                foreach (EventCardEffectInfo effectInfo in EffectInfoList456)
                {
                    tmpStr = ApplyEffect(effectInfo.EventCardEffectType, (int) effectInfo.Num);
                    applyDescription += $"{tmpStr}";
                }

                description = Description456;
                break;
            case EDiceNumber.Max:
            default:
                throw new ArgumentOutOfRangeException();
        }

        Logger.Log($"카드 사용 완료, 이름 : {Name}, 주사위 눈금 : {(int) dice.Number}, 사용된 효과 : {description} \n{GetDescription()}");
        Logger.Log($"카드 사용 효과 결과\n{applyDescription}");

        Logger.Log("카드 삭제 애니메이션 시작");
        StartDestroyAnimation(); // 카드 삭제
    }

    public override void DiceHint(Dice dice, Color color)
    {
        switch (dice.Number)
        {
            case EDiceNumber.One:
            case EDiceNumber.Two:
            case EDiceNumber.Three:
                _descriptionText123.color = color;

                if (color == dice.HintCardColor)
                {
                    _descriptionText123.text = $"1~3: {ChangeColorStr(Description123)}";
                }
                else
                {
                    _descriptionText123.text = $"1~3: {Description123}";
                };
                break;
            case EDiceNumber.Four:
            case EDiceNumber.Five:
            case EDiceNumber.Six:
                _descriptionText456.color = color;

                if (color == dice.HintCardColor)
                {
                    _descriptionText456.text = $"4~6: {ChangeColorStr(Description456)}";
                }
                else
                {
                    _descriptionText456.text = $"4~6: {Description456}";
                }
                break;
            case EDiceNumber.Max:
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
