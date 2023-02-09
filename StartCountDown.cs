using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MText;
public class StartCountDown : MonoBehaviour
{
    public AudioSource SoundPlayer;
    public AudioClip CountDownSoundClip;
    AudioSource audioData;
    public GameObject question_panel;
    public MText.Modular3DText timer;
    // Start is called before the first frame update
    IEnumerator countdown(){
        Debug.Log("start countdown");
        
        timer.Text = "Ready";
        yield return new WaitForSeconds((float)0.3);
        SoundPlayer.PlayOneShot(CountDownSoundClip);
        yield return new WaitForSeconds((float)0.7);
        timer.Text = "Set";
        yield return new WaitForSeconds((float)0.7);
        timer.Text = "GO!";
        yield return new WaitForSeconds(1);
        Debug.Log("countdown");
        timer.gameObject.SetActive(false);
        Debug.Log("Finish Init");
        question_panel.gameObject.SetActive(true);
        
        ProblemControl.instance.NextProblem();
    }
    void Start()
    {
        Debug.Log("started");
        StartCoroutine(countdown());
        audioData = GetComponent<AudioSource>();
        audioData.PlayDelayed(5);
        //Debug.Log("QAQ");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
