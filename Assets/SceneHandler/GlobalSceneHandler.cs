using System;
using System.Collections.Generic;
using Scenes.SceneTypes;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneHandler
{
    public static class GlobalSceneHandler
    {
        /// <summary>
        /// List of all loaded scenes
        /// </summary>
        private static readonly List<SceneBase> LoadedScenes = new();

        public static AsyncOperation LoadScene(Scene scene, bool overwrite = true)
        {
            var sceneInstance = NewScene(scene);

            if (overwrite)
            {
                LoadedScenes.Clear();
                LoadedScenes.Add(sceneInstance);
                return SceneManager.LoadSceneAsync(sceneInstance.SceneName);
            }

            LoadedScenes.Add(sceneInstance);

            return SceneManager.LoadSceneAsync(sceneInstance.SceneName, LoadSceneMode.Additive);
        }

        public static void UnloadScene(Scene scene)
        {
            var sceneInstance = NewScene(scene);

            LoadedScenes.RemoveAll(x => x.SceneName == sceneInstance.SceneName);

            SceneManager.UnloadSceneAsync(sceneInstance.SceneName);
        }

        public static void PauseScene(Scene scene)
        {
            var sceneInstance = NewScene(scene);
            sceneInstance.Pause();
        }

        public static void TempUnloadScene(Scene scene)
        {
            var sceneInstance = NewScene(scene);
            sceneInstance.TempUnload();
        }
        
        public static void ResumeScene(Scene scene)
        {
            var sceneInstance = NewScene(scene);
            sceneInstance.Resume();
        }

        private static SceneBase NewScene(Scene scene)
        {
            var sceneType = SceneType(scene);

            return (SceneBase)Activator.CreateInstance(sceneType);
        }

        private static Type SceneType(Scene scene)
        {
            return scene switch
            {
                Scene.TestWorld => typeof(TestWorldScene),
                Scene.EnemyBattle => typeof(EnemyBattleScene),
                _ => throw new NotImplementedException()
            };
        }
    }

    public enum Scene
    {
        TestWorld,
        EnemyBattle
    }
}