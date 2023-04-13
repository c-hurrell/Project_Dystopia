using System.Collections.Generic;
using UnityEngine;
using World;

namespace Combat
{
    public class CombatBg : MonoBehaviour
    {
        [SerializeField] private List<Sprite> backgrounds;

        private void Start()
        {
            var currentArea = ProgressionStatus.CurrentArea;
            if (backgrounds.Count <= (int)currentArea)
                return;
            var background = backgrounds[(int)currentArea];
            GetComponent<SpriteRenderer>().sprite = background;
        }
    }
}