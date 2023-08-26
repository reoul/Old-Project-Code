using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : Singleton<CardManager>
{
    [SerializeField] private GameObject _card222Prefab;
    [SerializeField] private GameObject _card33Prefab;
    [SerializeField] private GameObject _card6Prefab;
    
    [SerializeField] private Transform _cardParent;
    [SerializeField] private List<Card> _cards;
    
    /// <summary> 카드를 생성한다 </summary>
    /// <param name="infos">생성할 카드 정보 리스트</param>
    /// <param name="scale">카드 크기</param>
    /// <param name="createDelay">카드간의 생성 딜레이 시간</param>
    /// <returns>생성된 카드 개수</returns>
    public int CreateCards(List<EventCardInfo> infos, Vector3 scale, float createDelay = 0)
    {
        StartCoroutine(CreateCardsCoroutine(infos, scale, createDelay));
        return infos.Count;
    }

    private IEnumerator CreateCardsCoroutine(List<EventCardInfo> infos, Vector3 scale, float createDelay = 0)
    {
        List<GameObject> createCardObjs = new List<GameObject>();
        PositionSorterInfo sorterInfo = new PositionSorterInfo()
        {
            CardWidth = 225 * scale.x,
            CardHeight = 330 * scale.y,
            CardPaddingX = 20,
            CardPaddingY = 20,
            DicePaddingX = 0
        };

        int colummMaxCount = infos.Count > 5 ? infos.Count / 2 + infos.Count % 2 : infos.Count;
        List<Vector3> posList = PositionSorter.SortCard(infos.Count, colummMaxCount, sorterInfo);

        for (int i = 0; i < infos.Count; ++i) // 생성 가능한 카드 코드를 가지고 미리 카드 생성 후 Active 끄기
        {
            GameObject cardObj;
            Vector3 pos = posList[i] + new Vector3((sorterInfo.CardWidth + sorterInfo.CardPaddingX) / 2, 0, 0);
            
            switch (infos[i].Type)
            {
                case EventCardType.Two:
                {
                    cardObj = Instantiate(_card222Prefab, _cardParent);
                    CardBase222 card = cardObj.GetComponent<CardBase222>();
                    card.Name = infos[i].Title;
                    card.Description12 = infos[i].Context1;
                    card.Description34 = infos[i].Context2;
                    card.Description56 = infos[i].Context3;

                    card.EffectInfoList12 = infos[i].CardEffectInfos1;
                    card.EffectInfoList34 = infos[i].CardEffectInfos2;
                    card.EffectInfoList56 = infos[i].CardEffectInfos3;
                }
                    break;
                case EventCardType.Three:
                {
                    cardObj = Instantiate(_card33Prefab, _cardParent);
                    CardBase33 card = cardObj.GetComponent<CardBase33>();
                    card.Name = infos[i].Title;
                    card.Description123 = infos[i].Context1;
                    card.Description456 = infos[i].Context2;

                    card.EffectInfoList123 = infos[i].CardEffectInfos1;
                    card.EffectInfoList456 = infos[i].CardEffectInfos2;
                }
                    break;
                case EventCardType.Six:
                {
                    cardObj = Instantiate(_card6Prefab, _cardParent);
                    CardBase6 card = cardObj.GetComponent<CardBase6>();
                    card.Name = infos[i].Title;
                    card.Description = infos[i].Context6;
                    card.EffectType = infos[i].EventCardEffectType6;
                }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            cardObj.transform.localPosition = pos;
            cardObj.transform.localScale = scale;
            cardObj.SetActive(false);
            _cards.Add(cardObj.GetComponent<Card>());
            createCardObjs.Add(cardObj);
        }

        WaitForSeconds waitDelay = new WaitForSeconds(createDelay);
        foreach (GameObject cardObj in createCardObjs) // 딜레이 시간동안 하나씩 키기
        {
            cardObj.SetActive(true);
            yield return waitDelay;
        }
    }

    /// <summary> 특정 카드 제거 </summary>
    /// <param name="card">제거할 카드</param>
    public void RemoveCard(Card card)
    {
        Logger.Assert(_cards.Remove(card));
        if (_cards.Count == 0)
        {
            FadeManager.Instance.StartFadeOut();
            StageManager.Instance.SetFadeEvent(StageManager.Instance.NextStageType);
        }
    }

    /// <summary> 현재 생성된 모든 카드 제거 </summary>
    public void RemoveAllCard()
    {
        StopAllCoroutines();
        foreach (Card card in _cards)
        {
            Destroy(card.gameObject);
        }

        _cards.Clear();
    }
}
