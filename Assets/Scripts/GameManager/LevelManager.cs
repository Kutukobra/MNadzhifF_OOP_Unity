using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Animator animator;

    void Awake()
    {

    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        
        animator.SetTrigger("Start");
        
        yield return new WaitForSeconds(1);

        SceneManager.LoadSceneAsync(sceneName);

        Player.Instance.transform.position = new(0, -4.5f);
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }
}
