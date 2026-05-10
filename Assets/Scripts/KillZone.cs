using UnityEngine;

public class KillZone : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player")) return;

        PlayerHealth ph = col.GetComponent<PlayerHealth>();
        if (ph != null)
            ph.MorrerInstantaneamente();
    }
}
