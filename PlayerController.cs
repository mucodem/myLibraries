using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Variables")]
    [SerializeField] FloatingJoystick joystick;
    [SerializeField] float rotationSpeed;
    [SerializeField] float forwardSpeed;

    [Header("Player Components")]
    Rigidbody rb;

    public float joystickH;
    public float joystickV;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        joystickH = joystick.Horizontal;
        joystickV = joystick.Vertical;

        rb.velocity = forwardSpeed * Time.deltaTime * transform.forward;

        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            Rotate(new Vector3(joystick.Horizontal, 0, joystick.Vertical));
        }
           
    }

    void Rotate(Vector3 direction)
    {
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, direction, rotationSpeed * Time.deltaTime, .1f);
        Quaternion _newRotation = Quaternion.LookRotation(desiredForward);
        transform.rotation = _newRotation;
    }

    //void RotateMethod2()
    //{
    //    Vector3 lookAtPos = leader.forward;
    //    Quaternion newRotation = Quaternion.LookRotation(lookAtPos, transform.up);
    //    leader.transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 8);
    //}
}
