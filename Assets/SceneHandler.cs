using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    private static SceneHandler _instance;

    private Coroutine _currentTransitionWait;
    private ITransitionEffect _currentTransition;

    private AsyncOperation _nextScene;
    private AsyncOperation _backgroundScene;

    private const string EnemyBattleScene = "EnemyBattle";

    private readonly string[] _bgScenes = { "BGTest" };
    private string _loadedBgScene;

    private WorldObject[] _worldObjectsCache;

    // TODO create a way to load additional scenes and unload them, unique class for each way?

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

    public void EnemyBattleStart(Background background, ITransitionEffect transition)
    {
        _worldObjectsCache = FindObjectsOfType<WorldObject>();
        foreach (var worldObject in _worldObjectsCache)
        {
            worldObject.BattleStart();
        }

        if (_currentTransitionWait != null)
            StopCoroutine(_currentTransitionWait);
        _currentTransition = transition;
        StartCoroutine(BattleTransitionStartWait(background));
    }

    public void EnemyBattleEnd(ITransitionEffect transition)
    {
        foreach (var worldObject in _worldObjectsCache)
        {
            worldObject.BattleEnd();
        }

        if (_currentTransitionWait != null)
            StopCoroutine(_currentTransitionWait);
        _currentTransition = transition;
        StartCoroutine(BattleTransitionEndWait());
    }

    private IEnumerator BattleTransitionStartWait(Background? background)
    {
        _nextScene = SceneManager.LoadSceneAsync(EnemyBattleScene, LoadSceneMode.Additive);
        _nextScene.allowSceneActivation = false;
        _backgroundScene = BackgroundSceneLoad(background);

        StartCoroutine(_currentTransition.TransitionEffect());

        // wait for transition to finish
        yield return new WaitUntil(() => _currentTransition.IsDone);
        Debug.Log("Finished transition, loading battle scene");
        foreach (var worldObject in _worldObjectsCache)
        {
            worldObject.FinishedStartTransition();
        }

        _currentTransitionWait = null;
        _nextScene.allowSceneActivation = true;
        _nextScene = null;
        if (_backgroundScene != null)
        {
            _backgroundScene.allowSceneActivation = true;
        }
    }

    private IEnumerator BattleTransitionEndWait()
    {
        SceneManager.UnloadSceneAsync(EnemyBattleScene);
        BackgroundSceneUnload();

        StartCoroutine(_currentTransition.TransitionEffect());

        // wait for transition to finish
        yield return new WaitUntil(() => _currentTransition.IsDone);
        foreach (var worldObject in _worldObjectsCache)
        {
            worldObject.FinishedEndTransition();
        }

        _currentTransitionWait = null;
        _nextScene.allowSceneActivation = true;
        _nextScene = null;
    }

    private AsyncOperation BackgroundSceneLoad(Background? background)
    {
        if (background == null) return null;

        if (_loadedBgScene != null)
        {
            SceneManager.UnloadSceneAsync(_loadedBgScene);
        }

        if ((int)background.Value > _bgScenes.Length)
        {
            Debug.LogError("Background scene index out of range");
            return null;
        }

        _loadedBgScene = _bgScenes[(int)background.Value];
        var operation = SceneManager.LoadSceneAsync(_loadedBgScene, LoadSceneMode.Additive);
        operation.allowSceneActivation = false;
        return operation;
    }

    private void BackgroundSceneUnload()
    {
        if (_loadedBgScene != null)
        {
            SceneManager.UnloadSceneAsync(_loadedBgScene);
            _loadedBgScene = null;
        }
    }
}

public enum Background
{
    Test
}

public interface ITransitionEffect
{
    IEnumerator TransitionEffect();
    bool IsDone { get; }
}