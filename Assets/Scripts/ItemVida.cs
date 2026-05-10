using UnityEngine;

public class ItemVida : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player")) return;

        PlayerHealth ph = col.GetComponent<PlayerHealth>();

        if (ph != null)
        {
            ph.RestaurarVida(1);
            Destroy(gameObject);
        }
    }
}
