using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// used for Monster to specify some unique attributes or methods
/// </summary>
public class MonsterBase : Entity
{
    [Header("Detected Wall")]
    [SerializeField] protected float wallDistance;
    [SerializeField] protected Transform wall;
    protected bool isDetectedWall;

    [FormerlySerializedAs("playerDetectedDistance")]
    [FormerlySerializedAs("playerDistance")]
    [Header("Detected Player")] 
    [SerializeField] protected float monsterDetectedDistance;
    [SerializeField] protected float monsterAttackDistance;
    [SerializeField]protected LayerMask WhatIsPlayer;
    protected float monsterDashFactor = 1.2f;//used to speed up when find the player
    protected RaycastHit2D playerRaycastHit2D;
    protected bool isAttacking;
    //Monster all need to check the ground and wall
    protected override void CollisionCheck()
    {
        base.CollisionCheck();
        isDetectedWall = Physics2D.Raycast(wall.position, Vector2.right * faceDir, wallDistance, WhatIsGround);
        playerRaycastHit2D =
            Physics2D.Raycast(transform.position, Vector2.right * faceDir, monsterDetectedDistance, WhatIsPlayer);
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawLine(wall.position,new Vector3(wall.position.x+wallDistance*faceDir,wall.position.y));
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position,new Vector3(transform.position.x + faceDir*monsterDetectedDistance,transform.position.y));
    }
}
