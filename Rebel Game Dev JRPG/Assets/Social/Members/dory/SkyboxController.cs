using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// attempt dynamic skybox handling 10/15/23
// based off of https://www.youtube.com/watch?v=bftQ9W2a9TY 
public class SkyboxController : MonoBehaviour
{
    // similar to brandon's material instancing method
    [SerializeField] private Material baseSkyboxMaterial;
    private Material mat;

    // rgb values set by user dynamically
    private Vector3 rgba;

    /* setRGBAValue helper function
        @param pos RGBA position (must be between 0 and 3)
        @param val Value to set (must be between 0 and 255)
    */
    private void setRGBAValue(int pos, int val) {
        if(pos < 0 || pos > 3) {
            Debug.Log("Invalid position");
            return;
        }
        if(val < 0) {
            Debug.Log("RGBA value cannot be less than zero.");
            return;
        }
        rgba[pos] = (val > 255) ? 255 : val;
        return;
    }
    
    void Awake()
    {
        mat = baseSkyboxMaterial;
    }

    void Update()
    {
        RenderSettings.skybox = mat;
    }
}
