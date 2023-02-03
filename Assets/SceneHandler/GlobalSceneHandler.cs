using System;
using System.Collections.Generic;
using Scenes.SceneTypes;
using UnityEngine;

namespace SceneHandler
{
    public static class GlobalSceneHandler
    {
        /// <summary>
        /// List of all loaded scenes
        /// Used for unloading a scene safely
        /// </summary>
        private static readonly List<ISceneBase> LoadedScenes = new();

        public static void LoadScene(Scene scene, bool overwrite = true)
        {
            var sceneInstance = NewScene(scene);

            if (overwrite)
            {
                LoadedScenes.Clear();
            }

            LoadedScenes.Add(sceneInstance);

            sceneInstance.LoadAsync(overwrite);
        }

        public static void UnloadScene(Scene scene)
        {
            var sceneType = SceneType(scene);

            var loadedScene = LoadedScenes.Find(x => x.GetType() == sceneType);
            if (loadedScene == null)
            {
                Debug.LogError("Scene you're trying to unload is not loaded");
                return;
            }

            loadedScene.UnloadAsync();
            LoadedScenes.Remove(loadedScene);
        }

        public static void PauseScene(Scene scene)
        {
            var sceneType = SceneType(scene);

            var loadedScene = LoadedScenes.Find(x => x.GetType() == sceneType);
            if (loadedScene == null)
            {
                Debug.LogError("Scene you're trying to pause is not loaded");
                return;
            }

            loadedScene.Pause();
        }

        public static void TempUnloadScene(Scene scene)
        {
            var sceneType = SceneType(scene);

            var loadedScene = LoadedScenes.Find(x => x.GetType() == sceneType);
            if (loadedScene == null)
            {
                Debug.LogError("Scene you're trying to temp unload is not loaded");
                return;
            }

            loadedScene.TempUnload();
        }

        public static void ResumeScene(Scene scene)
        {
            var sceneType = SceneType(scene);

            var loadedScene = LoadedScenes.Find(x => x.GetType() == sceneType);
            if (loadedScene == null)
            {
                Debug.LogError("Scene you're trying to resume is not loaded");
                return;
            }

            loadedScene.Resume();
        }

        private static ISceneBase NewScene(Scene scene)
        {
            var sceneType = SceneType(scene);
            return (ISceneBase)Activator.CreateInstance(sceneType);
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