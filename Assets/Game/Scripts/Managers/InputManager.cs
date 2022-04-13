using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoSingleton<InputManager>
{
    private Vector2 mouseInput;
    private Vector2 rawMouseInput;
    public Vector2 MouseInput => mouseInput;
    public Vector2 RawMouseInput => rawMouseInput;
    private Vector2 previousMousePosition;
    private Vector2 previousRawMousePosition;
    private bool mouseClicking;
    private bool mouseReleased;
    public bool MouseClicking => mouseClicking;
    public bool MouseReleased => mouseReleased;

    private void Awake()
    {
        Debug.Log(Instance);
    }
    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }
    private Vector2 mousePositionCM
    {
        get
        {
            Vector2 pixels = Input.mousePosition;
            var inches = pixels / Screen.dpi;
            var centimeters = inches * 2.54f;
            return centimeters;
        }
    }
    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseClicking = true;
        }
        if (Input.GetMouseButton(0))
        {

        }
        if (Input.GetMouseButtonUp(0))
        {
            mouseClicking = false;
            
        }
    }
}