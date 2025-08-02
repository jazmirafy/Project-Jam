using UnityEngine;

public class ControllerDebugger : MonoBehaviour
{
    void Update()
    {
        // Show connected controller names
        // foreach (string name in Input.GetJoystickNames())
        // {
        //     if (!string.IsNullOrEmpty(name))
        //         Debug.Log("Controller connected: " + name);
        // }

        // // Check Joystick Buttons
        // for (int i = 0; i <= 19; i++)
        // {
        //     if (Input.GetKeyDown((KeyCode)(KeyCode.JoystickButton0 + i)))
        //     {
        //         Debug.Log($"JoystickButton{i} was pressed");
        //     }
        // }

        // // Check Common Trigger Axes
        // Debug.Log($"L2 (Axis 10): {Input.GetAxisRaw("L2")}");
        // Debug.Log($"R2 (Axis 11): {Input.GetAxisRaw("R2")}");

        // // Optional: Check all 20 axes
        // for (int a = 1; a <= 20; a++)
        // {
        //     try
        //     {
        //         float val = Input.GetAxisRaw("Axis " + a);
        //         if (Mathf.Abs(val) > 0.01f)
        //             Debug.Log($"Axis {a}: {val}");
        //     }
        //     catch { }
        // }
        for (int i = 0; i < 20; i++) {
            if (Input.GetKeyDown("joystick button " + i)) {
                Debug.Log("Button " + i + " was pressed");
            }
        }

    }
}
