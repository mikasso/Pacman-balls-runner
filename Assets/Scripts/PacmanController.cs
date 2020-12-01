//Attach this script to an empty GameObject
//Create some UI Text by going to Create>UI>Text.
//Drag this GameObject into the Text field of your GameObject’s Inspector window.

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class PacmanController : MonoBehaviour
{
    public float oldMouseX = 0.0f, xPos = 0.0f;
    public float yPos;
    public float maxX, minX;
    private Camera cam;
    public bool holdMouse = false;
    private int score = 0;
    public Text ScoreText;
    public Text SpeedTimer;
    public Text BonusTimer;

    private int multiplyVelTimeLeft = 0;
    public int MultiplyVelTime;

    private float multiplyBonus = 1.0f;
    private int multiplyBonusTimeLeft = 0;
    public int MultiplyBonusTime;

    private int speedTime = 0;
    private int bonusTime = 0;
    public int TimeLength = 10;
    const int Frames = 60;

    public GameObject heart;
    private Stack<GameObject> hearts;
    private int lifes;
    private BallSpawning ballSpawning;
    private int difficultLevel = 1;

    public int lvlThreshHolder;
    void Start()
    {
        cam = Camera.main;
        ballSpawning = cam.GetComponent<BallSpawning>();
        ScoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        SpeedTimer = GameObject.Find("SpeedTimer").GetComponent<Text>();
        BonusTimer = GameObject.Find("BonusTimer").GetComponent<Text>();
        refreshPoints();
        setTimer(SpeedTimer, 0);
        setTimer(BonusTimer, 0);
        hearts = new Stack<GameObject>();
        for (int i = 0; i < 3; i++) addHeart();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (holdMouse == true)
            {

                Vector3 vector3 = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, yPos, cam.nearClipPlane)) - cam.ScreenToWorldPoint (new Vector3(oldMouseX, yPos, cam.nearClipPlane));
                Debug.Log(vector3);
                vector3.y = yPos;
                vector3.x += xPos;
                vector3.x = vector3.x > maxX ? maxX : vector3.x; ;
                vector3.x = vector3.x < minX ? minX : vector3.x; ;
                xPos = vector3.x;
                transform.position = vector3;
            }
            holdMouse = true;
            oldMouseX = Input.mousePosition.x;
        }
        else
            holdMouse = false;


        tickTimer(ref multiplyVelTimeLeft, ref SpeedTimer, ref BallMove.multiplyVel);
        tickTimer(ref multiplyBonusTimeLeft, ref BonusTimer, ref multiplyBonus);
    }

    private void tickTimer(ref int timeLeft,ref Text timer, ref float multiplyValue)
    {
        if (timeLeft < 0)
            multiplyValue = 1.0f;
        else
        {
            timeLeft--;
            if (timeLeft % Frames == 0)
            {
                setTimer(timer, timeLeft / Frames);
            }
        }
    }


    public void multiplySpeed(float multiplyVel)
    {
        setTimer(SpeedTimer, MultiplyBonusTime / Frames);
        BallMove.multiplyVel = multiplyVel;
        multiplyVelTimeLeft = MultiplyVelTime;
        speedTime = TimeLength;
    }

    public void getMultiplyBonus(float multiplyBonus)
    {
        setTimer(BonusTimer, MultiplyBonusTime/Frames);
        this.multiplyBonus = multiplyBonus;
        multiplyBonusTimeLeft = MultiplyBonusTime;
        bonusTime = TimeLength;
    }


    public void increaseScore(int value)
    {
        score += (int) ((float)value*multiplyBonus);
        if(score> lvlThreshHolder * difficultLevel )
        {
            difficultLevel++;
            ballSpawning.difficultUp(difficultLevel);
        }
        refreshPoints();
    }

    private void refreshPoints()
    {
        ScoreText.text = ("Score: " + score);
    }

    private void setTimer(Text timer, int timeLeft)
    {
    if (timeLeft == 0)
        timer.text = "";
    else if (timer == SpeedTimer)
        timer.text = "SpeedUp: " + timeLeft;
    else if(timer == BonusTimer)
        timer.text = "Bonus: " + timeLeft;
    }

    public int decreaseLife()
    {
        lifes--;
        Destroy(hearts.Pop());
        if (lifes == 0)
        {
            ballSpawning.endGame(score);
            BonusTimer.text = "";
            SpeedTimer.text = "";
        }
        return lifes;
    }

    public void addHeart()
    {
        if (lifes > 3)
            return;
        hearts.Push(Instantiate(heart, new Vector3(-2.44f+0.7f*lifes, 4.64f, 0.0f), Quaternion.identity));
        lifes++;
    }

}
