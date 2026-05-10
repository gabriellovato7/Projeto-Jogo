using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("Vidas")]
    public int vidasMaximas = 3;
    private int vidasAtuais;

    [Header("UI - Ícones de Vida")]
    public SpriteRenderer[] iconesVida;

    [Header("Invencibilidade após dano")]
    public float tempoInvencivel = 1.5f;
    private bool invencivel = false;

    [Header("Piscar ao tomar dano")]
    public SpriteRenderer spriteRenderer;
    public float tempoPiscar = 0.1f;

    private readonly string[] tagsInimigo = { "Enemy1", "Enemy2", "Enemy3" };
    private readonly string[] tagsMorteInstantanea = { "Espinho" };

    void Start()
    {
        if (PlayerPrefs.GetInt("ModoHardcore", 0) == 1)
            vidasMaximas = 1;

        vidasAtuais = vidasMaximas;
        AtualizarUI();

        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TomarDano(int quantidade = 1)
    {
        if (invencivel) return;

        vidasAtuais -= quantidade;
        vidasAtuais = Mathf.Clamp(vidasAtuais, 0, vidasMaximas);

        AtualizarUI();
        StartCoroutine(RotinaDano());

        if (vidasAtuais <= 0)
            Morrer();
    }

    public void MorrerInstantaneamente()
    {
        vidasAtuais = 0;
        AtualizarUI();
        Morrer();
    }

    public void RestaurarVida(int quantidade = 1)
    {
        if (vidasAtuais >= vidasMaximas) return;

        vidasAtuais += quantidade;
        vidasAtuais = Mathf.Clamp(vidasAtuais, 0, vidasMaximas);
        AtualizarUI();
    }

    void AtualizarUI()
    {
        if (iconesVida == null) return;

        for (int i = 0; i < iconesVida.Length; i++)
        {
            if (iconesVida[i] != null)
                iconesVida[i].enabled = (i < vidasAtuais);
        }
    }

    System.Collections.IEnumerator RotinaDano()
    {
        invencivel = true;

        float tempoPassado = 0f;
        while (tempoPassado < tempoInvencivel)
        {
            if (spriteRenderer != null)
                spriteRenderer.enabled = !spriteRenderer.enabled;

            yield return new WaitForSeconds(tempoPiscar);
            tempoPassado += tempoPiscar;
        }

        if (spriteRenderer != null)
            spriteRenderer.enabled = true;

        invencivel = false;
    }

    [Header("Game Over")]
    public GameObject painelGameOver;

    void Morrer()
    {
        if (painelGameOver != null)
        {
            painelGameOver.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void ProcessarContato(string tagObjeto)
    {
        foreach (string tag in tagsInimigo)
        {
            if (tagObjeto == tag)
            {
                TomarDano(1);
                return;
            }
        }

        foreach (string tag in tagsMorteInstantanea)
        {
            if (tagObjeto == tag)
            {
                MorrerInstantaneamente();
                return;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        ProcessarContato(col.gameObject.tag);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        ProcessarContato(col.gameObject.tag);
    }
}
