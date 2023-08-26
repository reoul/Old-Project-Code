using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType { TreeSpirit, Wendigo, Skeleton }

public class EnemyBuilder
{
    private string name = "defaultName";
    private float maxHealth = 100;
    private float currentHealth = 100;
    private EnemyType enemyType;

    public EnemyBuilder(string tName)
    {
        this.name = tName;
    }

    public EnemyBuilder SetHealth(float tMaxHealth)
    {
        this.maxHealth = tMaxHealth;
        this.currentHealth = tMaxHealth;
        return this;
    }

    public EnemyBuilder SetEnemyType(EnemyType enemyType)
    {
        this.enemyType = enemyType;
        return this;
    }

    public Enemy Build()
    {
        GameObject enemyObj;
        switch (enemyType)
        {
            case EnemyType.TreeSpirit:
                enemyObj = GameObject.Instantiate(Resources.Load("TreeSpirit", typeof(GameObject))) as GameObject;
                break;
            case EnemyType.Skeleton:
                enemyObj = GameObject.Instantiate(Resources.Load("SkeletonWarrior", typeof(GameObject))) as GameObject;
                break;
            default:
                enemyObj = GameObject.Instantiate(Resources.Load("Wendigo", typeof(GameObject))) as GameObject;
                break;
        }
        enemyObj.name = this.name;
        return enemyObj.GetComponent<Enemy>();
    }
}
