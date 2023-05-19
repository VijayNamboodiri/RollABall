using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;   
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    #region Variables
    public float moveSpeed = 5.5f;
    public float jumpForce = 10.5f;
    public float gravity = 20f;

    private int count;
    public int winValue = 14;//allows user to easily switch requirements for victory & UI will still work


    public TextMeshProUGUI countText;   
    public GameObject winText;
    public List<GameObject> pickUps = new List<GameObject>();


    public CharacterController controller;
    private Vector3 moveDirection = Vector3.zero;
    #endregion 
    
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();

        count = 0;
        SetCountText();
        winText.SetActive(false);
    }
 
    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if(count >= winValue)
        {
            winText.SetActive(true);
        }
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

    if(pickUps.Count > 0)
    {
        for(int i = 0; i < pickUps.Count; i++) //A loop that delete's an unused index
        {
            if(pickUps[i] == null)
            {
                pickUps.RemoveAt(i);

                i--;
            }
        }
    }

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PickUp"))
        {
            Destroy(other.gameObject);//removes game objects
            count = count + 1;  
            SetCountText();
        }
    }       
}
