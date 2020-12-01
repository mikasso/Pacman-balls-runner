using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartScript : BallMove
{
    override protected void affectPacman(PacmanController pacman)
    {
        pacman.addHeart();
    }
}
