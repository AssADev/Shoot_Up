using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    // Variables :
    public float speedMovement;
    public float jumpForce;
    public ShootBullet bulletsAmountIndicator;

    // Dust Animation on jump :
    public GameObject playerDustJump;
    public GameObject playerDustAfterJump;

    // Check isGrounded :
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;

    // Jump :
    public float jumpTime;
    private float jumpTimeCounter;

    private bool isJumping;
    private bool isGrounded;
    private float moveInput;

    private static Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    void Start() {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    public void MoveReleased()
    {
        _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
    }

    public void Move(float value)
    {
        // FlipX :
        if (value < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        } else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        // Move
        _rigidbody.velocity = new Vector2(value * speedMovement, _rigidbody.velocity.y);
    }

    public void Jump()
    {
        if (isGrounded)
        {
            DustJump();
            isJumping = true;
            jumpTimeCounter = jumpTime;
            _rigidbody.velocity = Vector2.up * jumpForce;
        } else if (isJumping == true)
        {
            _animator.SetBool("IsJumping", true);

            if (jumpTimeCounter > 0)
            {
                _rigidbody.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }
    }

    public void JumpReleased()
    {
        isJumping = false;
    }

    void Update() {
        // isGrounded :
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

        // Reload :
        if (isGrounded && (_rigidbody.velocity.y == 0) && (bulletsAmountIndicator.bulletNumber < 8)) {
            bulletsAmountIndicator.bulletNumber = 8;
            bulletsAmountIndicator.UpdateBulletsIndicator();
            bulletsAmountIndicator.UpdateBulletsHUD(bulletsAmountIndicator.bulletNumber - 1);
        }

        // Jump :
        if (isGrounded && Input.GetKeyDown(KeyCode.UpArrow)) {
            DustJump();
            isJumping = true;
            jumpTimeCounter = jumpTime;
            _rigidbody.velocity = Vector2.up * jumpForce;
        }

        if (Input.GetKey(KeyCode.UpArrow) && isJumping == true) {
            _animator.SetBool("IsJumping", true);

            if (jumpTimeCounter > 0) {
                _rigidbody.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            } else {
                isJumping = false;
            }
        }

        // Player animations :
        // Running Animation :
        if (moveInput == 0) {
            _animator.SetBool("IsRunning", false);
        } else {
            _animator.SetBool("IsRunning", true);
        }

        // Jump Animation :
        _animator.SetFloat("PlayerVelocityVertical", _rigidbody.velocity.y);

        if (isGrounded) {
            _animator.SetBool("IsJumping", false);
        }
    }

    // Dust Animation before and after jump :
    public void DustJump() {
        GameObject dustJumpEffect = Instantiate(playerDustJump, transform.position, Quaternion.identity);
        Destroy(dustJumpEffect, 0.3f);
    }

    public void DustAfterJump() {
        GameObject dustJumpEffect = Instantiate(playerDustAfterJump, transform.position, Quaternion.identity);
        Destroy(dustJumpEffect, 0.3f);

        CameraShakeController.instance.StartShake(0.1f, 0.1f);
    }
}
