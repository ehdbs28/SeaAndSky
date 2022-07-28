using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class AreaVolume : MonoBehaviour
{
    private Volume volume;
    private ColorAdjustments colorAdjustments;
    private float originalSaturation;

    void Awake()
    {
        volume = GetComponent<Volume>();
        volume.profile.TryGet<ColorAdjustments>(out colorAdjustments);

        originalSaturation = colorAdjustments.saturation.value;

        EventManager<AreaState>.StartListening("ChangeArea", OnChangeArea);
    }

    private void OnChangeArea(AreaState area)
    {
        if (name.Contains(area.ToString()))
        {
            colorAdjustments.saturation.value = originalSaturation;
        }
        else
        {
            colorAdjustments.saturation.value =  - 80f;
        }
    }

    private void OnDestroy()
    {
        EventManager<AreaState>.StopListening("ChangeArea", OnChangeArea);
    }
}
