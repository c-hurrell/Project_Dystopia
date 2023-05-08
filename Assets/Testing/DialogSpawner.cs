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
                    @"According to all known laws of aviation, there is no way a bee should be able to fly.
Its wings are too small to get its fat little body off the ground.
The bee, of course, flies anyway because bees don't care what humans think is impossible.",
                    "Yellow, black. Yellow, black. Yellow, black. Yellow, black.",
                    "Ooh, black and yellow!"
                });
                // ReSharper restore StringLiteralTypo
            }
        }
    }
}