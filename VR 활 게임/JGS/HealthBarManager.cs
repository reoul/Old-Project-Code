using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarManager : Singleton<HealthBarManager>
{
    [SerializeField]
    private GameObject _healthBlock;

    [SerializeField]
    private int _maxHealth;

    [SerializeField] private int _playerMaxHealth;
    public int PlayerHealth => _playerHealthStack.Count;
    public int GolemHealth => _healthStack.Count;

    private Stack<GameObject> _healthStack;
    private Stack<GameObject> _playerHealthStack;

    private Color[] color;
    private float baseWidth = 42.5f;

    public Transform bossTextTrans;
    public Transform playerTextTrans;

    private Vector3 _playerTextDefaultPos;
    private Vector3 _playerTextMovePos;

    public bool IsPlayerInvin;

    private void Awake()
    {
        _healthStack = new Stack<GameObject>();
        _playerHealthStack = new Stack<GameObject>();
        _playerTextDefaultPos = playerTextTrans.transform.localPosition;
        _playerTextMovePos = _playerTextDefaultPos + Vector3.up * 55;
    }

    private void Start()
    {
        _playerMaxHealth = DataManager.Instance.Data.PlayerMaxHP;
    }

    public void Init()
    {
        color = new Color[2] { new Color(0.9960784f, 0.3035352f, 0.282353f), new Color(0.282353f, 0.5973283f, 0.9960784f) };

        _maxHealth = DataManager.Instance.Data.GolemMaxHealth;


        while (_healthStack.Count > 0)
        {
            Destroy(_healthStack.Pop());
        }

        while (_playerHealthStack.Count > 0)
        {
            Destroy(_playerHealthStack.Pop());
        }

        for (int i = 0; i < _maxHealth; i++)
        {
            GameObject _tempHealthBlock = GameObject.Instantiate(_healthBlock, bossTextTrans);
            RectTransform _tempRect = _tempHealthBlock.GetComponent<RectTransform>();
            _tempHealthBlock.GetComponent<Image>().color = color[1];
            _tempRect.sizeDelta = new Vector2((20f / _maxHealth) * baseWidth, _tempRect.sizeDelta.y);
            _tempHealthBlock.transform.localPosition = new Vector3(i * _tempRect.sizeDelta.x, 0, 0);
            _healthStack.Push(_tempHealthBlock);
        }

        for (int i = 0; i < _playerMaxHealth; i++)
        {
            GameObject _tempHealthBlock = GameObject.Instantiate(_healthBlock, playerTextTrans);
            RectTransform _tempRect = _tempHealthBlock.GetComponent<RectTransform>();
            _tempHealthBlock.GetComponent<Image>().color = color[0];
            _tempRect.sizeDelta = new Vector2((20f / _maxHealth) * baseWidth, _tempRect.sizeDelta.y);
            _tempHealthBlock.transform.localPosition = new Vector3(i * _tempRect.sizeDelta.x, 0, 0);
            _playerHealthStack.Push(_tempHealthBlock);
        }

        ActiveBossHP(false);
    }

    public void ActiveBossHP(bool active)
    {
        bossTextTrans.gameObject.SetActive(active);
        playerTextTrans.transform.localPosition = active ? _playerTextDefaultPos : _playerTextMovePos;
    }

    public void DistractDamage()
    {
        if (_healthStack.Count == 0)
        {
            return;
        }
        
        if (_healthStack.Peek().GetComponent<Image>().color == color[1])
        {
            _healthStack.Peek().GetComponent<Image>().color = color[0];
        }
        else
        {
            Destroy(_healthStack.Pop());
        }
    }

    public void DistractPlayerDamage()
    {
        if (IsPlayerInvin || PlayerHealth == 0)
        {
            return;
        }

        if(StageManager.Instance.CurStage.IsFinish)
        {
            return;
        }    
        
        if (_playerHealthStack.Peek().GetComponent<Image>().color == color[1])
        {
            _playerHealthStack.Peek().GetComponent<Image>().color = color[0];
        }
        else
        {
            Destroy(_playerHealthStack.Pop());
        }

        FindObjectOfType<AttackFade>().ShowAttackFade();
        SoundManager.Instance.sfxPlayer5.GetComponent<AudioSource>().pitch = 1.5f;
        SoundManager.Instance.PlaySoundFive("HeartBeat1_out", (StageManager.Instance.CurStageType == StageManager.StageType.Stage1) ? 3 : 2) ;
    }
}
