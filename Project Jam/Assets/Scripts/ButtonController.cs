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
