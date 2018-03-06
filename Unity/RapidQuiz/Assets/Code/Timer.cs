using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float _currentTime;
    private bool _running;
    private Text timerText;

    void Start()
    {
        Debug.Log("starting timer up");
        _currentTime = 10f;
        timerText = gameObject.GetComponent<Text>();
    }

    void Update()
    {
        if (!_running) return;
        if (_currentTime < float.Epsilon)
        {
            OnTimerComplete(EventArgs.Empty);
            Stop();
        }
        _currentTime -= Time.deltaTime;
        timerText.text = _currentTime.ToString("F0");
        
    }
    
    public void Begin()
    {
        _running = true;
    }
    
    public void Begin(float time)
    {
        _currentTime = time;
        _running = true;
    }

    public void Reset(float timeLimit)
    {
        Stop();
        _currentTime = timeLimit;
        timerText.text = _currentTime.ToString("F0");
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
