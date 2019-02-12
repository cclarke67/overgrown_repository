using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 8 Directional movement for the character, it also stops and faces the current direction when no input is detected
/// </summary>
public class FixedCameraMove : MonoBehaviour {

    public float Velocity = 5f;// controls their movement speed
    public float TurnSpeed = 10;// controls their turning speed

    Vector2 _input;// stores input in the x/y coordinates 
    float _angle;
    bool anyPlayerInput;// is there any input from the player

    Quaternion _targetRotation;
    Rigidbody _rb;


    void Start () {
        _rb = GetComponent<Rigidbody>();      
	}

    void Update()
    {
        GetInput();

        if (Mathf.Abs(_input.x) < 1 && Mathf.Abs(_input.y) < 1)// .Abs means absolute, this will make the number positive
        { anyPlayerInput = false; return; }
        else
        { anyPlayerInput = true; }


        if (Input.GetButtonDown("Fire1"))//dash function
        {
            _rb.AddForce(this.transform.forward * 10, ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        if (anyPlayerInput)
        {
            CalculateDirection();
            Rotate();

            _rb.MovePosition(this.transform.position + this.transform.forward * Velocity * Time.fixedDeltaTime);
        }
    }



    void GetInput()
    {
        _input.x = Input.GetAxisRaw("Horizontal");
        _input.y = Input.GetAxisRaw("Vertical");
    }

    //Calculate the direction to face towards
    void CalculateDirection()
    {
        _angle = Mathf.Atan2(_input.x, _input.y);   //
        _angle = Mathf.Rad2Deg * _angle;            //these calculate the angle from the input and convert it to degrees
    }

    //Rotate character towards calculated angle
    void Rotate()
    {
        _targetRotation = Quaternion.Euler(0, _angle, 0);// converts Vector3 to Eular
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, _targetRotation, TurnSpeed * Time.deltaTime);//Slerp is basically lerp. This'll interpolate between the different rotations
    }
}
