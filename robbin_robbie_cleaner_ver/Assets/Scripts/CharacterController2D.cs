using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
	public static CharacterController2D instance;
    [SerializeField] public float normal_JumpForce = 150f;                          // Amount of force added when the player jumps.

    [SerializeField] public float transformed_JumpForce = 300f;							// Amount of force added when the player jumps.

	[SerializeField] private int startingHidingPower = 300;
	[SerializeField] private int regen_counter = 0;

    public Vector2 groundCheckBoxSize;


	Sprite HidingSprite;

	Sprite OriginalSprite;
	[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;			// Amount of maxSpeed applied to crouching movement. 1 = 100%
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
	[SerializeField] private bool m_AirControl = true;							// Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;							// A mask determining what is ground to the character

	[SerializeField] private LayerMask m_WhatIsFinishLevel;

	[SerializeField] private LayerMask m_WhatIsItem;

	[SerializeField] private LayerMask m_WhatIsTrap;

	[SerializeField] private Transform m_DonutCheck;

	[SerializeField] private LayerMask m_WhatIsEnemy;
	[SerializeField] private Transform m_GroundCheck;							// A position marking where to check if the player is grounded.
	[SerializeField] private Transform m_CeilingCheck;							// A position marking where to check for ceilings
	[SerializeField] private Collider2D m_CrouchDisableCollider;				// A collider that will be disabled when crouching

	const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	private bool m_Grounded;            // Whether or not the player is grounded.
	const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 m_Velocity = Vector3.zero;

	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	public BoolEvent OnCrouchEvent;
	private bool m_wasCrouching = false;

	public int currentHidingPower;


	private void start() {
		//currentHidingPower = startingHidingPower;
	}

	private void Awake()
	{

		m_Rigidbody2D = GetComponent<Rigidbody2D>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();

		if (OnCrouchEvent == null)
			OnCrouchEvent = new BoolEvent();

		currentHidingPower = startingHidingPower;
	}

	public void resetCollisionInfo() {
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Interactable"), false);
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy2"), false);
	}

	private void FixedUpdate()
	{
		bool wasGrounded = m_Grounded;
		m_Grounded = false;
		if (GameController.instance.levelFinish) {
			Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Interactable"), true);
		}
		else {
			Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Interactable"), false);
		}

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders              = Physics2D.OverlapBoxAll(m_GroundCheck.position + new Vector3(0f, -1f, 0f), groundCheckBoxSize, m_WhatIsGround);
		Collider2D[] collidersItem          = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsItem);
		Collider2D[] collidersTrap          = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsTrap);
		Collider2D[] collides_with_donut    = Physics2D.OverlapCircleAll(m_DonutCheck.position, k_GroundedRadius, m_WhatIsFinishLevel);
        // Debug.Log("groundcheck position");
        // Debug.Log(m_GroundCheck.position);

		for (int i = 0; i < colliders.Length; i++)
		{
            colliders[i].sharedMaterial = new PhysicsMaterial2D();
			if (colliders[i].gameObject != gameObject)
			{
				m_Grounded = true;
				if (!wasGrounded)
					OnLandEvent.Invoke();
			}
            if (!m_Grounded)
            {
                colliders[i].sharedMaterial.friction = 0f;
                colliders[i].sharedMaterial = colliders[i].sharedMaterial;
            }

        }

		// for (int i = 0; i < collidersItem.Length; i++) {
		// 	Debug.Log("SHIIIIT!!!");
		// 	if (collidersItem[i].ToString().Contains("fire")) {
		// 		Debug.Log("FIRE!!!");
		// 		collidersItem[i].gameObject.SetActive(false);
		// 		GameController.instance.obtainCoin();
		// 	}
		// }

		// for (int i = 0; i < collidersTrap.Length; i++) {
		// 	GameController.instance.RobbieDied();
		// }
            
        for (int i = 0; i < collides_with_donut.Length; i++) {
			Collider2D d_col = collides_with_donut[i];
			//Debug.Log(d_col.gameObject.layer.ToString());
			if (d_col.gameObject.name == "Robbie") {
				Debug.Log(gameObject.ToString());
				Debug.Log(d_col.gameObject.ToString());
				GameController.instance.PickedDonut();
			}
		}

		// regenerate
		//regen();
        if (gameObject.GetComponent<RobbieMovement>().currentTransformation != RobbieMovement.Transformations.Normal) {
            transformationUpdate(1);
        }

        if (gameObject.GetComponent<RobbieMovement>().currentTransformation == RobbieMovement.Transformations.Bush)
        {
            // gameObject.GetComponent<SpriteRenderer>().sprite = HidingSprite;
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);
		}
		else {
			Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);
		}

        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemy"), LayerMask.NameToLayer("Enemy"));
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemy2"), LayerMask.NameToLayer("Enemy2"));
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemy"), LayerMask.NameToLayer("Enemy2"));
    }


	public void Move(float move, bool crouch, bool jump)
	{
		// If crouching, check to see if the character can stand up
		if (!crouch)
		{
			// If the character has a ceiling preventing them from standing up, keep them crouching
			if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
			{
				crouch = true;
			}
		}

		//only control the player if grounded or airControl is turned on
		if (m_Grounded || m_AirControl)
		{

			// If crouching
			if (crouch)
			{
				if (!m_wasCrouching)
				{
					m_wasCrouching = true;
					OnCrouchEvent.Invoke(true);
				}

				// Reduce the speed by the crouchSpeed multiplier
				move *= m_CrouchSpeed;

				// Disable one of the colliders when crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = false;
			} else
			{
				// Enable the collider when not crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = true;

				if (m_wasCrouching)
				{
					m_wasCrouching = false;
					OnCrouchEvent.Invoke(false);
				}
			}

			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
			// And then smoothing it out and applying it to the character
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

			// If the input is moving the player right and the player is facing left...
			if (move > 0 && !m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
		}

		// If the player should jump...
		if (m_Grounded && jump)
		{
			// Add a vertical force to the player.
			m_Grounded = false;
            if (gameObject.GetComponent<RobbieMovement>().currentTransformation == RobbieMovement.Transformations.Rabbit)
            {
                transformationUpdate(10);
                m_Rigidbody2D.AddForce(new Vector2(0f, transformed_JumpForce));
            }

            else
            {
                m_Rigidbody2D.AddForce(new Vector2(0f, normal_JumpForce));
            }
            Debug.Log(transformed_JumpForce);
		}
	}


	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	private void transformationUpdate(int fr) {
		// logging check
		float powerRatio = ((float) currentHidingPower) / getMaxHidingEnergy();
		currentHidingPower = Mathf.Max(0, currentHidingPower - fr);
		if (powerRatio >= 0.5 && powerRatio - fr < 0.5) {
			//LoggingManager.instance.RecordEvent(4, "Stamina at 50%");
		}
		//Debug.Log(currentHidingPower);
	}
	private void regen() {
		regen_counter += 1;
		if (regen_counter % 24 == 0) {
			currentHidingPower = Mathf.Min(startingHidingPower, currentHidingPower + 1);
		}
		regen_counter %= 24;
	}
	public int getMaxHidingEnergy() {
        return this.startingHidingPower;
    }
}
