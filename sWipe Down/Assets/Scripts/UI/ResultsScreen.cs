using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultsScreen : MonoBehaviour
{
    [SerializeField] private Animator exitAnimator = default;

    private bool switchingScenes = false;

    public void RetryGame()
    {
        if (switchingScenes) return;

        StartCoroutine("LoadGameScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator LoadGameScene()
    {
        switchingScenes = true;

        exitAnimator.SetTrigger("Exit");

        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene("GameScene");
    }
}