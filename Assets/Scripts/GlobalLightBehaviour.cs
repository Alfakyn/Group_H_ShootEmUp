using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalLightBehaviour : MonoBehaviour
{
    public UnityEngine.Experimental.Rendering.Universal.Light2D global_light;
    float light_intensity_timer;
    const float SHADING_RATE = 200;
    // Start is called before the first frame update
    void Start()
    {
        light_intensity_timer = SHADING_RATE;  
    }

    // Update is called once per frame
    private void Update()
    {
        if (light_intensity_timer > 0)
        {
            global_light.intensity -= Time.deltaTime / SHADING_RATE;
        }
    }
}
