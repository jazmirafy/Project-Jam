using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class Greyscaler : MonoBehaviour
{
    public Volume volume;
    private ColorAdjustments colorAdjustments;
    public float colorChangeAmount = 0;
    // Start is called before the first frame update
    void Start()
    {
        // Ensure the Volume has a profile
        if (volume != null && volume.profile != null)
        {
            // Try to get the ColorAdjustment from the Volume Profile
            if (!volume.profile.TryGet<ColorAdjustments>(out colorAdjustments))
            {
                Debug.LogWarning("Color Adjustments component not found in Volume Profile.");
            }
            else
            {
                // Modify Color Adjustment Saturation settings
                colorAdjustments.saturation.overrideState = true;
                colorAdjustments.saturation.value = 0f; // Set intensity to 0 (no color changes)
                
                Debug.Log("Color Adjustment settings updated!");
            }
        }
        else
        {
            Debug.LogError("Volume or Volume Profile is missing!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addColor()
    {
        if (colorAdjustments.saturation.value <= 0)
        {
            colorAdjustments.saturation.value += colorChangeAmount;
        }
    }

    public void subtractColor()
    {
        if (colorAdjustments.saturation.value >= -100)
        {
            colorAdjustments.saturation.value -= colorChangeAmount;
        }
    }
}
