using UnityEngine;

namespace Testing
{
    // press p to spawn a dialog
    public class DialogSpawner : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                // ReSharper disable StringLiteralTypo
                Dialog.DialogManager.ShowDialog(new[]
                {
                    "Hello", "World", "Foo", "laksdjglaskdjgasldkgnasldkjgasldkdjgasdglkjadsfg\nalskdfjlaskjdgadfhg"
                });
                // ReSharper restore StringLiteralTypo
            }
        }
    }
}