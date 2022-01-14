using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //public variables
    [Min(0f)] public float acceleration;
    [Min(0f)] public float targetSpeed;
    [Min(0f)] public float modelRotationSmoothnessFactor;
    public bool lockMovement; //Access this property from other scripts whenever they move the character instead
    [HideInInspector] bool readyForAction; //Use this property in other scripts to check if the player is currently in the middle of another action (example: atttacking or knocked on the ground)

    //character object and its components
    private GameObject character;
    private new Collider collider;
    private new Rigidbody rigidbody;

    //model object and its components
    private GameObject model;
    private Animator animator;

    //maths variables
    private Vector3 characterDirection;
    private Vector3 modelDirection;
    void Start()
    {
        //load character information
        character = transform.Find("Character").gameObject;
        if (character != null)
        {
            collider = character.GetComponent<Collider>();
            rigidbody = character.GetComponent<Rigidbody>();
            //load model information
            model = character.transform.Find("Model").gameObject;
            if (model != null)
            {
                animator = model.GetComponent<Animator>();
            }
            else Debug.Log("No child object with the name 'Model' was found");
        }
        else Debug.Log("No child object with the name 'Character' was found");



    }
    void Update()
    {
        //update handles logic such as input. Physics calculations are done in fixed update
        Vector3 inputDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
    }
}
