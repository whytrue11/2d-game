using System;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Threading;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

	[SerializeField] private float jumpForce = 400f;                          // Amount of force added when the player jumps.
	[SerializeField] private float dashForce = 400f;                          // Amount of force added when the player dashes.
	[SerializeField] private float dashCooldown = 1.5f;
	[SerializeField] private float dashingTime = 0.2f;
	[Range(0, 1)][SerializeField] private float crouchSpeed = .36f;           // Amount of maxSpeed applied to crouching movement. 1 = 100%
	[Range(0, .3f)][SerializeField] private float movementSmoothing = .05f;   // How much to smooth out the movement
	[SerializeField] private bool airControl = false;                         // Whether or not a player can steer while jumping;
	[SerializeField] private bool canDoubleJump = false;
	[SerializeField] private LayerMask whatIsGround;                          // A mask determining what is ground to the character
	[SerializeField] private Transform groundCheck;                           // A position marking where to check if the player is grounded.
	[SerializeField] private Transform ceilingCheck;                          // A position marking where to check for ceilings
	[SerializeField] private Collider2D crouchDisableCollider;   // A collider that will be disabled when crouching
	[SerializeField] private Health playerHealth;
	[SerializeField] private Attack playerAttack;
	[SerializeField] private GameObject deathMenu;
	[SerializeField] private Animator animator;

	const float groundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	private bool grounded;            // Whether or not the player is grounded.
	const float ceilingRadius = .1f; // Radius of the overlap circle to determine if the player can stand up
	private Rigidbody2D rigidBody2D;
	private bool facingRight = true;  // For determining which way the player is currently facing.
	private Vector3 velocity = Vector3.zero;
	private bool canDash = true;
	private int jumpCounter = 1;


	private PowerUp buff;
	private ChangeWeapon weapon;
	private bool nextToTheBuff = false;
	private bool nextToTheWeapon = false;
	private bool invulnerable = false;

	private GameManager gameManager;
	private bool isDead;
    
	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	public BoolEvent OnCrouchEvent;
	private bool wasCrouching = false;

	private void Awake()
	{
		gameManager = GameObject.FindGameObjectWithTag("Utils").GetComponent<GameManager>();
		rigidBody2D = GetComponent<Rigidbody2D>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();

		if (OnCrouchEvent == null)
			OnCrouchEvent = new BoolEvent();
	}
	
	private void Start()
	{
		InitPlayerParams();
	}

	private void InitPlayerParams()
	{
		playerHealth.SetHealth(DataHolder.playerCurrentHealth);
		playerHealth.SetMaxHealth(DataHolder.playerMaxHealth);
		playerAttack.SetWeaponAnimation(DataHolder.playerWeaponAnimation);
		playerAttack.SetDamage(DataHolder.playerDamage);
		canDoubleJump = DataHolder.playerDoubleJumpBuff;
	}

	private void FixedUpdate()
	{
		bool wasgrounded = grounded;
		grounded = false;

		// The player is grounded if a circlecast to the groundCheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, whatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				grounded = true;
				if (!wasgrounded)
                {
					animator.SetBool("Jump", false);
					OnLandEvent.Invoke();
					jumpCounter = 2;
				}	
			}
		}
	}


	public void Move(float move, bool crouch, bool jump, bool dash)
	{
		// If crouching, check to see if the character can stand up
		if (!crouch && wasCrouching)
		{
			// If the character has a ceiling preventing them from standing up, keep them crouching
			if (Physics2D.OverlapCircle(ceilingCheck.position, ceilingRadius, whatIsGround))
			{
				crouch = true;
			}
		}

		//only control the player if grounded or airControl is turned on
		if (grounded || airControl)
		{

			// If crouching
			if (crouch)
			{
				if (!wasCrouching)
				{
					wasCrouching = true;
					OnCrouchEvent.Invoke(true);
					animator.SetBool("Crouch", true);
				}

				// Reduce the speed by the crouchSpeed multiplier
				move *= crouchSpeed;

				// Disable one of the colliders when crouching
				if (crouchDisableCollider != null)
					crouchDisableCollider.enabled = false;
			}
			else
			{
				// Enable the collider when not crouching
				if (crouchDisableCollider != null)
					crouchDisableCollider.enabled = true;

				if (wasCrouching)
				{
					wasCrouching = false;
					OnCrouchEvent.Invoke(false);
					animator.SetBool("Crouch", false);
				}
			}

			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * 10f, rigidBody2D.velocity.y);
			// And then smoothing it out and applying it to the character
			rigidBody2D.velocity = Vector3.SmoothDamp(rigidBody2D.velocity, targetVelocity, ref velocity, movementSmoothing);

			// If the input is moving the player right and the player is facing left...
			if (move > 0 && !facingRight)
			{
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && facingRight)
			{
				// ... flip the player.
				Flip();
			}
		}
		// If the player should jump...
		if(!canDoubleJump)
        {
			if (!crouch && grounded && jump)
			{
				// Add a vertical force to the player.
				//grounded = false;
				animator.SetBool("Jump", true);
				rigidBody2D.velocity =  Vector2.up * jumpForce;
			}
		}
        else
        {
			if (!crouch && grounded && jump)
			{
				// Add a vertical force to the player.
				//grounded = false;
				//jumpCounter--;
				animator.SetBool("Jump", true);
				rigidBody2D.velocity = Vector2.up * jumpForce;
			}

			if(!crouch && !grounded && jump)
            {
				jumpCounter--;
			}
		
			if (!crouch && !grounded && jump && jumpCounter == 1)
			{
				//grounded = false;
				jumpCounter--;
				animator.SetBool("Jump", true);
				rigidBody2D.velocity = Vector2.up * jumpForce;
			}
			
		}
		
		if (dash)
		{
			if(canDash)
            {
				StartCoroutine(Dash());
			}
		}
	}

	public void AttackEnemy()
	{
		if (playerAttack.GetCanAttack())
		{
			StartCoroutine(playerAttack.AttackEnemy());
		}
	}

	public void PickUpItem()
	{
		if (nextToTheBuff)
		{
			StartCoroutine(buff.Apply());
		}

		if (nextToTheWeapon)
		{
			StartCoroutine(weapon.Apply());
		}
	}

	public void NextToTheBuff(PowerUp buff)
	{
		nextToTheBuff = true;
		this.buff = buff;
	}
	public void NotNextToTheBuff()
	{
		nextToTheBuff = false;
		this.buff = null;
	}

	public void NextToTheWeapon(ChangeWeapon weapon)
	{
		nextToTheWeapon = true;
		this.weapon = weapon;
	}
	public void NotNextToTheWeapon()
	{
		nextToTheWeapon = false;
		this.weapon = null;
	}

	public void setDoubleJump(bool doubleJump)
	{
		canDoubleJump = doubleJump;
	}


	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	private IEnumerator Dash()
	{
		canDash = false;
		animator.SetTrigger("Dash");
		invulnerable = true;
		float originalGravity = rigidBody2D.gravityScale;
		rigidBody2D.gravityScale = 0f;
		if (facingRight)
		{
			rigidBody2D.velocity = new Vector2(dashForce, 0f);
		}
		else
		{
			rigidBody2D.velocity = new Vector2(-dashForce, 0f);
		}
		yield return new WaitForSeconds(dashingTime);
		invulnerable = false;
		rigidBody2D.gravityScale = originalGravity;
		yield return new WaitForSeconds(dashCooldown);
		canDash = true;
	}
	
	
	public void PlayerDmg(int dmg)
	{
		if (!invulnerable)
		{
			playerHealth.DmgUnit(dmg);
		}
		if (playerHealth.GetHealth() <= 0 && !isDead)
		{
			isDead = true;
			Die();
		}
	}
	
	private void Die()
	{
		deathMenu.SetActive(true);
		gameManager.pause = true;
		gameManager.End(true);

		StartCoroutine("FadeDeathScreen");
	}

	private IEnumerator FadeDeathScreen()
	{
		Image image = deathMenu.GetComponent<Image>();
		Color color = image.color;
		color.a = 0;
		image.color = color;

		float fadeTimeSeconds = 1.5f;
		float time = 1f / fadeTimeSeconds;
		float progress = 0;
		
		while (progress < 1)
		{
			progress += time * Time.deltaTime;
			color.a = Mathf.Lerp(0, 1, progress);
			image.color = color;
			yield return null;
		}
	}
}
