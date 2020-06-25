using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] float delayBetweenLevels = 2f;
    void Update()
    {
        StartCoroutine(LoadFromSplashScreen()); //старт корутины загрузка после сплэшскрина с задержкой
        /*if (Input.GetKeyDown(KeyCode.L)) // DEBUG
        {
            LoadNextLevel();
        }*/
    }

    /*void LoadNextLevel() // DEBUG
    {
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        int nextLevelIndex = currentLevelIndex + 1;
        if (currentLevelIndex == SceneManager.sceneCount) //проверка что есть след уровень (если нет, то грузим нулевой)
        {
            nextLevelIndex = 0;
        }
        SceneManager.LoadScene(nextLevelIndex);
    }*/

    IEnumerator LoadFromSplashScreen() //тело корутины закрузки со сплэшскрина
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            yield return new WaitForSecondsRealtime(delayBetweenLevels);
            SceneManager.LoadScene(1);
        }
    }

    public void ReloadScene() //перезагрузка уровня (вызывается из PlayerController)
    {
        StartCoroutine(Reload());
    }
    IEnumerator Reload() //тело корутины по перезагрузке уровня
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        yield return new WaitForSecondsRealtime(delayBetweenLevels);
        SceneManager.LoadScene(sceneIndex);
    }

}
