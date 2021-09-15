using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class Notifications : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _waveText;

    public void NotifyAboutWave(int waveNumber)
    {
        _waveText.color = Color.clear;
        _waveText.text = $"Wave {waveNumber}";
        Sequence sequence = DOTween.Sequence();
        sequence.Append(_waveText.DOColor(Color.white, 1));
        sequence.AppendInterval(1);
        sequence.Append(_waveText.DOColor(Color.clear, 1));
    }
}