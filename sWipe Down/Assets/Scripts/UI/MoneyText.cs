using UnityEngine;
using UnityEngine.UI;

public class MoneyText : MonoBehaviour
{
    [SerializeField] Text text;
    
    public void UpdateText(int value)
    {
        text.text = ": " + value;
    }
}
