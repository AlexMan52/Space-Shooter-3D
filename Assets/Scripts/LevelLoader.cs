using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] float delayBetweenLevels = 2f;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        StartCoroutine(LoadFromSplashScreen());
        if (Input.GetKeyDown(KeyCode.L)) // DEBUG
        {
            LoadNextLevel();
        }
    }

    void LoadNextLevel()
    {
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        int nextLevelIndex = currentLevelIndex + 1;
        if (currentLevelIndex == SceneManager.sceneCount) //проверка что есть след уровень (если нет, то грузим нулевой)
        {
            nextLevelIndex = 0;
        }
        SceneManager.LoadScene(nextLevelIndex);
    }

    IEnumerator LoadFromSplashScreen()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            yield return new WaitForSecondsRealtime(delayBetweenLevels);
            SceneManager.LoadScene(1);
        }
    }

}
