using UnityEngine;

public class BossDeathReward : MonoBehaviour
{
    [Header("Boss a monitorar")]
    public GameObject boss;

    [Header("Item que aparece ao boss morrer")]
    public GameObject recompensa;

    void Update()
    {
        if (boss == null && recompensa != null)
        {
            recompensa.SetActive(true);
            Destroy(this);
        }
    }
}
