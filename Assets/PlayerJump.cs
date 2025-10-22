using UnityEngine;
public class PlayerJump : MonoBehaviour
{
    private Collider2D playerCollider;
    private float ignoreTime = 0.2f; // Tiempo que tarda en reactivar la colisión
    private bool isDropping = false;
    void Start()
    {
        playerCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        // Si el jugador presiona la tecla de abajo y salto, atraviesa la plataforma
        if (Input.GetKey(KeyCode.S) && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(DisableCollision());
        }
    }

    private System.Collections.IEnumerator DisableCollision()
    {
        isDropping = true;
        PlatformEffector2D[] effectors = FindObjectsByType<PlatformEffector2D>(FindObjectsSortMode.None);

        foreach (var effector in effectors)
        {
            Collider2D platformCollider = effector.GetComponent<Collider2D>();
            Physics2D.IgnoreCollision(playerCollider, platformCollider, true);
        }

        yield return new WaitForSeconds(ignoreTime);

        foreach (var effector in effectors)
        {
            Collider2D platformCollider = effector.GetComponent<Collider2D>();
            Physics2D.IgnoreCollision(playerCollider, platformCollider, false);
        }

        isDropping = false;
    }
}
