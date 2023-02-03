using System;
using System.Collections.Generic;
using Scenes.SceneTypes;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SceneHandler
{
    public static class GlobalSceneHandler
    {
        /// <summary>
        /// List of all loaded scenes
        /// Used for unloading a scene safely
        /// </summary>
        private static readonly List<ISceneBase> LoadedScenes = new();

        private static readonly GameObject[] SceneObjects;

        static GlobalSceneHandler()
        {
            var allScenes = Enum.GetValues(typeof(Scene));
            SceneObjects = new GameObject[allScenes.Length];

            for (var i = 0; i < allScenes.Length; i++)
            {
                var scene = (Scene)allScenes.GetValue(i);
                var component = SceneType(scene);
                SceneObjects[i] = NewObjectWithComponent(component);
            }
        }

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

        public static void PauseAllScenes()
        {
            foreach (var loadedScene in LoadedScenes)
            {
                loadedScene.Pause();
            }
        }

        // public static void PauseScene(Scene scene)
        // {
        //     var sceneType = SceneType(scene);
        //
        //     var loadedScene = LoadedScenes.Find(x => x.GetType() == sceneType);
        //     if (loadedScene == null)
        //     {
        //         Debug.LogError("Scene you're trying to pause is not loaded");
        //         return;
        //     }
        //
        //     loadedScene.Pause();
        // }

        public static void TempUnloadAllScenes()
        {
            foreach (var loadedScene in LoadedScenes)
            {
                loadedScene.TempUnload();
            }
        }

        // public static void TempUnloadScene(Scene scene)
        // {
        //     var sceneType = SceneType(scene);
        //
        //     var loadedScene = LoadedScenes.Find(x => x.GetType() == sceneType);
        //     if (loadedScene == null)
        //     {
        //         Debug.LogError("Scene you're trying to temp unload is not loaded");
        //         return;
        //     }
        //
        //     loadedScene.TempUnload();
        // }

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


        private static GameObject NewObjectWithComponent(Type component)
        {
            var obj = new GameObject();
            obj.AddComponent(component);
            return obj;
        }

        private static ISceneBase NewScene(Scene scene)
        {
            return Object.Instantiate(SceneObjects[(int)scene]).GetComponent<ISceneBase>();
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