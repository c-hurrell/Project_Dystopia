using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneHandler
{
    public interface ISceneBase
    {
        public string SceneName { get; }
        public void Pause();
        public void Resume();
        public void TempUnload();
        public AsyncOperation LoadAsync(bool overwrite = true);
        public AsyncOperation UnloadAsync();
    }

    /// <summary>
    /// Base class for a possible scene
    /// </summary>
    public abstract class SceneBase<TPauseType> : MonoBehaviour, ISceneBase where TPauseType : SceneObject
    {
        public abstract string SceneName { get; }

        public virtual void Pause()
        {
            var pauseObjects = FindObjectsOfType<TPauseType>();

            foreach (var pauseObject in pauseObjects)
            {
                pauseObject.Pause();
            }
        }

        public virtual void Resume()
        {
            var pauseObjects = FindObjectsOfType<TPauseType>();

            foreach (var pauseObject in pauseObjects)
            {
                pauseObject.Resume();
            }
        }

        public virtual void TempUnload()
        {
            var pauseObjects = FindObjectsOfType<TPauseType>();

            foreach (var pauseObject in pauseObjects)
            {
                pauseObject.TempUnload();
            }
        }

        public virtual AsyncOperation LoadAsync(bool overwrite = true)
        {
            return SceneManager.LoadSceneAsync(SceneName, overwrite ? LoadSceneMode.Single : LoadSceneMode.Additive);
        }

        public virtual AsyncOperation UnloadAsync()
        {
            return SceneManager.UnloadSceneAsync(SceneName);
        }
    }
}