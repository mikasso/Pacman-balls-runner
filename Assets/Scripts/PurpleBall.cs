using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleBall : BallMove
{
    public float multiply = 1.2f;
    override protected void affectPacman(PacmanController pacman)
    {
        pacman.multiplySpeed(multiply);
    }
}
