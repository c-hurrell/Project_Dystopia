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
            var background = backgrounds[(int)currentArea];
            GetComponent<SpriteRenderer>().sprite = background;
        }
    }
}