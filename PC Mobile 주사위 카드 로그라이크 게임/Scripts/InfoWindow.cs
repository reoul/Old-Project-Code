using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoWindow : MonoBehaviour
{
    [SerializeField] private Image _hpBarImg;
    [SerializeField] private TMP_Text _hpText;
    [SerializeField] private TMP_Text _offensivePowerText;
    [SerializeField] private TMP_Text _piercingDamageText;
    [SerializeField] private TMP_Text _defensivePowerText;

    /// <summary> 체력바 업데이트 </summary>
    /// <param name="hp">체력</param>
    /// <param name="maxHp">최대 체력</param>
    public void UpdateHpBar(int hp, int maxHp)
    {
        _hpText.text = $"{hp.ToString()}/{maxHp.ToString()}";
        _hpBarImg.fillAmount = (float)hp / maxHp;
    }

    /// <summary> 공격력 텍스트 업데이트 </summary>
    /// <param name="offensivePower">공격력</param>
    public void UpdateOffensivePowerText(int offensivePower)
    {
        _offensivePowerText.text = offensivePower.ToString();
    }

    /// <summary> 관통 데미지 텍스트 업데이트 </summary>
    /// <param name="piercingDamage">관통 데미지</param>
    public void UpdatePiercingDamageText(int piercingDamage)
    {
        _piercingDamageText.text = piercingDamage.ToString();
    }

    /// <summary> 방어력 텍스트 업데이트 </summary>
    /// <param name="defensive">방어력</param>
    public void UpdateDefensivePowerText(int defensive)
    {
        _defensivePowerText.text = defensive.ToString();
    }
}
