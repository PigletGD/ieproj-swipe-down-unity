using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToEndScreen : MonoBehaviour
{
    // Start is called before the first frame update
    public void SwitchScene()
    {
        SceneManager.LoadScene("GameOver");
    }
}
