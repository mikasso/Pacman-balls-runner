using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeBall : BallMove
{
    public float minAmount;
    public float maxAmount;
    public GameObject lightRedBall;
    override protected void affectPacman(PacmanController pacman)
    {
        for (int i = 0; i < (int)Random.Range(minAmount, maxAmount); i++)
            Instantiate(lightRedBall, new Vector3( Random.Range(-2.0f,2.0f), 5.0f, 0.0f), Quaternion.identity);
    }
}
