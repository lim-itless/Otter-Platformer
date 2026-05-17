using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private Transform startPoint;
    [SerializeField] private Rigidbody2D playerRigidbody;

    public void ResetPlayer()
    {
        transform.position = startPoint.position;
        playerRigidbody.linearVelocity = Vector2.zero;
    }
}