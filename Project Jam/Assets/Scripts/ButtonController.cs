using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class ButtonController : MonoBehaviour
{
    private SpriteRenderer buttonSR;
    public Sprite defaultImage; //sprite for the images normal look
    public Sprite pressedImage; //sprite for how the button looks pressed down
    public KeyCode keyToPress;

    // Minimal additions for LT/RT axis support
    public bool useAxis = false;        // set true for LT/RT objects
    public string axisName = "";        // "LeftTrigger" or "RightTrigger"
    private bool isPressed = false;     // internal state for axis press
    private const float pressPoint = 0.3f;
    private const float releasePoint = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        buttonSR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        /*this basically says if the user presses the bind corresponding to the button, 
        show the pressed image sprite. when the user unpresses the button, show the default
        image sprite*/
        if (useAxis)
        {
            float v = Input.GetAxisRaw(axisName);

            if (!isPressed && v > pressPoint)
            {
                isPressed = true;
                buttonSR.sprite = pressedImage;
                Debug.Log("time scale =" + Time.timeScale);
            }
            if (isPressed && v <= releasePoint)
            {
                isPressed = false;
                buttonSR.sprite = defaultImage;
            }
        }
        else
        {
            if (Input.GetKeyDown(keyToPress))
            {
                buttonSR.sprite = pressedImage;
                Debug.Log("time scale =" + Time.timeScale);
            }
            if (Input.GetKeyUp(keyToPress))
            {
                buttonSR.sprite = defaultImage;
            }
        }
    }
}