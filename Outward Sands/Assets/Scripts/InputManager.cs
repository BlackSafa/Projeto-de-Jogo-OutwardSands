using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    public GameObject inputTarget;

    void Update() {
        Vector2 dir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        
        inputTarget.GetComponent<IKeyboardInputs>().DirectionalInputs(dir);
    }
}

public interface IKeyboardInputs {
    public void DirectionalInputs(Vector2 dir);
    public void BasicInputs(bool buttonA, bool buttonB, bool buttonX, bool buttonY);
    public void UIInputs(bool startButton, bool selectButton);
}

public interface IJoystickInputs : IKeyboardInputs {
    public void AnalogInput(Vector2 dir, bool button);
    public void SecondaryAnalogInput(Vector2 dir, bool button);
    public void ShouderInputs(bool shouderL, bool shouderR);
    public void TriggerInputs(float triggerL, float triggerR);
}
