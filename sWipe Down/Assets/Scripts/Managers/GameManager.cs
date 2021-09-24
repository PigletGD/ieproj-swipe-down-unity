using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    public int Currency { get; private set; }
    public int TotalValue { get; private set; }

    [SerializeField] MoneyText moneyText;

    [SerializeField] VoidEvent onUpdateMoney = null;

    [ContextMenuItem("Organize Milestones", "OrganizeTotalValueMilestones")]
    [SerializeField] List<MilestoneSO> milestones = default;
    [ContextMenuItem("Organize TP Milestones", "OrganizeCurrentTPMilestones")]
    [SerializeField] List<MilestoneSO> tpMilestones = default;

    [SerializeField] GameObject pauseMenu = default;

    private float tick = 0;

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

    public void CheckForReachedTPMilestones()
    {
        if (tpMilestones.Count <= 0) return;

        bool reached;

        do
        {
            if (Currency >= tpMilestones[0].valueThreshold)
            {
                tpMilestones[0].milestoneType.Raise();
                tpMilestones.RemoveAt(0);
                reached = true;
            }
            else reached = false;
        } while (reached && tpMilestones.Count > 0);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    [ContextMenu("Add Currency")]
    public void AddCurrencyDebug()
    {
        Currency += 500;
        TotalValue += 500;
        moneyText.UpdateText(Currency);
        onUpdateMoney.Raise();
    }

    [ContextMenu("Organize Total Value Milestones")]
    public void OrganizeTotalValueMilestones()
    {
        milestones = milestones.OrderBy(e => (e.valueThreshold)).ToList();

        Debug.Log("Finished Organizing Milestones");
    }

    [ContextMenu("Organize Current TP Milestones")]
    public void OrganizeCurrentTPMilestones()
    {
        tpMilestones = tpMilestones.OrderBy(e => (e.valueThreshold)).ToList();

        Debug.Log("Finished Organizing TP Milestones");
    }

    public void StopTime() => Time.timeScale = 0.0f;

    public void StartTime() => Time.timeScale = 1.0f;
}