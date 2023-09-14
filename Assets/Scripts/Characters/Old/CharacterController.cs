using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Cainos.CustomizablePixelCharacter;
public class CharacterController : MonoBehaviour
{
    

    private float verticalInput;
    private float horizontalInput;
    private bool jumpInput;

    public float runSpeedMax = 2.5f;                        // max run speed
    public float runAcc = 5.0f;                            // running acceleration

    public float jumpSpeed = 5.0f;                          // speed applied to character when jump
    public float jumpCooldown = 0.55f;                      // time to be able to jump again after landing
    public float jumpGravityMutiplier = 0.6f;               // gravity multiplier when character is jumping, should be within [0.0,1.0], set it to lower value so that the longer you press the jump button, the higher the character can jump    
    public float fallGravityMutiplier = 1.3f;               // gravity multiplier when character is falling, should be equal or greater than 1.0

    private CharacterSpecifications fx;                              // the FXCharacter script attached the character
    private CapsuleCollider2D collider2d;                   // Collider compoent on the character
    private Rigidbody2D rb2d;                               // Rigidbody2D component on the character


    private Vector2 posBot;                                 // local position of the character's middle bottom
    private Vector2 posTop;                                 // local position of the character's middle top



    public bool isGrounded = true;                                // is the character on ground
    public float groundCheckRadius = 0.17f;

    private void Awake()
    {
        fx = GetComponent<CharacterSpecifications>();
        collider2d = GetComponent<CapsuleCollider2D>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        posBot = collider2d.offset - new Vector2(0.0f, collider2d.size.y * 0.5f);
        posTop = collider2d.offset + new Vector2(0.0f, collider2d.size.y * 0.5f);
    }

    // Update is called once per frame
    private void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE //using pc controller
        PcInput();
#endif

#if UNITY_IOS || UNITY_ANDROID //using mobile controller
                 
#endif

#if UNITY_WEBGL //using webgl build

        if (Application.isMobilePlatform == true) //catch mobile device
        {
        //mobile
        }
        else
        {
        //pc
        }
#endif

        CheckIsGrounded();
    }


    private void PcInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        jumpInput = Input.GetKeyDown(KeyCode.Space);
        float currentSpeed = runSpeedMax;
        Move(horizontalInput, currentSpeed, jumpInput);
    }


    private void Move(float hInput, float speed, bool jInput)
    {
        Vector2 curVel = rb2d.velocity;

        if (Mathf.Abs(hInput) > 0)
        {
            rb2d.velocity = new Vector2(hInput* speed, 0);
            curVel.x = hInput * speed;
        }
        else
        {
            rb2d.velocity = new Vector2(0, 0);
            curVel.x = 0;
        }

        if(isGrounded && jInput)
        {
            isGrounded = false;

            curVel.y += jumpSpeed;
        }
        if (jInput && curVel.y > 0)
        {
            curVel.y += Physics.gravity.y * (jumpGravityMutiplier - 1.0f) * Time.deltaTime;
        }
        else if (curVel.y > 0)
        {
            curVel.y += Physics.gravity.y * (fallGravityMutiplier - 1.0f) * Time.deltaTime;
        }
        rb2d.velocity = curVel;
    }



    private void CheckIsGrounded()
    {
        isGrounded = false;
        Vector2 worldPos = transform.position;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(worldPos + posBot, groundCheckRadius);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].isTrigger) continue;
            if (colliders[i].gameObject != gameObject) isGrounded = true;
        }
    }
}
