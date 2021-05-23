using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int Currency { get; private set; }
    [SerializeField] MoneyText moneyText;

    [SerializeField] GameEventsSO onUpdateMoney = null;

    // Start is called before the first frame update
    void Start()
    {
        Currency = 0;
    }

    public void IncrementScore(int value)
    {
        Currency += value;
        moneyText.UpdateText(Currency);
        onUpdateMoney.Raise();
    }

    public void SpendCurrency(int value)
    {
        Currency -= value;
        moneyText.UpdateText(Currency);
        onUpdateMoney.Raise();
    }
}
