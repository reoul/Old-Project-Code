﻿using DG.Tweening;
using System.Collections;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private Notification notificationPanel;

    private GameObject _endingCredit;

    private GameObject EndingCredit
    {
        get
        {
            if (_endingCredit == null)
            {
                _endingCredit = transform.GetChild(0).gameObject;
            }

            return _endingCredit;
        }
    }

    private GameObject _gameOver;

    private GameObject gameOver
    {
        get
        {
            if (_gameOver == null)
            {
                _gameOver = transform.GetChild(1).gameObject;
            }

            return _gameOver;
        }
    }


    private void Awake()
    {
        CheckExistInstanceAndDestroy(this);
    }

    private void Start()
    {
        DOTween.Init(false, true, LogBehaviour.ErrorsOnly);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F12))
        {
            ResetManager.Inst.ResetGame();
        }

        if (Input.GetKeyDown(KeyCode.I) && TopBarManager.Inst != null)
        {
            TopBarManager.Inst.Open(TOPBAR_TYPE.Bag);
        }

        if (Input.GetKeyDown(KeyCode.K) && TopBarManager.Inst.GetIcon(TOPBAR_TYPE.Skill).gameObject.activeInHierarchy &&
            TopBarManager.Inst != null)
        {
            TopBarManager.Inst.Open(TOPBAR_TYPE.Skill);
        }

        if (Input.GetKeyDown(KeyCode.Escape) && TopBarManager.Inst != null)
        {
            TopBarManager.Inst.Open(TOPBAR_TYPE.Setting);
        }

        if (Input.GetKeyDown(KeyCode.O))
            UnityEngine.SceneManagement.SceneManager.LoadScene("Intro");
    }

    public void Notification(string message)
    {
        if (notificationPanel == null)
        {
            notificationPanel = GameObject.Find("MyTurn").GetComponent<Notification>();
        }

        if (notificationPanel != null)
        {
            notificationPanel.Show(message);
        }
    }

    public void CloseAllUI()
    {
        BagManager.Inst.Close();
        SkillManager.Inst.Close();
    }

    public void Ending()
    {
        StartCoroutine(EndingCoroutine());
    }

    private IEnumerator EndingCoroutine()
    {
        yield return StartCoroutine(GhostManager.Inst.ShowGhost());
        for (int i = 0; i < TalkWindow.Inst.TalkLists[14].Count; i++)
        {
            yield return StartCoroutine(TalkWindow.Inst.TalkTypingCoroutine(14, i));
            yield return StartCoroutine(TalkWindow.Inst.CheckFlagIndexCoroutine());
            yield return StartCoroutine(TalkWindow.Inst.CheckFlagNextCoroutine());
        }

        yield return StartCoroutine(TalkWindow.Inst.HideText());
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(EndingCreditCoroutine());
    }

    private IEnumerator EndingCreditCoroutine()
    {
        SoundManager.Inst.Play(BACKGROUNDSOUND.Ending);
        EndingCredit.SetActive(true);
        yield return new WaitForSeconds(5f);
        ResetManager.Inst.ResetGame();
    }

    public void GameOver()
    {
        SoundManager.Inst.Play(BACKGROUNDSOUND.Ending);
        gameOver.SetActive(true);
    }
}
