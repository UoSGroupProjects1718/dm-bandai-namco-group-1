using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float CurrentTime;
    private bool _running;
    private Text _timerText;

    void Start()
    {
        Debug.Log("starting timer up");
        CurrentTime = 10f;
        _timerText = gameObject.GetComponent<Text>();
    }

    void Update()
    {
        if (!_running) return;
        if (CurrentTime < float.Epsilon)
        {
            OnTimerComplete(EventArgs.Empty);
            
            // Check again because there is no guarantee CurrentTime won't change during an event.
            if (CurrentTime < float.Epsilon)
            {
                Stop();

            }
            
        }
        CurrentTime -= Time.deltaTime;
        _timerText.text = CurrentTime.ToString("F0");
        
    }
    
    public void Begin()
    {
        _running = true;
    }
    
    public void Begin(float time)
    {
        CurrentTime = time;
        _running = true;
    }

    public void Reset(float timeLimit)
    {
        Stop();
        CurrentTime = timeLimit;
        _timerText.text = CurrentTime.ToString("F0");
    }

    public void Stop()
    {
        _running = false;
    }


    
    public virtual void OnTimerComplete(EventArgs e)
    {
        EventHandler handler = TimerComplete;
        if (handler != null)
        {
            handler(this, e);
        }
    }
    
    public event EventHandler TimerComplete;
}
