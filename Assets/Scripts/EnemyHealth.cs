using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Vida")]
    public float vidaMaxima = 2f;
    private float vidaAtual;

    [Header("Efeito ao tomar dano (opcional)")]
    public SpriteRenderer sr;
    public Color corDano = Color.red;
    private Color corOriginal;

    void Start()
    {
        vidaAtual = vidaMaxima;

        if (sr == null)
            sr = GetComponent<SpriteRenderer>();

        if (sr != null)
            corOriginal = sr.color;
    }

    public void TomarDano(float quantidade)
    {
        vidaAtual -= quantidade;

        if (sr != null)
            StartCoroutine(PiscarDano());

        if (vidaAtual <= 0)
            Morrer();
    }

    System.Collections.IEnumerator PiscarDano()
    {
        if (sr != null) sr.color = corDano;
        yield return new WaitForSeconds(0.15f);
        if (sr != null) sr.color = corOriginal;
    }

    void Morrer()
    {
        Destroy(gameObject);
    }
}
