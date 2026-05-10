using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    enum Estado { Patrulha, Perseguindo }
    Estado estadoAtual = Estado.Patrulha;

    [Header("Patrulha")]
    public float velocidadePatrulha = 2f;
    public float distanciaPatrulha = 3f;

    [Header("Perseguição")]
    public float velocidadePerseguicao = 5f;
    public float rangeDeteccao = 6f;
    public float rangePerdaVisao = 9f;

    [Header("Detecção de Borda")]
    public bool evitarBordas = true;
    public float distanciaCheckBorda = 0.5f;
    public float alturaCheckBorda = 0.2f;
    public LayerMask camadaChao;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Transform jogador;
    private Vector2 posicaoInicial;
    private int direcao = 1;
    private float cooldownInversao = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        posicaoInicial = transform.position;

        GameObject go = GameObject.FindGameObjectWithTag("Player");
        if (go != null) jogador = go.transform;

        if (camadaChao == 0)
            camadaChao = ~(1 << gameObject.layer);
    }

    void FixedUpdate()
    {
        if (jogador == null) return;

        float dist = Vector2.Distance(transform.position, jogador.position);

        if (estadoAtual == Estado.Patrulha)
        {
            if (dist <= rangeDeteccao)
                estadoAtual = Estado.Perseguindo;
            else
                Patrulhar();
        }
        else
        {
            if (dist > rangePerdaVisao)
                estadoAtual = Estado.Patrulha;
            else
                Perseguir();
        }

        AtualizarSprite();
    }

    void Patrulhar()
    {
        cooldownInversao -= Time.fixedDeltaTime;

        bool deveInverter = false;

        float deslocamento = transform.position.x - posicaoInicial.x;
        if ((direcao == 1 && deslocamento >= distanciaPatrulha) ||
            (direcao == -1 && deslocamento <= -distanciaPatrulha))
            deveInverter = true;

        if (evitarBordas && cooldownInversao <= 0f)
        {
            Vector2 origem = (Vector2)transform.position + new Vector2(direcao * distanciaCheckBorda, -alturaCheckBorda);
            bool temChao = Physics2D.Raycast(origem, Vector2.down, 0.6f, camadaChao);
            if (!temChao)
                deveInverter = true;
        }

        if (deveInverter && cooldownInversao <= 0f)
        {
            direcao *= -1;
            cooldownInversao = 0.5f;
        }

        rb.velocity = new Vector2(direcao * velocidadePatrulha, rb.velocity.y);
    }

    void Perseguir()
    {
        direcao = jogador.position.x > transform.position.x ? 1 : -1;
        rb.velocity = new Vector2(direcao * velocidadePerseguicao, rb.velocity.y);
    }

    void AtualizarSprite()
    {
        if (sr != null)
            sr.flipX = (direcao == -1);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, rangeDeteccao);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangePerdaVisao);
    }
}
