using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class MMButtonController : MonoBehaviour
{
    //private UnityEngine.UI.Image buttonSR;
    public GameObject defaultImage; //normal image look
    public GameObject pressedImage; //pressed down image look
    public KeyCode keyToPress;
    // Start is called before the first frame update
    void Start()
    {
        //buttonSR = GetComponent<UnityEngine.UI.Image>();

    }

    // Update is called once per frame
    void Update()
    {
        /*this basically says if the user presses the bind corresponding to the button, 
        show the pressed image. when the user unpresses the button, show the default
        image*/
        if (Input.GetKeyDown(keyToPress))
        {
            pressedImage.SetActive(true);
            defaultImage.SetActive(false);
            Debug.Log("time scale =" + Time.timeScale);
        }
        if (Input.GetKeyUp(keyToPress))
        {
            pressedImage.SetActive(false);
            defaultImage.SetActive(true);
        }
    }
}