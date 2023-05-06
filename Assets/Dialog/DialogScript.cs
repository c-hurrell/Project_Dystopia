using System;
using UnityEngine;

namespace Dialog
{
    public enum DialogPosition
    {
        TopMiddle
    }

    public class DialogScript : MonoBehaviour
    {
        public string[] dialogs;
        public DialogPosition position;

        // cached values
        private static bool _alreadyCached;
        private static float _baseWidth;
        private static float _baseHeight;
        private static float _imageWidth;
        private static float _imageHeight;
        private static float _textWidth;
        private static float _textHeight;

        private void Start()
        {
            if (dialogs == null || dialogs.Length == 0)
            {
                throw new("Dialogs are empty");
            }

            var dialogBase = transform.Find("Canvas").Find("DialogBase");
            var bg = dialogBase.Find("DialogBG");
            var text = dialogBase.Find("DialogText");

            // handle size
            var textRect = text.GetComponent<RectTransform>();
            var bgRect = bg.GetComponent<RectTransform>();

            // calculate size
            if (!_alreadyCached)
            {
                // size settings
                const float baseRatioToScreen = 1 / 1.5f;
                const float baseHeightRatio = 1 / 5f;

                const float imageWidthRatioToBase = 1f;
                const float imageHeightRatioToBase = 1f;

                const float textWidthRatioToBase = 0.948616600791f;
                const float textHeightRatioToBase = 0.83870967742f;

                if (Screen.width > Screen.height)
                {
                    _baseWidth = Screen.width * baseRatioToScreen;
                    _baseHeight = _baseWidth * baseHeightRatio;
                }
                else
                {
                    _baseHeight = Screen.height * baseRatioToScreen;
                    _baseWidth = _baseHeight / baseHeightRatio;
                }

                _imageWidth = _baseWidth * imageWidthRatioToBase;
                _imageHeight = _baseHeight * imageHeightRatioToBase;

                _textWidth = _baseWidth * textWidthRatioToBase;
                _textHeight = _baseHeight * textHeightRatioToBase;

                _alreadyCached = true;
            }

            // set size
            bgRect.sizeDelta = new(_imageWidth, _imageHeight);
            textRect.sizeDelta = new(_textWidth, _textHeight);

            // position calculation
            const float offsetFromEdge = 10f;

            var (x, y) = position switch
            {
                DialogPosition.TopMiddle => (0, -(_baseHeight / 2f + offsetFromEdge)),
                _ => throw new ArgumentOutOfRangeException()
            };

            // set position
            var baseRect = dialogBase.GetComponent<RectTransform>();
            baseRect.anchoredPosition = new(x, y);
        }
    }
}