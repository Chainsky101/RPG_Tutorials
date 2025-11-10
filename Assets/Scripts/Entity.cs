using UnityEngine;

/// <summary>
/// Base for player and enemies
/// </summary>
public class Entity : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Animator animator;
    protected bool isGround = true;
    [SerializeField] protected LayerMask WhatIsGround;
    [SerializeField] protected float groundDistance;
    [SerializeField] protected Transform ground;
    protected bool faceRight = true;
    protected float faceDir = 1f;
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    protected virtual void Update()
    {
        CollisionCheck();
    }
    
    /// <summary>
    /// check whether the entity is on the ground
    /// </summary>
    protected virtual void CollisionCheck()
    {
        isGround = Physics2D.Raycast(ground.position, Vector2.down, groundDistance, WhatIsGround);
    }
    
    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(ground.position, new Vector3(ground.position.x, ground.position.y - groundDistance));
    }
    
    protected void Flip()
    {
        faceDir *= -1f;
        faceRight = !faceRight;
        transform.Rotate(Vector3.up,180);
    }
}
