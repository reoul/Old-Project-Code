using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public sealed class NomalDice : Dice
{

    public override void Roll()
    {
        Number = (EDiceNumber) Random.Range((int) EDiceNumber.One, (int) EDiceNumber.Max);
        _diceAni.Play($"Roll{(int)Number}",-1,0f);  // 굴리는 애니메이션 호출
        _textObj.GetComponent<TMP_Text>().text = ((int)Number).ToString();
    }
}
