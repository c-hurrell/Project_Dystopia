using System.Collections.Generic;
using UnityEngine;

namespace Dialog
{
    public static class DialogManager
    {
        private static readonly GameObject DialogPrefab;

        private static bool _isDialogActive;
        private static readonly Queue<GameObject> PendingDialogs = new();

        static DialogManager()
        {
            DialogPrefab = Resources.Load<GameObject>("Dialog");
        }

        public static void ShowDialog(string[] dialogs, DialogPosition position = DialogPosition.TopMiddle)
        {
            var obj = Object.Instantiate(DialogPrefab);
            var dialogScript = obj.GetComponent<DialogScript>();
            dialogScript.dialogs = dialogs;
            dialogScript.position = position;
            dialogScript.OnDialogEnd = ProcessPendingDialogs;

            if (_isDialogActive)
            {
                PendingDialogs.Enqueue(obj);
                obj.SetActive(false);
            }
            else
            {
                _isDialogActive = true;
            }
        }

        private static void ProcessPendingDialogs()
        {
            if (PendingDialogs.Count == 0)
            {
                _isDialogActive = false;
                return;
            }

            var dialog = PendingDialogs.Dequeue();
            dialog.SetActive(true);
        }
    }
}