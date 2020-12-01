using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBall : BallMove
{
    // Start is called before the first frame update
    public int pointsValue = 5;

    override protected void affectPacman(PacmanController pacman)
    {
        pacman.increaseScore(pointsValue);
    }
}
