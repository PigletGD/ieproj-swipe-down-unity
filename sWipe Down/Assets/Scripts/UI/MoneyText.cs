using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoneyText : MonoBehaviour
{
    //[SerializeField] Text text;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Animator textAnimator;
    
    public void UpdateText(int value)
    {
        text.text = value.ToString();

        //textAnimator.enabled = true;
        //textAnimator.Play("Text Bounce", -1, 0);
    }
}
