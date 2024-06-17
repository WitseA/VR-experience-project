using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    private Light _light;
    void Start()
    {
        _light = GetComponentInChildren<Light>();
    }

    public void LightOn()
    {
        _light.enabled = true;
    }
    public void LightOff()
    {
        _light.enabled = false;
    }
}
