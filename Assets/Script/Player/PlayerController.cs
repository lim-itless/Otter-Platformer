using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerView playerView;
    [SerializeField] private Rigidbody2D rigidbody;

    [SerializeField] private float moveSpeed = 5f;

    private void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");

        bool isMove = moveX != 0;

        playerView.SetMove(isMove);
        playerView.Flip(moveX);

        rigidbody.linearVelocity = new Vector2(moveX * moveSpeed, rigidbody.linearVelocity.y);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerView.SetAttack(true);
            Invoke(nameof(EndAttack), 0.5f);
        }
    }

    private void EndAttack()
    {
        playerView.SetAttack(false);
    }
}