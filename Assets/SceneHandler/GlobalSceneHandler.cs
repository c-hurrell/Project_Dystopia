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

        private void Awake()
        {
            if (_instance != null) return;
            _instance = this;

            DontDestroyOnLoad(this);
        }

        public static void LoadScene(Scene scene)
        {
            // scuffed but this is fine for now
            SceneBase loader = scene switch
            {
                Scene.TestWorld => new TestWorldScene(),
                Scene.EnemyBattle => new EnemyBattleScene(),
                _ => throw new ArgumentOutOfRangeException(nameof(scene), scene, null)
            };

            _instance.StartCoroutine(LoadSceneBg(loader));
        }

        private static IEnumerator LoadSceneBg(SceneBase scene)
        {
            yield return scene.Load();
            SceneManager.LoadScene(scene.SceneName);
        }
    }

    public enum Scene
    {
        TestWorld,
        EnemyBattle
    }
}