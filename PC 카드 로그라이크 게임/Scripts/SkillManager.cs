﻿using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SKILL_TYPE
{
    Skill1,
    Skill2,
    Skill3,
    Skill4,
    Skill5,
    Skill6
}

public class SkillManager : Singleton<SkillManager>
{
    private bool isOpen;
    [SerializeField] private int page;

    public List<SkillBookPage> pages;
    public List<SkillBookCardButton> bookmarks;
    public List<SkillBookCard> choiceCards;
    public List<SkillBookCard> applyCards;

    public List<GameObject> skillXObjs;
    public bool[] isUseSkill;

    public SkillBookPage ActivePage
    {
        get { return pages[page]; }
    }

    public int ActivePageIndex
    {
        get { return page; }
    }

    private void Awake()
    {
        CheckExistInstanceAndDestroy(this);
    }

    public void Init()
    {
        transform.position = new Vector3(0, 0, -2);
    }

    public void Open() //스킬창 여는 것
    {
        if (isOpen)
        {
            Close();
            return;
        }

        SoundManager.Inst.Play(SKILLBOOKSOUND.OpenBook);
        GameManager.Inst.CloseAllUI();
        isOpen = true;
        transform.GetChild(0).gameObject.SetActive(true);
        SelectPage(page);
        if (MapManager.Inst.tutorialIndex == 3 && SceneManager.GetActiveScene().name == "Tutorial2")
        {
            TutorialManager.Inst.isToturialOpenSkill = true;
            TalkWindow.Inst.SetFlagNext(true);
            TalkWindow.Inst.SetSkip(true);
            TalkWindow.Inst.Index2 = 1;
        }
    }

    public void Close() //스킬창 여는 것
    {
        SoundManager.Inst.Play(SKILLBOOKSOUND.CloseBook);
        InitCard();
        pages[page].gameObject.SetActive(false);
        transform.GetChild(0).gameObject.SetActive(false);
        isOpen = false;
    }

    public void SetCard(SkillBookCard skillBookCard, Card card)
    {
        if (isUseSkill[page])
        {
            return;
        }

        foreach (SkillBookCard choiceCard in choiceCards)
        {
            if (choiceCard.curSelectCard != card)
            {
                continue;
            }

            choiceCard.curSelectCard = null;
            choiceCard.HideCard();
            choiceCard.SetColorAlpha(true);
            choiceCard.GetComponentInChildren<TMP_Text>().text = "+";
        }

        SoundManager.Inst.Play(SKILLBOOKSOUND.CardONBook);
        skillBookCard.frontCard.SetActive(true);
        skillBookCard.OriginNum = card.FinalNum;
        skillBookCard.frontCard.GetComponent<SkillBookCard>().OriginNum =
            skillBookCard.frontCard.GetComponent<SkillBookCard>().isQuestionMark
                ? RandomNum(card.FinalNum)
                : card.FinalNum;

        skillBookCard.SetColorAlpha(false);
        skillBookCard.GetComponentInChildren<TMP_Text>().text = (card.FinalNum + 1).ToString();

        if (skillBookCard.curSelectCard == null)
        {
            card.SetColorAlpha(true);
            skillBookCard.curSelectCard = card;
            skillBookCard.frontCard.GetComponent<SkillBookCard>().curSelectCard = card;
        }
        else
        {
            skillBookCard.curSelectCard.SetColorAlpha(false);
            skillBookCard.curSelectCard = card;
            skillBookCard.curSelectCard.SetColorAlpha(true);
            skillBookCard.frontCard.GetComponent<SkillBookCard>().curSelectCard = card;
        }
    }

    /// <summary>
    /// 입력된 값과 다른 랜던한 값을 리턴해준다.
    /// </summary>
    private int RandomNum(int num)
    {
        int result = 0;
        do
        {
            result = Random.Range(0, 6);
        } while (num == result);

        return result;
    }

    public void ApplyCardAll()
    {
        foreach (SkillBookCard applyCard in applyCards)
        {
            if (applyCard.gameObject.activeInHierarchy)
            {
                applyCard.curSelectCard.SetFinalNum(applyCard.CurNum);
            }
        }

        UseSkill(true);
        InitCard();
    }

    public void SelectPage(int index)
    {
        for (int i = 0; i < pages.Count; i++)
        {
            if (!pages[i].gameObject.activeInHierarchy)
            {
                continue;
            }

            if (i == index)
            {
                return;
            }

            pages[i].Init();
            pages[i].gameObject.SetActive(false);
            break;
        }

        SoundManager.Inst.Play(SKILLBOOKSOUND.TurnPage);
        foreach (SkillBookCardButton bookmark in bookmarks)
        {
            bookmark.gameObject.SetActive(true);
        }

        bookmarks[page].transform.localPosition += new Vector3(-0.09158f, 0, 0);
        page = index;
        bookmarks[page].transform.localPosition += new Vector3(0.09158f, 0, 0);
        pages[page].gameObject.SetActive(true);
        pages[page].Show();
    }

    private void InitCard()
    {
        foreach (SkillBookCard applyCard in applyCards)
        {
            if (applyCard.gameObject.activeInHierarchy)
            {
                applyCard.curSelectCard.SetColorAlpha(false);
            }
        }

        foreach (SkillBookCard choiceCard in choiceCards)
        {
            choiceCard.curSelectCard = null;
            choiceCard.HideCard();
            choiceCard.SetColorAlpha(true);
            choiceCard.GetComponentInChildren<TMP_Text>().text = "+";
        }
    }

    /// <summary>
    /// 스킬들의 사용 횟수를 초기화해준다.
    /// </summary>
    public void InitSkillTime()
    {
        for (int i = 0; i < isUseSkill.Length; i++)
        {
            isUseSkill[i] = false;
            skillXObjs[i].SetActive(false);
        }
    }

    /// <summary>
    /// 현재 페이지 스킬의 사용 여부를 설정해준다.
    /// </summary>
    private void UseSkill(bool use)
    {
        isUseSkill[page] = use;
        skillXObjs[page].SetActive(use);
    }
}
