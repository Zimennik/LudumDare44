using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class MessageBox : MonoBehaviour
{
    public static MessageBox Instance;
    private Coroutine _coroutine;

    public TextMeshProUGUI TextBox;

    public TextMeshProUGUI InteractText;
    public TextMeshProUGUI ProgressText;
    

    public void Awake()
    {
        Instance = this;
    }

    public void ShowText(string text, float time = 3f)
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
        _coroutine = StartCoroutine(ShowingText(text, time));
    }

    IEnumerator ShowingText(string text, float time)
    {
        DateTime startTime = DateTime.Now;
        DateTime finishTime = startTime.AddSeconds(time);

        TextBox.text = text;

        TextBox.color = new Color(1,1,1,0);
        TextBox.DOFade(1, 0.3f);
        while (DateTime.Now < finishTime)
        {
            yield return new WaitForSeconds(0.1f);
        }
        TextBox.DOFade(0, 0.3f);
        _coroutine = null;
    }


    public void ShowInteractText(string text)
    {
        InteractText.gameObject.SetActive(true);
        InteractText.text = text;
    }

    public void HideInteractText()
    {
        InteractText.gameObject.SetActive(false);
    }

    public void ShowProgress(string text)
    {
        ProgressText.gameObject.SetActive(true);
        ProgressText.text = text;
    }

    public void HideProgress()
    {
        ProgressText.gameObject.SetActive(false);
    }
}