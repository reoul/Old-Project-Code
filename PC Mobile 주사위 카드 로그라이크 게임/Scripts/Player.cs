using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Status
{
    private int _defaultStatus;

    public int DefaultStatus
    {
        get => _defaultStatus;
        set => _defaultStatus = value > 0 ? value : 0;
    }

    public int ItemStatus { get; set; }
    public int FinalStatus => DefaultStatus + ItemStatus;

    public override string ToString()
    {
        return $"[기본 스텟 : {_defaultStatus}, 아이템 스텟 : {ItemStatus}, 최종 스탯 : {FinalStatus}]";
    }
}

public sealed class Player : MonoBehaviour, IBattleable
{
    public GameObject OwnerObj => this.gameObject;

    private int _maxHp;

    public int MaxHp
    {
        get => _maxHp;
        set
        {
            _maxHp = value;
            Hp = _maxHp > Hp ? Hp : MaxHp;
        }
    }

    private int _hp;

    public int Hp
    {
        get => _hp;
        set => _hp = value > 0 ? value : 0;
    }

    public Status OffensivePower { get; set; }
    public Status DefensivePower { get; set; }
    public Status PiercingDamage { get; set; }
    public int LastAttackDamage { get; set; }


    [SerializeField] private InfoWindow _infoWindow;
    public InfoWindow InfoWindow
    {
        get { return _infoWindow; }
        set { _infoWindow = value; }
    }

    private Animator _animator;
    public Animator Animator => _animator;

    public ValueUpdater ValueUpdater { get; private set; }

    public UnityEvent FinishAttackEvent { get; set; }
    public UnityEvent HitEvent { get; set; }


    [SerializeField] private DisplayMoney _moneySC;

    private int _money;
    private static readonly int AttackHash = Animator.StringToHash("Attack");
    private static readonly int HitHash = Animator.StringToHash("Hit");
    private static readonly int DeathHash = Animator.StringToHash("Death");

    public int Money
    {
        get => _money;
        set
        {
            _money = value > 0 ? value : 0;
            _moneySC.SetTargetMoney(_money);
        }
    }


    public void Init()
    {
        OffensivePower = new Status();
        DefensivePower = new Status();
        PiercingDamage = new Status();

        MaxHp = 300;
        Hp = MaxHp;
        OffensivePower.DefaultStatus = 5;
        PiercingDamage.DefaultStatus = 5;
        DefensivePower.DefaultStatus = 10;

        _animator = GetComponent<Animator>();
        _infoWindow.UpdateHpBar(Hp, MaxHp);

        ValueUpdater = FindObjectOfType<ValueUpdater>(true);

        ValueUpdater.Init();

        ValueUpdater.AddVal(OffensivePower.DefaultStatus, ValueUpdater.valType.pow, false);
        ValueUpdater.AddVal(PiercingDamage.DefaultStatus, ValueUpdater.valType.piercing, false);
        ValueUpdater.AddVal(DefensivePower.DefaultStatus, ValueUpdater.valType.def, false);

        FinishAttackEvent = new UnityEvent();
        HitEvent = new UnityEvent();
        Money = 150;
    }

    public void Attack()
    {
        Logger.Log("플레이어 Attack() 시작");

        IBattleable enemy = BattleManager.Instance.EnemyBattleable;
        
        int damage = OffensivePower.FinalStatus * (BattleManager.IsDoubleDamage ? 2 : 1);
        LastAttackDamage = damage;
        enemy.ToDamage(damage);
        enemy.ToPiercingDamage(PiercingDamage.FinalStatus);

        int effectiveDamage = Mathf.Max(damage - enemy.DefensivePower.FinalStatus, 0) + PiercingDamage.FinalStatus;   // 유효 데미지
        DamageCounter.Instance.DamageCount(enemy.OwnerObj.transform.position, effectiveDamage);

        Logger.Log("적 피격 이벤트 시작");

        enemy.HitEvent.Invoke();

        Logger.Log("적 피격 이벤트 종료");

        if (enemy.Hp != 0)
        {
            enemy.StartHitAnimation();
        }
        else
        {
            enemy.StartDeadAnimation();
            Time.timeScale = 1;
        }

        Logger.Log("플레이어 Attack() 종료");

        SoundManager.Instance.PlaySound("AttackSound");
    }
    
    public void ToDamage(int damage)
    {
        Logger.Assert(_infoWindow != null);

        damage = damage >= DefensivePower.FinalStatus ? damage - DefensivePower.FinalStatus : 0;
        Hp = Hp - damage > 0 ? Hp - damage : 0;

        _infoWindow.UpdateHpBar(Hp, MaxHp);
        Logger.Log($"플레이어 데미지 {damage} 입음. 현재 체력 {Hp.ToString()}", gameObject);
    }

    public void ToPiercingDamage(int piercingDamage)
    {
        Logger.Assert(_infoWindow != null);

        Hp = Hp - piercingDamage > 0 ? Hp - piercingDamage : 0;

        _infoWindow.UpdateHpBar(Hp, MaxHp);
        Logger.Log($"플레이어 관통 데미지 {piercingDamage} 입음. 현재 체력 {Hp.ToString()}", gameObject);
    }

    public void SetDefensivePower(int defensivePower)
    {
        Logger.Assert(_infoWindow != null);

        DefensivePower.DefaultStatus = defensivePower;

        Logger.Log($"플레이어 방어력 {defensivePower}로 설정됨", gameObject);
    }

    public void ToHeal(int heal)
    {
        Logger.Assert(_infoWindow != null);

        Hp = Hp + heal < MaxHp ? Hp + heal : MaxHp;

        _infoWindow.UpdateHpBar(Hp, MaxHp);
        Logger.Log($"플레이어 {heal} 힐. 현재 체력 : {Hp.ToString()}", gameObject);
    }

    public void StartAttackAnimation()
    {
        Logger.Assert(_animator != null);

        _animator.SetTrigger(AttackHash);

        Logger.Log("플레이어 Attack Trigger On");
    }

    public void StartHitAnimation()
    {
        Logger.Assert(_animator != null);
        
        if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            _animator.SetTrigger(HitHash);
        }

        Logger.Log("플레이어 Hit Trigger On");
    }

    public void StartDeadAnimation()
    {
        Logger.Assert(_animator != null);

        _animator.SetTrigger(DeathHash);
        
        float deathAnimTime = AnimationTime.Duration(AnimationType.Death, _animator);
        Invoke(nameof(FinishDeathAnimation), deathAnimTime);
        
        Logger.Log("플레이어 Death Trigger On");
    }

    public void FinishAttackAnimation()
    {
        // 공격 후 이벤트 발동
        Logger.Log("플레이어 공격 후 이벤트 시작");
        
        FinishAttackEvent.Invoke();
        
        Logger.Log("플레이어 공격 후 이벤트 종료");
        
        if (BattleManager.Instance.EnemyBattleable.Hp == 0)
        {
            BattleManager.Instance.EnemyBattleable.StartDeadAnimation();
        }
    }

    /// <summary> Death 애니메이션 끝날 때 </summary>
    public void FinishDeathAnimation()
    {
        Logger.Log("플레이어 Death 애니메이션 끝 시작");
        Destroy(gameObject);
        Time.timeScale = 1;
        FadeManager.Instance.StartFadeOut();
        StageManager.Instance.SetFadeEvent(StageType.GameOver);
        Logger.Log("플레이어 Death 애니메이션 끝 종료");
    }
}
