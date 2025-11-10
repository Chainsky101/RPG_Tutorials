using Unity.VisualScripting;
using UnityEngine;

namespace CashCourses
{
    public class Skeleton : MonsterBase
    {
        [Header("Move Info")]
        [SerializeField] private float moveSpeed;
        
        protected override void Start()
        {
            base.Start();
        }
        protected override void Update()
        {
            base.Update();
            SwitchDir();
            PlayerDetecting();
        }

        private void PlayerDetecting()
        {
            if (playerRaycastHit2D)
            {
                if (playerRaycastHit2D.distance > monsterAttackDistance)
                {
                    Movement(monsterDashFactor);
                    isAttacking = false;
                    Debug.Log("find you!");
                }
                else
                {
                    isAttacking = true;
                    Debug.Log("Attack you!");
                }
            }
            else
            {
                Movement();
            }
        }

        protected void Movement(float factor = 1f)
        {
            rb.linearVelocity = new Vector2(faceDir*factor* moveSpeed, 0);
        }

        private void SwitchDir()
        {
            if (!isGround || isDetectedWall)
            {
                Flip();
            }
        }
    }
}