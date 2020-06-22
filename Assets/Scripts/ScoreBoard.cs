using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] int scorePerEnemy = 200;

    int currentScore;
    Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
        currentScore = 0;
        scoreText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = currentScore.ToString();
    }

    public void AddToScore()
    {
        currentScore = currentScore + scorePerEnemy;
    }
}
