using System;
using System.Collections;
using System.Collections.Generic;
using Dialog;
using UnityEngine;

public class BattleTutorial : MonoBehaviour
{
    [SerializeField] private string[] tutorialText = new string[]
    {
        "Wait stop for a second", "That guy ahead doesn't look friendly",
        "They don't usually show themselves like that",
        "Thankfully he looks like a small fry so you should have no problem dealing with him",
        "Just remember to Attack where you can as they do tend to drop some nice Parts",
        "if you don't want to risk the health or time you can always run away"
    };
    private void OnTriggerEnter2D(Collider2D col)
    {
        DialogManager.ShowDialog(tutorialText);
    }
}
