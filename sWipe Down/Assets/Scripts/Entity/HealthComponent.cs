using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] int maxHealth = 0;
    [SerializeField] int currentHealth = 0;
    [SerializeField]private int armor = 0;
    [SerializeField] bool isEnemy = false;
    [SerializeField] GameObject entity = null;

    private bool deadAlready = false;

    [SerializeField] HealthBar healthBar = null;

    [SerializeField] VoidEvent onEnemyKilled = null;

    [SerializeField] List<IDeathHandler> deathHandlerList;

    private void Awake()
    {
        deathHandlerList = new List<IDeathHandler>();
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        deadAlready = false;
       // currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= (damage-armor);
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0 && !deadAlready)
        {
            currentHealth = 0;
            Die();
            Debug.Log("Dead");
            OnDeath();
            deadAlready = true;
        }
    }

    private void Die()
    {
        // call game event

        if (!isEnemy)
        {
            if (entity.tag != "Base")
            {
                entity.GetComponent<TowerBehaviour>().RemoveTargetedList();
            }
            else { entity.GetComponent<GoToEndScreen>().SwitchScene(); return; }
            
        }
        else
        {
            entity.GetComponent<EnemyMove>().RemoveTargetedList();

            onEnemyKilled.Raise();
        }

        //gameObject.tag = "Untagged";

        TowerBehaviour TB = gameObject.GetComponent<TowerBehaviour>();
        if (TB != null)
        {
            TB.ReduceTotalValue();
            BuildingManager.instance.RemoveBuildingFromDictionary(TB.key);
        }


        PooledObject pooled = gameObject.GetComponent<PooledObject>();
        if (pooled != null) pooled.ReturnObject();
        else Destroy(gameObject);

        // StartCoroutine("Death");
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(1f);

        Destroy(gameObject);
    }

    private void OnDeath()
    {
        if (deathHandlerList != null)
        {
            foreach (IDeathHandler listener in deathHandlerList)
            {
                listener.OnDeath();
            }
        }
    }

    public int MaxHealth => maxHealth;
    public int CurrentHealth => currentHealth;

    public void AddDeathHandler(IDeathHandler handler)
    {
        deathHandlerList.Add(handler);
    }

    public void RemoveDeathHandler(IDeathHandler handler)
    {
        deathHandlerList.Remove(handler);
    }

    public void heal(int heal)
    {
        currentHealth += heal;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
}