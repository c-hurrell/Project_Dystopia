using UnityEngine;

namespace SceneHandler
{
    public abstract class SceneObject : MonoBehaviour
    {
        private Vector3 _originalScale;

        /// <summary>
        /// Helper bool that tells if the scene is paused
        /// </summary>
        public bool IsPaused { get; set; }

        /// <summary>
        /// Ran when a scene is paused
        /// </summary>
        public abstract void Pause();

        /// <summary>
        /// Ran when scene is resumed
        /// Also loads back the objects that was hidden
        /// Default behaviour is restoring the original scale
        /// </summary>
        public virtual void Resume()
        {
            transform.localScale = _originalScale;
        }

        /// <summary>
        /// Ran when scene is temporarily unloaded
        /// Run code to hide objects
        /// Default behaviour is shrinking the object to 0
        /// </summary>
        public virtual void TempUnload()
        {
            var t = transform;

            _originalScale = t.localScale;
            t.localScale = Vector3.zero;
        }
    }
}