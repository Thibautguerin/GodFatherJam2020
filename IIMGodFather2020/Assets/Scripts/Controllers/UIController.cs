﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    public TextMeshProUGUI timerText;
    public string timerSentence;

    private void Awake()
    {
        if(!instance)
        {
            instance = this;
        }else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void Timer(float timer)
    {

        timerText.text = timerSentence+" "+timer.ToString();
    }
}
