using System;
using System.Collections;
using UnityEngine;

public class SceneHandler : MonoBehaviour
{
    private static SceneHandler _instance;
    
    private Coroutine _currentTransition;

    private void Start()
    {
        if (_instance != null)
        {
            Destroy(this);
            return;
        }
        
        // safety check to make sure there is only one instance of this class
        _instance = this;
        DontDestroyOnLoad(this);
    }

    public void EnemyBattleStart(Background background, IEnemyBattleTransition transition)
    {
        if (_currentTransition != null)
            StopCoroutine(_currentTransition);
        _currentTransition = StartCoroutine(transition.TransitionEffect());
    }

    public enum Background
    {
        Test
    }
}

public interface IEnemyBattleTransition
{
    IEnumerator TransitionEffect();
}
