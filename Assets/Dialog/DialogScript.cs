using UnityEngine;

namespace Dialog
{
    public enum DialogPosition
    {
        TopMiddle,
        BottomMiddle
    }

    public class DialogScript : MonoBehaviour
    {
        public string[] dialogs;
        public DialogPosition position;
    }
}