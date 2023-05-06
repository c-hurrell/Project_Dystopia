using UnityEngine;

namespace Dialog
{
    public static class DialogManager
    {
        private static readonly GameObject DialogPrefab;

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
        }
    }
}