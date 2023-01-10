using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
//SCRIPT TO CONTROL THE CAR

    [Header("Car settings")]
    public float driftFactor = 0.95f;
    public float accelerationFactor = 30.0f;
    public float turnFactor = 3.5f;
    public float maxSpeed = 20;


    // Variables
    float accelarationInput = 0;
    float steeringInput = 0;

    float roationAngle = 0;
    float velocityVsUp = 0;

    //Component RIGIDBODY
    Rigidbody2D car;

    void Awake()
    {
        car = GetComponent<Rigidbody2D>();

    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        // Rigid Body Own Forward Method
        ApplyEngineForce();

        // Algorithm that I found that improves turning
        KillOrthogonalVelocity();

        // Telling the RigidBody to Turn the Sprite
        ApplySteering();
    }


    void ApplyEngineForce()
    {

        velocityVsUp = Vector2.Dot(transform.up, car.velocity);

        // Max Speed
        if (velocityVsUp > maxSpeed && accelarationInput > 0)
            return;

        //Slow down
        if (velocityVsUp < -maxSpeed * 0.5f && accelarationInput < 0)
            return;

        //Stop from forever moving
        if (car.velocity.sqrMagnitude > maxSpeed * maxSpeed && accelarationInput > 0)
            return;

        //Using Rigidbody Drag Function to Slowdown car
        if (accelarationInput == 0)
            car.drag = Mathf.Lerp(car.drag, 3.0f, Time.fixedDeltaTime * 3);
        else car.drag = 0;

        // Forward Push using Rigid Body
        Vector2 engineForceVector = transform.up * accelarationInput * accelerationFactor;

        car.AddForce(engineForceVector, ForceMode2D.Force);

    }

    void ApplySteering()
    {

        float minSpeedBeforeAllowTurningFactor = (car.velocity.magnitude / 8);
        minSpeedBeforeAllowTurningFactor = Mathf.Clamp01(minSpeedBeforeAllowTurningFactor);

        roationAngle -= steeringInput * turnFactor * minSpeedBeforeAllowTurningFactor;

        car.MoveRotation(roationAngle);
    }

    public void SetInputVector(Vector2 inputVector)
    {
        steeringInput = inputVector.x;
        accelarationInput = inputVector.y;
    }

    void KillOrthogonalVelocity()
    {
        Vector2 forwardVelocity = transform.up * Vector2.Dot(car.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(car.velocity, transform.right);


        car.velocity = forwardVelocity + rightVelocity * driftFactor;
    }float GetLateralVelocity()
    {
        //Returns how how fast the car is moving sideways. 
        return Vector2.Dot(transform.right, car.velocity);
    }

    public bool IsTireScreeching(out float lateralVelocity, out bool isBraking)
    {
        lateralVelocity = GetLateralVelocity();
        isBraking = false;

        //Check if we are moving forward and if the player is hitting the brakes. In that case the tires should screech.
        if (accelarationInput < 0 && velocityVsUp > 0)
        {
            isBraking = true;
            return true;
        }

        //If we have a lot of side movement then the tires should be screeching
        if (Mathf.Abs(GetLateralVelocity()) > 3.0f)
            return true;

        return false;
    }public float GetVelocityMagnitude()
    {
        return car.velocity.magnitude;
    }
}
