using UnityEngine;

namespace CashCourses
{
    public class Move : MonoBehaviour
    {
        private Rigidbody2D rb;
        private Animator animator;
        [SerializeField]private float moveSpeed = 5;
        private float xVelocity;
        [SerializeField]private float yValue = 5;
        private bool faceRight = true;

        [SerializeField]private float groundDistance;

        private bool isGround = true;

        [SerializeField] private LayerMask WhatIsGround;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponentInChildren<Animator>();
        }
        // Update is called once per frame
        void Update()
        {
            Movement();
            CheckInput();
            CheckFlip();
            AnimatorController();//设置动画切换
            CollisionCheck();

        }
        
        /// <summary>
        /// check whether the player is on the ground
        /// the isGround is to confine whether the play can jump or not.
        /// </summary>
        private void CollisionCheck()
        {
            isGround = Physics2D.Raycast(transform.position, Vector2.down, groundDistance, WhatIsGround);
        }

        private void AnimatorController()
        {
            animator.SetBool("isMoving",rb.linearVelocityX!=0);
            animator.SetFloat("yVelocity",rb.linearVelocityY);
            animator.SetBool("isGrounded",isGround);
        }

        private void CheckInput()
        {
            xVelocity = Input.GetAxisRaw("Horizontal");
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }

        private void Movement()
        {
            rb.linearVelocity = new Vector2(xVelocity*moveSpeed, rb.linearVelocityY);
        }

        
        private void Jump()
        {
            if (isGround)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocityX, yValue);
            }
        }

        private void Flip()
        {
            faceRight = !faceRight;
            transform.Rotate(Vector3.up,180);
        }

        private void CheckFlip()
        {
            if(rb.linearVelocityX>0 && !faceRight)
                Flip();
            else if(rb.linearVelocityX<0 && faceRight)
                Flip();
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - groundDistance));
        }
    }
}
