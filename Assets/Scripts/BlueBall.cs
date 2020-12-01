using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBall : BallMove
{
    // Start is called before the first frame update
    public float multiplyBonus;
    override protected void affectPacman(PacmanController pacman)
    {
        pacman.getMultiplyBonus(multiplyBonus);
    }
}
