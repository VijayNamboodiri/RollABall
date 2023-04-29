using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    #region Variables
    public float moveSpeed = 5.5f;
    public float jumpForce = 10.5f;
    public float gravity = 20f;

    public CharacterController controller;
    private Vector3 moveDirection = Vector3.zero;
    #endregion 
    
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");
        
        Vector3 movement = new Vector3(xInput,0f,yInput);
        movement = transform.TransformDirection(movement);
        movement*=moveSpeed;
        
        if(controller.isGrounded)
        {
            moveDirection = movement;
            if(Input.GetButton("Jump"))
            {
                moveDirection.y = jumpForce;
            }
        }
        else
        {
            moveDirection.y -= gravity*Time.deltaTime;
            
        }
        
        controller. Move(moveDirection*Time.deltaTime);
    }
}
