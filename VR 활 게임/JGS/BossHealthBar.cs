using UnityEngine;

public class BossHealthBar : Singleton<BossHealthBar>
{
    [SerializeField]
    private RectTransform _healthBarBG;
    [SerializeField]
    private RectTransform _healthBar;
    [SerializeField]
    private Dragon _dragon;

    private float _healthBarWidth;

    private float _maxHealth;
    private float _currentHealth;

    public void ChageCurrentHealth(float health)
    {
        _currentHealth = health;
        //_healthBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _healthBarWidth / (_maxHealth / _currentHealth));
    }

    public void InitHealthBar(float mHealth, float cHealth)
    {
        _maxHealth = mHealth;
        _currentHealth = cHealth;
    }

    private BossHealthBar()
    {
    }

    private void Start()
    {
        InitHealthBar(100, 100);
        _healthBarWidth = _healthBar.sizeDelta.x;
    }

    private void Update()
    {
        _healthBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Mathf.Lerp(_healthBarBG.sizeDelta.x, _healthBarWidth / (_maxHealth / _currentHealth), 0.1f));
        _healthBarBG.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Mathf.Lerp(_healthBarBG.sizeDelta.x, _healthBar.sizeDelta.x,0.1f));
        //체력바 디버깅용
    }
}
