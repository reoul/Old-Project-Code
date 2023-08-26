using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Score
{
    public string name { get; }
    public int score { get; }

    public Score(string name, int score)
    {
        this.name = name;
        this.score = score;
    }
}

public static class ScoreSystem
{
    private static int _score;
    public static int Score
    {
        get { return _score;}
        set { _score = Mathf.Clamp(value,0, 99999) ; }
    }
    public static int SumScore { get; set; }
    
    static ScoreSystem()
    {
        Init();
    }

    public static void Init()
    {
        Score = 0;
        SumScore = 0;
    }

    public static void AddScore(int score)
    {
        SumScore += score;
    }
}