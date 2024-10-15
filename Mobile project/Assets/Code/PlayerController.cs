using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
   public InputActionAsset inputActions;
   private InputAction moveAction;
   private InputAction jumpAction;
   private InputAction dashAction;

   private Vector2 moveInput;
   private bool isJumping;
   private bool isDashing;
   private bool isGrounded;

   public Transform groundCheck;
   public LayerMask groundLayer; 
   private Rigidbody2D rb;

   public float moveSpeed = 5f; // Walking speed
   public float jumpForce = 10f; // Jumping force
   public float dashSpeed = 20f; // Speed during dash
   public float dashDuration = 0.2f; // How long the dash lasts
   private float dashTime; // Time remaining for dash

   void Awake()
   {
      // Reference the Rigidbody2D component
      rb = GetComponent<Rigidbody2D>();

      // Set up input actions
      var gameplayActions = inputActions.FindActionMap("Player");
      moveAction = gameplayActions.FindAction("Move");
      jumpAction = gameplayActions.FindAction("Jump");
      dashAction = gameplayActions.FindAction("Dash");
   }

   void OnEnable()
   {
      moveAction.Enable();
      jumpAction.Enable();
      dashAction.Enable();

      moveAction.performed += OnMove;
      moveAction.canceled += OnMove;
      jumpAction.performed += OnJump;
      dashAction.performed += OnDash;
   }

   void OnDisable()
   {
      moveAction.Disable();
      jumpAction.Disable();
      dashAction.Disable();

      moveAction.performed -= OnMove;
      moveAction.canceled -= OnMove;
      jumpAction.performed -= OnJump;
      dashAction.performed -= OnDash;
   }

   void OnMove(InputAction.CallbackContext context)
   {
      moveInput = context.ReadValue<Vector2>();
   }

   void OnJump(InputAction.CallbackContext context)
   {
      // Only allow jumping if the player is grounded
      if (isGrounded)
      {
         isJumping = context.ReadValueAsButton();
         rb.velocity = new Vector2(rb.velocity.x, jumpForce);  // Apply jump force
      }
   }

   void OnDash(InputAction.CallbackContext context)
   {
      if (!isDashing) // Dash only if not already dashing
      {
         isDashing = true;
         dashTime = dashDuration; // Start dash with full duration
      }
   }

   void Update()
   {
      // Handle horizontal movement
      Vector3 move = new Vector3(moveInput.x * moveSpeed, 0, 0) * Time.deltaTime;
      transform.Translate(move);

      // Check if the player is grounded
      isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

      // Reset jump state if the player is grounded again
      if (isGrounded)
      {
         isJumping = false;
      }

      // Handle dash
      if (isDashing)
      {
         if (dashTime > 0)
         {
            float dashDirection = Mathf.Sign(moveInput.x); // Dash in the direction the player is moving
            rb.velocity = new Vector2(dashDirection * dashSpeed, rb.velocity.y); // Apply dash speed
            dashTime -= Time.deltaTime; // Decrease dash time
         }
         else
         {
            isDashing = false; // End dash
         }
      }
   }
}
