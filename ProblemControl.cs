using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MText;
using Michsky.MUIP;
using System;
using UnityEngine.SceneManagement;
public class ProblemControl : MonoBehaviour
{
    public MText.Modular3DText Statement;
    public static double TimeSinceStart = 0;
    int TotProblem = 2;
    int CurrentProblem = -1;
    string[] ProblemStatement = {"TEST1","TEST2"};
    int[] ProblemAns = { 1 ,2};//1=a 2=b etc
    public static ProblemControl instance;
    public ProgressBar myBar;
    
    // Start is called before the first frame update
    void Start()
    {
        TimeSinceStart = Time.realtimeSinceStartupAsDouble;
        if (Statement == null)
        {
            Debug.Log("Error: Statement is null");
        }
     //   StartCoroutine(RandomNoise());
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(UpdatePB());
    }
    private void Awake()
    {
        if (Statement == null)
        {
            Debug.LogError("Error: Statement is indeed null");
        }
        Debug.Log("Awake Problem Control");
        instance = this;
    }
    IEnumerator UpdatePB()
    {
        while (myBar.currentPercent != Math.Max(100 * (float)CurrentProblem / TotProblem, 0))
        {
            //Debug.Log("UpdatePB");
            float MaxPercentagePerSecond = 0.1F;
            float target = Math.Max(100 * (float)CurrentProblem / TotProblem, 0), delta = target - myBar.currentPercent;
            delta = Math.Max(delta, -MaxPercentagePerSecond);
            //Debug.Log("UpdatePB"+delta+" "+target);
            delta = Math.Min(delta, MaxPercentagePerSecond);
            myBar.currentPercent += delta;
            yield return new WaitForSeconds((float)0.02);
        }
        
    }
    public void NextProblem()
    {
        NoiseControl.PlayNoise = 1;
        Debug.Log("NextProblem");
        CurrentProblem++;
        if (CurrentProblem == TotProblem)
        {
            Debug.Log("ShowScoreBoard");
            Debug.Log("Correct:"+ScoreManager.problems_correct);
            Debug.Log("Wrong:" + ScoreManager.problems_wrong);
            ShowScoreBoard();
            return;
        }
        Debug.Log("ProblemStatement=" + ProblemStatement[CurrentProblem]);
        if (Statement == null)
        {
            Debug.Log("Error: Statement is null");
        }
        Statement.Text = ProblemStatement[CurrentProblem];
    }
    public void CheckAnswer(int ans)
    {
        Debug.Log("CheckAnswer");
        if (ans == ProblemAns[CurrentProblem]) ScoreManager.instance.Correct();
        else ScoreManager.instance.Wrong();
        NextProblem();
    }
    public void ShowScoreBoard()
    {
        NoiseControl.PlayNoise = 0;
        SceneManager.LoadScene(2);
        //ScoreBoard.instance.ShowScoreBoard(ScoreManager.instance.problems_correct, ScoreManager.instance.problems_wrong, ScoreManager.instance.noanswer);
    }
}
