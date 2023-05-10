using System;
using System.Collections;
using System.Collections.Generic;
using Dialog;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeAttack : MonoBehaviour
{
    [SerializeField] private bool hasReachedEnd = false;
    [SerializeField] private float timeLeft = 60;

    [SerializeField] private TextMeshProUGUI timerText;
    // Update is called once per frame
    private void Update()
    {
        if (timeLeft <= 0 && !hasReachedEnd)
        {
            string[] timeRunOut = { "You ran out of time!" };
            DialogManager.ShowDialog(timeRunOut);
            SceneManager.LoadScene("GameOver");
        }
        else if (!hasReachedEnd)
        {
            timeLeft -= Time.deltaTime;
            timerText.text = Math.Round(timeLeft).ToString();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        hasReachedEnd = true;
    }
}
