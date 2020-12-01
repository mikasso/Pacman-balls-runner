using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallSpawning : MonoBehaviour
{
    public GameObject startButton;
    public GameObject pacman;
    // Start is called before the first frame update
    public GameObject lightGreenBall;
    public GameObject greenBall;
    public GameObject darkGreenBall;

    public GameObject lightRedBall;
    public GameObject redBall;
    public GameObject darkRedBall;

    public GameObject purpleBall;
    public GameObject orangeBall;
    public GameObject blueBall;
    public GameObject heart;

    public float spawningBallChance;

    public float chanceLightGreen;
    public float chanceGreen;
    public float chanceDarkGreen;

    public float chanceLightRedBall;
    public float chanceRedBall;
    public float chanceDarkRedBall;

    public float chancePurpleBall;
    public float chanceOrangeBall;
    public float chanceBlueBall;
    public float chancehHeart;
    public int durationLvlUpInfo;
    public int maxLvl;

    const float yPos = 5.0f;
    // Update is called once per frame
    private float tempSumProbability = 0.0f;
    private List<BallProbability> probabilityList;
    private bool lvlUpInfo = false;
    private int tickLvlUp = 0;
    Text EndGameText;
    public Text lvlUpText;
    private void addDistribution(GameObject ball, float p)
    {
        probabilityList.Add(new BallProbability(ball, p + tempSumProbability));
        tempSumProbability += p;
    }

    void Start()
    {
        lvlUpText.enabled = false;
        this.enabled = false;
        EndGameText = GameObject.Find("EndGameText").GetComponent<Text>();
        probabilityList = new List<BallProbability>();
        addDistribution(lightGreenBall, chanceLightGreen);
        addDistribution(greenBall, chanceGreen);
        addDistribution(darkGreenBall, chanceDarkGreen);
        addDistribution(lightRedBall, chanceLightRedBall);
        addDistribution(redBall, chanceRedBall);
        addDistribution(darkRedBall, chanceDarkRedBall);
        addDistribution(purpleBall, chancePurpleBall);
        addDistribution(orangeBall, chanceOrangeBall);
        addDistribution(blueBall, chanceBlueBall);
        addDistribution(heart, chancehHeart);
        if (tempSumProbability != 1.0f)
        {
            Debug.Log("Incorrect sum of probability in chance of ball spawining. Value is " + tempSumProbability);
        }
    }
    
    public class BallProbability{
        public GameObject ball;
        public float probability;
        public BallProbability(GameObject ball, float probability)
        {
            this.ball = ball;
            this.probability = probability;
        }
    }

    void Update()
    {
        float r = Random.Range(0.0f, 1.0f);
        float xPos = Random.Range(-2.2f, 2.2f);
        if (r < spawningBallChance)
        {
            r = Random.Range(0.0f, 1.0f);
            foreach (BallProbability ballProbality in probabilityList)
            {
                if (r < ballProbality.probability)
                {
                    Instantiate(ballProbality.ball, new Vector3(xPos, yPos, 0), Quaternion.identity);
                    break;
                }
            }
        }
        checkLvlUpInfo();
    }

    private void checkLvlUpInfo()
    {
        if (lvlUpInfo == true)
        {
            lvlUpText.enabled = true;
            Debug.Log("Lvl Up");
            lvlUpInfo = false;
            tickLvlUp = 60;
        }
        if (tickLvlUp > 0)
            tickLvlUp--;
        else
            lvlUpText.enabled = false;
    }


    public void startGame()
    {
        this.enabled = true;
        startButton.SetActive(false);
        EndGameText.enabled = false;
        GameObject livingPacman = Instantiate(pacman, new Vector3(0, -4.05f, 0), Quaternion.identity);
        livingPacman.name = "pacman";
        livingPacman.transform.Rotate(new Vector3(0,0,90.0f));
    }

    public void endGame(int score)
    {
        this.enabled = false;
        startButton.SetActive(true);
        EndGameText.enabled = true;
        int highscore = PlayerPrefs.GetInt("Highscore", 0);
        Debug.Log("Highscore = "+highscore);
        if (highscore < score)
        {
            PlayerPrefs.SetInt("Highscore", score);
            EndGameText.text = "New high score:" + score + "!";
        }
        else
        {
            EndGameText.text = "Score:" + score + "\n High score:" + highscore;
        }
    }

    public void difficultUp(int level )
    {
        for(int i =0;i< (level < maxLvl ? level : maxLvl) * 3 ;i++)
        {
            Instantiate(lightGreenBall, new Vector3(Random.Range(-2.2f, 2.2f), Random.Range(5.0f, 7.0f), 0), Quaternion.identity);
        }
        if (level == maxLvl)
            return;
        spawningBallChance *= 1.06f;
        BallMove.velAdd -= 0.5f;
        lvlUpInfo = true;
    }
}