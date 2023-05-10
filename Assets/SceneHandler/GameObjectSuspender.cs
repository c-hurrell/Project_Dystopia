using System.Collections.Generic;
using UnityEngine;

namespace SceneHandler
{
    public class GameObjectSuspender
    {
        private readonly List<GameObject> _inactiveGameObjects = new();

        public void SuspendAll()
        {
            _inactiveGameObjects.AddRange(Object.FindObjectsOfType<GameObject>());
            foreach (var inactiveObj in _inactiveGameObjects)
            {
                inactiveObj.SetActive(false);
            }
        }

        public void ResumeAll()
        {
            foreach (var inactiveObj in _inactiveGameObjects)
            {
                inactiveObj.SetActive(true);
            }

            _inactiveGameObjects.Clear();
        }
    }
}