using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreData 
{

    public float newScore;
    public float highScore;
    public float lastScore;

    public ScoreData(float newScore, float highScore, float lastScore)
    {
        this.newScore = newScore;
        this.highScore = highScore;
        this.lastScore = lastScore;
    }


}
