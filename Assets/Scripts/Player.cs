using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;


public class Player : MonoBehaviour
{
    public static Player instance;

    [SerializeField] Rigidbody2D playerRigidBody;
    [SerializeField] Animator playerAnimator;
    [SerializeField] int moveSpeed;

    public string playerTransition;
    public bool movementDisabled = false;
    // Start is called before the first frame update
    void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        float verticalMovement = Input.GetAxisRaw("Vertical");

        if (movementDisabled)
        {
            playerRigidBody.velocity = Vector2.zero;
        }
        else
        {
            playerRigidBody.velocity = new Vector2(horizontalMovement, verticalMovement) * moveSpeed;
        }       

        playerAnimator.SetFloat("movementX", playerRigidBody.velocity.x);
        playerAnimator.SetFloat("movementY", playerRigidBody.velocity.y);

        if (horizontalMovement == 1 || horizontalMovement == -1 || verticalMovement == 1 || verticalMovement == -1)
        {
            if (!movementDisabled)
            {
                playerAnimator.SetFloat("lastX", horizontalMovement);
                playerAnimator.SetFloat("lastY", verticalMovement);
            }
        }
    }   
}
