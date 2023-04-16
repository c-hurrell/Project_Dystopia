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

        private void Awake()
        {
            if (_instance != null) return;
            _instance = this;

            DontDestroyOnLoad(this);
        }

        public static void LoadScene(Scene scene)
        {
            _instance.LoadSceneInternal(scene);
        }

        public static void UnloadScene(Scene scene)
        {
            var loader = _instance.SceneEnumToScene(scene);
            SceneManager.UnloadSceneAsync(loader.SceneName);
        }

        private void LoadSceneInternal(Scene scene)
        {
            // scuffed but this is fine for now
            var loader = SceneEnumToScene(scene);

            _loadingScene = SceneManager.LoadSceneAsync(loader.SceneName,
                loader.Additive ? LoadSceneMode.Additive : LoadSceneMode.Single);
            _loadingScene.allowSceneActivation = false;
            StartCoroutine(LoadSceneBg(loader));
        }

        private SceneBase SceneEnumToScene(Scene scene)
        {
            return scene switch
            {
                Scene.TestWorld => new TestWorldScene(),
                Scene.EnemyBattle => new EnemyBattleScene(),
                Scene.GameOver => new GameOverScene(),
                _ => throw new ArgumentOutOfRangeException(nameof(scene), scene, null)
            };
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
        EnemyBattle,
        GameOver
    }
}