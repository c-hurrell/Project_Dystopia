using System;
using UnityEngine;

namespace Combat
{
    public class CombatEventHandle : MonoBehaviour
    {
        private static CombatEventHandle _instance;

        private void Start()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
            OnCombatStart?.Invoke();
        }

        private void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
            }
        }

        public static event Action OnCombatStart;
        public static event Action OnCombatWin;
        public static event Action OnCombatLose;

        public static void ExitCombat(bool win)
        {
            if (win)
            {
                OnCombatWin?.Invoke();
            }
            else
            {
                OnCombatLose?.Invoke();
            }
        }
    }
}