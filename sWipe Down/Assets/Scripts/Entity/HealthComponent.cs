using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] int maxHealth = 0;
    private int currentHealth = 0;

    [SerializeField] bool isEnemy = false;
    [SerializeField] GameObject entity = null;

    [SerializeField] MeshRenderer mr = null;

    private bool deadAlready = false;

    [SerializeField] HealthBar healthBar = null;

    [SerializeField] GameEventsSO onEnemyKilled = null;

    // Start is called before the first frame update
    void OnEnable()
    {
        deadAlready = false;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0 && !deadAlready)
        {
            currentHealth = 0;
            Die();
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

                MeshRenderer[] mrList = entity.GetComponentsInChildren<MeshRenderer>();
                foreach (MeshRenderer meshrend in mrList)
                {
                    meshrend.enabled = false;
                }
            }
            else { entity.GetComponent<GoToEndScreen>().SwitchScene(); return; }
            
        }
        else
        {
            entity.GetComponent<EnemyMove>().RemoveTargetedList();

            onEnemyKilled.Raise();
        }

        // make mesh invisible
        //mr.enabled = false;

        //gameObject.tag = "Untagged";

        if (!isEnemy && entity.tag != "Base") BuildingManager.instance.RemoveBuildingFromDictionary(gameObject.GetComponent<TowerBehaviour>().key);

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
}