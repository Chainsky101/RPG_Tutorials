using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace CashCourses
{
    public class Player : Entity
    {
        [Header("Move Info")]
        [SerializeField]private float moveSpeed = 5;
        private float xVelocity;
        [SerializeField]private float yValue = 5;

        [Header("Dash Info")] [SerializeField] private float dashDuration;
        private float dashTimer;
        [SerializeField] private float dashSpeed;
        [SerializeField] private float dashCooldown;
        [SerializeField]private float dashCooldownTimer;

        [Header("Attack Info")]
        [SerializeField] private bool isAttacking;
        [SerializeField] private float comboDuration;
        [SerializeField] private float comboTimer;
        [SerializeField] private int attackCount = 0;
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        protected override void Start()
        {
            base.Start();
        }
        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
            Movement();
            CheckInput();
            CheckFlip();
            AnimatorController();//设置动画切换
            CollisionCheck();
        }
        
        


        private void AnimatorController()
        {
            animator.SetBool("isMoving",rb.linearVelocityX!=0);
            animator.SetFloat("yVelocity",rb.linearVelocityY);
            animator.SetBool("isGrounded",isGround);
            animator.SetBool("isDashing",dashTimer>0);
            animator.SetBool("isAttacking",isAttacking);
            animator.SetInteger("attackCount",attackCount);
        }
        private void CheckInput()
        {
            dashTimer -= Time.deltaTime;
            dashCooldownTimer -= Time.deltaTime;
            comboTimer -= Time.deltaTime;
            xVelocity = Input.GetAxisRaw("Horizontal");
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
            if (Input.GetKeyDown(KeyCode.LeftShift))
                DashAccessibility();
            if (Input.GetMouseButtonDown(0))
            {
                AttackAccessibility();
            }
        }
        private void AttackAccessibility()
        {
            if (!isGround || isAttacking)
            {
                return;
            }
            if (comboTimer > 0)
            {
                attackCount = (attackCount + 1) % 3;
            }
            else
                attackCount = 0;
            isAttacking = true;
            comboTimer = comboDuration;
        }
        private void DashAccessibility()
        {
            if (dashCooldownTimer < 0)
            {
                dashTimer = dashDuration;
                dashCooldownTimer = dashCooldown;
            }
        }
        private void Movement()
        {
            if (isAttacking)
            {
                rb.linearVelocity = Vector2.zero;
            }else if (dashTimer > 0)
                rb.linearVelocity = new Vector2(faceDir * dashSpeed, 0);
            else
                rb.linearVelocity = new Vector2(xVelocity*moveSpeed, rb.linearVelocityY);
        }
        public void SetAttackingOver()
        {
            isAttacking = false;
        }
        private void Jump()
        {
            if (isGround&& !isAttacking)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocityX, yValue);
            }
        }
        private void CheckFlip()
        {
            if(rb.linearVelocityX>0 && !faceRight)
                Flip();
            else if(rb.linearVelocityX<0 && faceRight)
                Flip();
        }
    }
}
