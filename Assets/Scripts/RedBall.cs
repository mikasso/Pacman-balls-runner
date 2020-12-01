using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBall : BallMove
{
    public float chanceToLossLife;
    override protected void affectPacman(PacmanController pacman)
    {
        if (Random.Range(0.0f, 1.0f) < chanceToLossLife)
            if (pacman.decreaseLife() == 0)
                Destroy(pacman.gameObject);
    }
}
