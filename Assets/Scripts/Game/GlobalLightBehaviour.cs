using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalLightBehaviour : MonoBehaviour
{
    public UnityEngine.Experimental.Rendering.Universal.Light2D global_light;
    float light_intensity_timer;
    const float BRIGHTNESS_RATE = 1000;
    const float MAX_BRIGHTNESS = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (global_light.intensity <= MAX_BRIGHTNESS)
        {
            global_light.intensity += Time.deltaTime / BRIGHTNESS_RATE;
        }
       
    }
}
