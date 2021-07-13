using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int Currency { get; private set; }
    public int TotalValue { get; private set; }

    [SerializeField] MoneyText moneyText;

    [SerializeField] GameEventsSO onUpdateMoney = null;

    [SerializeField] List<MilestoneSO> milestones = default;

    private float tick = 0;

    // Start is called before the first frame update
    private void Start()
    {
        Currency = 0;
        TotalValue = 0;
    }

    private void Update()
    {
        tick += Time.deltaTime;

        if (tick > 0.25f)
        {
            CheckForReachedMilestones();

            tick = 0.0f;
        }
    }

    public void IncrementScore(int value)
    {
        Currency += value;
        TotalValue += value;
        moneyText.UpdateText(Currency);
        onUpdateMoney.Raise();
    }

    public void SpendCurrency(int value)
    {
        Currency -= value;
        moneyText.UpdateText(Currency);
        onUpdateMoney.Raise();
    }

    public void DecrementScore(int value)
    {
        TotalValue -= value;
    }

    public void CheckForReachedMilestones()
    {
        if (milestones.Count <= 0) return;

        bool reached;

        do
        {
            if (TotalValue >= milestones[0].valueThreshold)
            {
                milestones[0].milestoneType.Raise();
                milestones.RemoveAt(0);
                reached = true;
            }
            else reached = false;
        } while (reached && milestones.Count > 0);
    }
}
