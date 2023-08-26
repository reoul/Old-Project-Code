using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Reflection;

public class ItemCard : MonoBehaviour
{
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _contextText;
    [SerializeField] private Image _gem;
    [SerializeField] private ItemRatingType _rank;
    [SerializeField] private Sprite[] _gemSprite;
    [SerializeField] private TMP_Text _priceText;

    public ItemInfo ItemCardInfo { get; private set; }

    public bool CanBuy => BattleManager.Instance.PlayerBattleable.OwnerObj.GetComponent<Player>().Money >= ItemCardInfo.Price;

    void Start()
    {
        SetCard(_nameText.text, _contextText.text, _rank);
    }

    public void SetInfo(ItemInfo itemInfo)
    {
        ItemCardInfo = itemInfo;
        SetCard(itemInfo.Name, itemInfo.Description, itemInfo.ratingType);
        GetComponent<BuyItemCard>().Init();
        _priceText.text = itemInfo.Price.ToString();
    }

    public void ApplyItem()
    {
        ItemInfo itemInfo = new ItemInfo(ItemCardInfo);
        Logger.Log($"아이템 등록 시작 {itemInfo}");
        
        switch (ItemCardInfo.EffectInvokeTimeType)
        {
            case ItemEffectInvokeTimeType.BattleStart:
                ApplyItemOfBattleStart(itemInfo);
                break;
            case ItemEffectInvokeTimeType.BattleFinish:
                ApplyItemOfBattleFinish(itemInfo);
                break;
            case ItemEffectInvokeTimeType.AttackFinish:
                ApplyItemOfAttackFinish(itemInfo);
                break;
            case ItemEffectInvokeTimeType.GetItem:
                ApplyItemOfGetType(itemInfo);
                break;
            case ItemEffectInvokeTimeType.Hit:
                ApplyItemOfHit(itemInfo);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        Logger.Log("아이템 등록 종료");
    }

    public void SetCard(string inputName, string InputContext, ItemRatingType rank)
    {
        _nameText.text = inputName;
        _contextText.text = InputContext;
        _rank = rank;
        _gem.sprite = _gemSprite[(int) rank];
    }

    public string GetCardName()
    {
        return _nameText.text;
    }

    public string GetContext()
    {
        return _contextText.text;
    }

    public ItemRatingType GetRank()
    {
        return _rank;
    }

    private string ApplyItem(ItemInfo itemInfo, bool isItemStatus, bool isSoundOn)
    {
        IBattleable player = BattleManager.Instance.PlayerBattleable;

        Debug.Assert(player != null);
        
        // 효과 설명 문자열
        string effectDescription;
        // 이전 스텟 관련 정보 문자열
        string previousStatusStr;

        switch (itemInfo.EffectType)
        {
            case ItemEffectType.Heal:
                previousStatusStr = $"체력/최대체력 : {player.Hp}/{player.MaxHp}";
                
                player.ToHeal(itemInfo.Num);
                
                effectDescription = $"체력 {itemInfo.Num} 증가, \n이전 {previousStatusStr}\n이후 체력/최대체력 : {player.Hp}/{player.MaxHp}";
                break;
            case ItemEffectType.OffensivePower:
                previousStatusStr = player.OffensivePower.ToString();
                
                if (isItemStatus)
                {
                    player.OffensivePower.ItemStatus += itemInfo.Num;
                    effectDescription = $"공격력 아이템스텟 {itemInfo.Num} 증가";
                }
                else
                {
                    player.OffensivePower.DefaultStatus += itemInfo.Num;
                    effectDescription = $"공격력 기본스텟 {itemInfo.Num} 증가";
                }

                player.OwnerObj.GetComponent<Player>().ValueUpdater.AddVal(itemInfo.Num, ValueUpdater.valType.pow, isSoundOn);
                
                effectDescription += $", \n이전 {previousStatusStr}\n이후 {player.OffensivePower}";
                break;
            case ItemEffectType.PiercingDamage:
                previousStatusStr = player.PiercingDamage.ToString();

                if (isItemStatus)
                {
                    player.PiercingDamage.ItemStatus += itemInfo.Num;
                    effectDescription = $"관통데미지 아이템스텟 {itemInfo.Num} 증가";
                }
                else
                {
                    player.PiercingDamage.DefaultStatus += itemInfo.Num;
                    effectDescription = $"관통데미지 기본스텟 {itemInfo.Num} 증가";
                }

                player.OwnerObj.GetComponent<Player>().ValueUpdater.AddVal(itemInfo.Num, ValueUpdater.valType.piercing, isSoundOn);
                
                effectDescription += $", \n이전 {previousStatusStr}\n이후 {player.PiercingDamage}";
                break;
            case ItemEffectType.DefensivePower:
                previousStatusStr = player.DefensivePower.ToString();

                if (isItemStatus)
                {
                    player.DefensivePower.ItemStatus += itemInfo.Num;
                    effectDescription = $"방어력 아이템스텟 {itemInfo.Num} 증가";
                }
                else
                {
                    player.DefensivePower.DefaultStatus += itemInfo.Num;
                    effectDescription = $"방어력 기본스텟 {itemInfo.Num} 증가";
                }

                player.OwnerObj.GetComponent<Player>().ValueUpdater.AddVal(itemInfo.Num, ValueUpdater.valType.def, isSoundOn);
                
                effectDescription += $", \n이전 {previousStatusStr}\n이후 {player.DefensivePower}";
                break;
            case ItemEffectType.MaxHp:
                previousStatusStr = $"체력/최대체력 : {player.Hp}/{player.MaxHp}";
                
                player.MaxHp += itemInfo.Num;
                player.Hp += itemInfo.Num;
                player.InfoWindow.UpdateHpBar(player.Hp, player.MaxHp);
                
                effectDescription = $"최대체력 {itemInfo.Num} 증가, \n이전 {previousStatusStr}\n이후 체력/최대체력 : {player.Hp}/{player.MaxHp}";
                break;
            case ItemEffectType.Gold:
                Player player_ = player.OwnerObj.GetComponent<Player>();
                previousStatusStr = $"돈 : {player_.Money}";
                
                player_.Money += itemInfo.Num;
                
                effectDescription = $"돈 {itemInfo.Num} 증가, \n이전 {previousStatusStr}\n이후 돈 : {player_.Money}";

                if (isSoundOn)
                {
                    SoundManager.Instance.PlaySound("MP_Coin Drop (mp3cut.net)", 1);
                }

                break;
            case ItemEffectType.DoubleDamage:
                BattleManager.IsDoubleDamage = true;
                effectDescription = "데미지 2배, 피해량 2배 활성화";
                break;
            case ItemEffectType.Custom:
                itemInfo.itemObj.GetComponent<Item>().Active();
                effectDescription = "커스텀 아이템 적용";
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(itemInfo.EffectType), itemInfo.EffectType, null);
        }

        if (player.Hp == 0)
        {
            Logger.Log("아이템 사용 중에 HP가 0이 되어 게임오버 되었습니다.");
            FadeManager.Instance.StartFadeOut();
            StageManager.Instance.SetFadeEvent(StageType.GameOver);
        }
        
        return effectDescription;
    }

    /// <summary> 전투 시작 시에 발동되는 아이템 적용 </summary>
    /// <param name="effectType">발동 효과 타입</param>
    /// <param name="num">수치</param>
    private void ApplyItemOfBattleStart(ItemInfo itemInfo)
    {
        StageManager.Instance.BattleStage.StartBattleEvent.AddListener(() =>
        {
            Logger.Log($"[전투 시작 시]아이템 발동 {itemInfo}");
            string applyDescription = ApplyItem(itemInfo, true, false);
            Logger.Log($"아이템 사용 효과 결과 : {applyDescription}");
        });
    }

    /// <summary> 전투 종료 시에 발동되는 아이템 적용 </summary>
    /// <param name="effectType">발동 효과 타입</param>
    /// <param name="num">수치</param>
    private void ApplyItemOfBattleFinish(ItemInfo itemInfo)
    {
        StageManager.Instance.BattleStage.FinishBattleEvent.AddListener(() =>
        {
            Logger.Log($"[전투 종료 시]아이템 발동 {itemInfo}");
            string applyDescription = ApplyItem(itemInfo, false, false);
            Logger.Log($"아이템 사용 효과 결과 : {applyDescription}");
        });
    }

    /// <summary> 공격 후 발동되는 아이템 적용 </summary>
    /// <param name="effectType">발동 효과 타입</param>
    /// <param name="num">수치</param>
    private void ApplyItemOfAttackFinish(ItemInfo itemInfo)
    {
        BattleManager.Instance.PlayerBattleable.FinishAttackEvent.AddListener(() =>
        {
            Logger.Log($"[공격 후]아이템 발동 {itemInfo}");
            string applyDescription = ApplyItem(itemInfo, true, false);
            Logger.Log($"아이템 사용 효과 결과 : {applyDescription}");
        });
    }

    /// <summary> 피격 시 발동되는 아이템 적용 </summary>
    /// <param name="effectType">발동 효과 타입</param>
    /// <param name="num">수치</param>
    private void ApplyItemOfHit(ItemInfo itemInfo)
    {
        BattleManager.Instance.PlayerBattleable.HitEvent.AddListener(() =>
        {
            Logger.Log($"[피격 시]아이템 발동 {itemInfo}");
            string applyDescription = ApplyItem(itemInfo, true, false);
            Logger.Log($"아이템 사용 효과 결과 : {applyDescription}");
        });
    }


    /// <summary> 획득 시 발동되는 아이템 적용 </summary>
    /// <param name="effectType">발동 효과 타입</param>
    /// <param name="num">수치</param>
    private void ApplyItemOfGetType(ItemInfo itemInfo)
    {
        Logger.Log($"[즉시]아이템 발동 {itemInfo}");
        string applyDescription = ApplyItem(itemInfo, false, true);
        Logger.Log($"아이템 사용 효과 결과 : {applyDescription}");
    }
}
