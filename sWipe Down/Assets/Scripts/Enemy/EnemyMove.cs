using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float speed = 0;
    public float maxSpeed = 0;
    public Transform owner = null;
    public Rigidbody rb = null;
    public Transform baseBuilding = null;
    public Transform target = null;
    public List<Transform> targetList = null;
    public List<Transform> targetedList = null;
    public List<StatusEffect> statusEffectsList = new List<StatusEffect>();
    [SerializeField] public Dictionary<StatusType, StatusParticleSystem> statusParticleSystemDictionary;

    private float timeElapsed = 0f;
    [SerializeField] float attackRate = 0f;

    private void Awake()
    {
        statusParticleSystemDictionary = new Dictionary<StatusType, StatusParticleSystem>();

        StatusParticleSystem[] spsList = this.gameObject.GetComponentsInChildren<StatusParticleSystem>();
        foreach (StatusParticleSystem sps in spsList)
        {
            statusParticleSystemDictionary[sps.statusType] = sps;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        baseBuilding = GameObject.FindGameObjectWithTag("Base").transform;
        target = baseBuilding;

        owner = transform;
    }

    private void OnEnable()
    {
        baseBuilding = GameObject.FindGameObjectWithTag("Base").transform;
        target = baseBuilding;

        targetList = new List<Transform>();
        targetedList = new List<Transform>();
        statusEffectsList = new List<StatusEffect>();
    }

    private void OnCollisionStay(Collision collision)
    {
        if((collision.gameObject.tag == "Building" || collision.gameObject.tag == "Base") && timeElapsed > attackRate)
        {
            timeElapsed = 0;
            collision.gameObject.GetComponent<HealthComponent>().TakeDamage(1);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timeElapsed += Time.deltaTime;

        ApplyStatusEffects(Time.deltaTime);

        if (target != null)
        {
            owner.LookAt(new Vector3(target.position.x, 0.0f, target.position.z));

            rb.MovePosition(owner.position + (owner.forward * speed * Time.deltaTime));
            
        }
        else Debug.LogWarning("ERROR: NOT SUPPOSED TO BE HERE");
    }

    public void AddTarget(Transform foundTarget)
    {
        if (targetList.Count <= 0) target = foundTarget;

        targetList.Add(foundTarget);
    }

    public void RemoveTarget(Transform lostTarget)
    {
        targetedList.Remove(lostTarget);
        targetList.Remove(lostTarget);

        if (targetList.Count > 0) target = targetList[0];
        else target = baseBuilding;
    }

    public void AddForcedTarget(Transform foundTarget)
    {
        if (targetList.Count <= 0) target = foundTarget;

        targetList.Insert(0, foundTarget);
    }

    public void RemoveTargetedList()
    {
        foreach (Transform targeted in targetedList)
        {
            if(targeted != null)
            {
                TowerBehaviour tb = targeted.GetComponent<TowerBehaviour>();
                if (tb != null) tb.RemoveTarget(transform);
            }
        }
    }

    public void AddStatusEffect(StatusEffect statusEffect)
    {
        statusEffectsList.Add(statusEffect);
        if (statusParticleSystemDictionary.ContainsKey(statusEffect.statusType) && statusParticleSystemDictionary[statusEffect.statusType] != null)
        {
            statusParticleSystemDictionary[statusEffect.statusType].StatusUpdate(statusEffect);
        }
    }

    private void ApplyStatusEffects(float deltaTime)
    {
        speed = maxSpeed;
        for (int i = statusEffectsList.Count -1; i >= 0; i--)
        {
            StatusEffect statusEffect = statusEffectsList[i];
            statusEffect.ApplyEffect(this.gameObject);
            if (statusEffect.durationType == Duration.INSTANTANEOUS)
            {
                statusEffectsList.RemoveAt(i);
            }
            else if (statusEffect.durationType == Duration.LASTING)
            {
                statusEffect.duration -= deltaTime;
                if (statusEffect.duration <= 0)
                {
                    statusEffectsList.RemoveAt(i);
                    
                }
            }
        }
    }

    public void SetAttackRate(float rate)
    {
        attackRate = rate;
    }
}
