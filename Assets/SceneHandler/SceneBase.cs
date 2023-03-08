using System.Collections;

namespace SceneHandler
{
    public abstract class SceneBase
    {
        /// <summary>
        /// Scene name to load
        /// </summary>
        public abstract string SceneName { get; }

        /// <summary>
        /// Coroutine called when the scene is about to load
        /// </summary>
        public virtual IEnumerator Load()
        {
            yield return null;
        }
    }
}