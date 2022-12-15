using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExplorationControllerKeyboard : MonoBehaviour, IKeyboardInputs
{
    //Player Movement Variables
    [SerializeField] private CharacterController characterCtrl;
    [SerializeField] private Transform groundChecker;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float speed = 4, turningSpeed, turningTime = 0.1f;
    [SerializeField] private float groundCheckerLength = 0.2f;
    [SerializeField] private float gravityAcceleration = 10;
    [SerializeField] private bool isGrounded = false;
    [SerializeField] private Vector3 gravityVector;
    public float modifier;
    public bool modifierOn;
    private RaycastHit over;

    //Input Variables
    private Vector3 movementDir, inputs;

    //Menu Variables
    private RectTransform menu;

    public void DirectionalInputs(Vector2 dir) {
        
        inputs = new Vector3(dir.x, 0, dir.y);
        movementDir.x = 0;
        movementDir.z = 0;
        movementDir = movementDir + (inputs.normalized * speed);
        //movementDir = new Vector3(dir.x, 0, dir.y) * speed;
    }

    public void UIInputs(bool start, bool select) {
    }

    public void BasicInputs(bool buttonA, bool buttonB, bool buttonX, bool buttonY) {
    }

    void Update() {
        //if(isGrounded)
            Movement();
        
        Gravity();
    }

    private void Movement(){
        //animator.SetFloat("Move", movementDir.magnetude);
        modifier = modifierOn ? FindObjectOfType<cameraTripod>().transform.eulerAngles.y : 0;
        modifier = Mathf.RoundToInt(modifier);
        if (inputs.normalized != Vector3.zero)
        {
            float targetRotation;
            if(modifierOn)
                targetRotation = Mathf.Atan2(inputs.x, inputs.z) * Mathf.Rad2Deg + modifier;
            else
                targetRotation = Mathf.Atan2(inputs.x, inputs.z) * Mathf.Rad2Deg;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turningSpeed, turningTime);
        }
        float hMov = movementDir.x * Mathf.Cos(modifier * Mathf.Deg2Rad) + movementDir.z * Mathf.Sin(modifier * Mathf.Deg2Rad);
        float vMov = (movementDir.x * Mathf.Sin(modifier * Mathf.Deg2Rad)) * -1 + movementDir.z * Mathf.Cos(modifier * Mathf.Deg2Rad);
        characterCtrl.Move(new Vector3(hMov, 0, vMov) * speed * Time.deltaTime);
    }

    private void Gravity() {
        isGrounded = Physics.Raycast(groundChecker.position, -groundChecker.up, out over, groundCheckerLength, groundLayer);
        Debug.DrawLine(groundChecker.position, groundChecker.position - new Vector3(0,groundCheckerLength,0), Color.red);

        if (isGrounded && gravityVector.y < 0) {
            gravityVector.y = -gravityAcceleration -(-1);
        }
        gravityVector.y += -gravityAcceleration * Time.deltaTime;

        characterCtrl.Move(gravityVector * Time.deltaTime);
    }
}