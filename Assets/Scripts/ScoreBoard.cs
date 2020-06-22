using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    int currentScore;
    int scorePerTime = 50;
    Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
        currentScore = 0;
        scoreText = GetComponent<Text>();
        StartCoroutine(AddScorePerTimeCor());
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = currentScore.ToString();
    }

    public void AddToScore(int scoreToAdd)
    {
        currentScore = currentScore + scoreToAdd;
    }

    IEnumerator AddScorePerTimeCor() // добавить счет каждые несколько секунд (указано в WaitForSec)
    {
        for(; ; )
        {
            AddToScore(scorePerTime);
            yield return new WaitForSecondsRealtime(1f);
        }
        
    }
}
