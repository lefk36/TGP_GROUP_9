using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTestMovement : MonoBehaviour
{ 
    public float movementSpeed = 500f;
    //super brain dead ez pz movement for testing the AI
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);
        } 
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * Time.deltaTime * movementSpeed);
        } 
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * Time.deltaTime * movementSpeed);
        } 
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * Time.deltaTime * movementSpeed);
        }       
    }
}
