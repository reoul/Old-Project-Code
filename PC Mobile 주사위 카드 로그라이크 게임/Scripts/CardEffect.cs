using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEffect : MonoBehaviour
{
    /// <summary> 카드에서 보여지는 오브젝트(이미지, 텍스트 모아둔 오브젝트) </summary>
    [SerializeField] private GameObject _cardShowObj;
    
    /// <summary> 카드 생성할 때 애니메이션에서 호출 </summary>
    public void ShowCard()
    {
        _cardShowObj.SetActive(true);
    }

    /// <summary> 카드 삭제할 때 애니메이션에서 호출 </summary>
    public void HideCard()
    {
        _cardShowObj.SetActive(false);
    }

    public void Destroy()
    {
        Card card = transform.GetComponentInParent<Card>();
        CardManager.Instance.RemoveCard(card);
        Destroy(card.gameObject);
    }
}
