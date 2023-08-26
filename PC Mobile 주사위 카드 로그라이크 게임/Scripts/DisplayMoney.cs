using UnityEngine;
using System.Collections;
using TMPro;

public class DisplayMoney : MonoBehaviour
{
    private int _displayMoney;
    [SerializeField] private TMP_Text _text;

    public void SetTargetMoney(int money)
    {
        StopAllCoroutines();
        StartCoroutine(SetNewTarget(money));
    }

    private IEnumerator SetNewTarget(int targetMoney)
    {
        int oldMoney = _displayMoney;
        float time = 0;
        float calculTime = 1;
        while(_displayMoney != targetMoney)
        {
            time += Time.deltaTime;
            _displayMoney = (int)Mathf.Lerp(oldMoney, targetMoney, time / calculTime);
            _text.text = _displayMoney.ToString();
            yield return null;
        }
    }
}
