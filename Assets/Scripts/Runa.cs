using UnityEngine;

public class Runa : MonoBehaviour
{
    [Header("Identificação Única da Runa")]
    [Tooltip("Deve ser único no jogo inteiro. Ex: fase1_runa1, fase2_runa3")]
    public string runaId = "fase1_runa1";

    private void Start()
    {
        if (RunaManager.Instance != null && RunaManager.Instance.RunaJaColetada(runaId))
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player")) return;

        if (RunaManager.Instance != null)
            RunaManager.Instance.ColetarRuna(runaId);

        Destroy(gameObject);
    }
}
