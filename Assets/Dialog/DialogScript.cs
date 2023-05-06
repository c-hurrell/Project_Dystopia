using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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

        private TextMeshProUGUI _text;

        // cached values
        private static bool _alreadyCached;
        private static float _baseWidth;
        private static float _baseHeight;
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
            var pressToContinueText = dialogBase.Find("PressToContinueText");

            _text = text.GetComponent<TextMeshProUGUI>();

            // handle size
            var textRect = text.GetComponent<RectTransform>();
            var bgRect = bg.GetComponent<RectTransform>();
            var pressToContinueTextRect = pressToContinueText.GetComponent<RectTransform>();

            // calculate size
            if (!_alreadyCached)
            {
                // size settings
                const float baseRatioToScreen = 1 / 1.5f;
                const float baseHeightRatio = 1 / 5f;

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

                _textWidth = _baseWidth * textWidthRatioToBase;
                _textHeight = _baseHeight * textHeightRatioToBase;

                _alreadyCached = true;
            }

            // set size
            bgRect.sizeDelta = new(_baseWidth, _baseHeight);
            textRect.sizeDelta = new(_textWidth, _textHeight);
            pressToContinueTextRect.sizeDelta = new(_textWidth, _textHeight);

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

            // start dialog coroutine
            StartCoroutine(DialogCoroutine());
        }

        private IEnumerator DialogCoroutine()
        {
            const float charDelay = 0.05f;
            const int maxCharsPerLine = 40;
            var dialogsQueue = new Queue<string>(dialogs);

            yield return new WaitForSeconds(0.5f);

            while (dialogsQueue.Count > 0)
            {
                var currentDialog = dialogsQueue.Dequeue();
                // remove newlines
                currentDialog = currentDialog.Replace("\n", " ");

                _text.text = "";

                var fastForward = false;
                var fastForwardWaitForNoInput = Input.anyKeyDown;
                var waitingForSpace = false;
                var splitWait = maxCharsPerLine;
                foreach (var c in currentDialog)
                {
                    _text.text += c;
                    splitWait--;

                    // wait for space to split by words
                    if (waitingForSpace && c == ' ')
                    {
                        waitingForSpace = false;
                        splitWait = maxCharsPerLine;
                        _text.text += "\n";
                    }

                    if (splitWait == 0)
                    {
                        waitingForSpace = true;
                    }

                    if (fastForward)
                    {
                        continue;
                    }

                    var waitTime = charDelay;
                    while (waitTime > 0f)
                    {
                        waitTime -= Time.deltaTime;

                        if (!Input.anyKeyDown)
                        {
                            fastForwardWaitForNoInput = false;
                        }

                        if (!fastForwardWaitForNoInput && Input.anyKeyDown)
                        {
                            fastForward = true;
                            break;
                        }

                        yield return null;
                    }
                }

                if (fastForward)
                {
                    yield return new WaitWhile(() => Input.anyKeyDown);
                }

                // wait for user input
                yield return new WaitUntil(() => Input.anyKeyDown);
            }

            yield return new WaitForSeconds(0.1f);

            Destroy(gameObject);
        }
    }
}