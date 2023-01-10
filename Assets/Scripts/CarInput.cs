using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInput : MonoBehaviour
{

    CarController CarController;

    void Awake()
    {
        CarController = GetComponent<CarController>();
    }


  

    // Update is called once per frame
    void Update()
    {
        //Accepting input from user  to controll car and pass to car control
        Vector2 inputVector = Vector2.zero;

        inputVector.x = Input.GetAxis("Horizontal");
        inputVector.y = Input.GetAxis("Vertical");

        CarController.SetInputVector(inputVector);
    }
}
