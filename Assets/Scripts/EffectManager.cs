using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EffectManager : MonoBehaviour
{
    [SerializeField]
    float transitionTime = 1f;

    Animator transition;

    private void Awake()
    {
        transition = GetComponent<Animator>();
    }


    public void LoadNext(int levelToLoad)
    {
        StartCoroutine(levelNext(levelToLoad));
    }

    public void ReLoad()
    {
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        transition.SetTrigger("Death");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator levelNext(int levelToLoad)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(levelToLoad);
    }
}
