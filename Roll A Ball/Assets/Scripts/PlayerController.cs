using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    #region Variables
    public float moveSpeed = 5.5f;
    public float jumpForce = 10.5f;
    public float gravity = 20f;

    private int count;
    public int winValue = 14;

    public TextMeshProUGUI countText;
    public GameObject winText;
    public List<GameObject> pickUps = new List<GameObject>();

    public CharacterController controller;
    private Vector3 moveDirection = Vector3.zero;

    public LineRenderer jumpPreviewLine;
    public int jumpPreviewResolution = 50;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();

        count = 0;
        SetCountText();
        winText.SetActive(false);

        jumpPreviewLine.positionCount = jumpPreviewResolution;
        jumpPreviewLine.enabled = false;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= winValue)
        {
            winText.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(xInput, 0f, yInput);
        movement = transform.TransformDirection(movement);
        movement *= moveSpeed;

        if (controller.isGrounded)
        {
            moveDirection = movement;
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpForce;
            }
        }
        else
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        controller.Move(moveDirection * Time.deltaTime);

        if (pickUps.Count > 0)
        {
            for (int i = 0; i < pickUps.Count; i++)
            {
                if (pickUps[i] == null)
                {
                    pickUps.RemoveAt(i);
                    i--;
                }
            }
        }

        if (Input.GetButton("Jump"))
        {
            ShowJumpPreview();
        }
        else
        {
            HideJumpPreview();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            Destroy(other.gameObject);
            count = count + 1;
            SetCountText();
        }
    }

 void ShowJumpPreview()
{
    jumpPreviewLine.enabled = true;

    float timeInterval = 0.1f; // Adjust this for smoother preview

    // Calculate landing position
    float totalAirTime = (2f * jumpForce) / gravity;
    Vector3 landingPosition = transform.position + moveDirection * moveSpeed * totalAirTime;

    // Draw line to landing position
    jumpPreviewLine.positionCount = 2;
    jumpPreviewLine.SetPosition(0, transform.position);
    jumpPreviewLine.SetPosition(1, landingPosition);
}






    void HideJumpPreview()
    {
        jumpPreviewLine.enabled = false;
    }
}
