using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BuyItemCard : MonoBehaviour
{
    [SerializeField] private GameObject _soldOutObj;
    [SerializeField] private GameObject _soldOutButton;

    public void Init()
    {
        _soldOutObj.SetActive(false);
        _soldOutButton.SetActive(false);
    }
    
    public void BuyCard()
    {
        ItemCard itemCard = GetComponent<ItemCard>();
        if (itemCard.CanBuy)
        {
            BattleManager.Instance.PlayerBattleable.OwnerObj.GetComponent<Player>().Money -= itemCard.ItemCardInfo.Price;
            BackPack.Instance.AddItem(this.gameObject);
            itemCard.ApplyItem();
            _soldOutObj.SetActive(true);
            _soldOutButton.SetActive(true);
            SoundManager.Instance.PlaySound("CoinSound");
        }
    }
}
