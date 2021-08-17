using UnityEngine;
using UnityEngine.UI;

public class MoneyText : MonoBehaviour
{
    [SerializeField] Text text;
    [SerializeField] Animator textAnimator;
    
    public void UpdateText(int value)
    {
        text.text = value.ToString();

        textAnimator.enabled = true;
        textAnimator.Play("Text Bounce", -1, 0);
    }
}
