using UnityEngine;

public class Bardbarian : MonoBehaviour
{
    [SerializeField] public int curtainHP { get; private set; } = 10;

    private HealthComponent healthComponent;
    private bool rage = false;

    public float rageMoveSpeed = 2.0f;
    public float rageAttackSpeed = 1.0f;

    private void Start()
    {
        healthComponent = gameObject.GetComponent<HealthComponent>();
    }

    private void Update()
    {
        if (!rage && healthComponent.CurrentHealth < healthComponent.MaxHealth - curtainHP)
        {
            Rage();
        }
    }

    private void Rage()
    {
        Debug.Log("Rage");
        gameObject.GetComponentInChildren<EnemyMove>().maxSpeed = rageMoveSpeed;
        gameObject.GetComponentInChildren<EnemyMove>().SetAttackRate(rageAttackSpeed);
        rage = true;
    }
}
