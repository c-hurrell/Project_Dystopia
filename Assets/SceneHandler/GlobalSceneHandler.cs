using System;
using System.Collections;
using Scenes.SceneTypes;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneHandler
{
    public class GlobalSceneHandler : MonoBehaviour
    {
        private static GlobalSceneHandler _instance;

        private AsyncOperation _loadingScene;

        public static Scene CurrentScene { get; private set; }

        private void Awake()
        {
            if (_instance != null) return;
            _instance = this;

            DontDestroyOnLoad(this);
        }

        public static void LoadScene(Scene scene)
        {
            CurrentScene = scene;
            _instance.LoadSceneInternal(scene);
        }

        private void LoadSceneInternal(Scene scene)
        {
            // scuffed but this is fine for now
            SceneBase loader = scene switch
            {
                Scene.TestWorld => new TestWorldScene(),
                Scene.EnemyBattle => new EnemyBattleScene(),
                _ => throw new ArgumentOutOfRangeException(nameof(scene), scene, null)
            };

            _loadingScene = SceneManager.LoadSceneAsync(loader.SceneName);
            _loadingScene.allowSceneActivation = false;
            StartCoroutine(LoadSceneBg(loader));
        }

        private IEnumerator LoadSceneBg(SceneBase scene)
        {
            yield return scene.Load();
            _loadingScene.allowSceneActivation = true;
        }
    }

    public enum Scene
    {
        TestWorld,
        EnemyBattle
    }
}