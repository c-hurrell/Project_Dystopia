using TMPro;
using UnityEngine;

namespace Dialog
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class PressToContinue : MonoBehaviour
    {
        private bool _showing;
        private const float ShowTime = 0.5f;
        private float _showTime;

        private TextMeshProUGUI _text;

        private void Start()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            if (_showTime <= 0f)
            {
                _showTime = ShowTime;
                _showing = !_showing;

                _text.enabled = _showing;
            }

            _showTime -= Time.deltaTime;
        }
    }
}