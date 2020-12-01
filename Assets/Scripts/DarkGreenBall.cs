using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkGreenBall : GreenBall
{
    public GameObject tinyGreenBall;
    override protected void affectPacman(PacmanController pacman)
    {
        for (int i = 0; i < (int)Random.Range(4.0f, 9.0f); i++)
            Instantiate(tinyGreenBall, gameObject.transform.position + new Vector3(0,0.1f,0), Quaternion.identity);
        pacman.increaseScore(pointsValue);
    }
}
