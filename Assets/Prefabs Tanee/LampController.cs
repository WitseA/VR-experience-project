using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampController : MonoBehaviour
{
    private Light lampLight;
    private bool isOn = false;

    void Start()
    {
        lampLight = GetComponentInChildren<Light>();
        if (lampLight == null)
        {
            Debug.LogError("LampController: No Light component found in children.");
        }
    }

    public void ToggleLight()
    {
        if (lampLight != null)
        {
            isOn = !isOn;
            lampLight.enabled = isOn;
        }
    }
}
