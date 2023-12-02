using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningLight : MonoBehaviour
{
    public Light warningLight;
    public float cycleDuration = 2.0f;  // Total time for one on-off cycle

    private void Update()
    {
        // Calculate the interpolation factor using a sine function
        float lerpFactor = 0.5f + 0.5f * Mathf.Sin(2 * Mathf.PI * Time.time / cycleDuration);

        // Interpolate between on and off intensities
        warningLight.intensity = Mathf.Lerp(0.0f, 2.0f, lerpFactor);
    }
}
