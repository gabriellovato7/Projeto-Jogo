using UnityEngine;

public class Enemy3Boss : MonoBehaviour
{
    enum Estado { Esperando, Investindo, Recuperando }
    Estado estadoAtual = Estado.Esperando;

    [Header("Limites da Arena (posição X no mundo)")]
    public float limiteEsquerdo = -15f;
    public float limiteDireito  =  15f;

    [Header("Investida")]
    public float velocidadeInvestida = 10f;
    public float tempoEspera = 1.5f;
    public float tempoRecuperacao = 0.8f;

    private SpriteRenderer sr;
    private int direcao = -1;
    private float timer = 0f;
    private bool danoJaCausado = false;
    private float posY;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        timer = tempoEspera;
        posY = transform.position.y;

        float x = Mathf.Clamp(transform.position.x, limiteEsquerdo, limiteDireito);
        transform.position = new Vector3(x, posY, 0f);
    }

    void Update()
    {
        switch (estadoAtual)
        {
            case Estado.Esperando:   Esperar();   break;
            case Estado.Investindo:  Investir();  break;
            case Estado.Recuperando: Recuperar(); break;
        }

        if (sr != null) sr.flipX = (direcao == -1);
    }

    void Esperar()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            danoJaCausado = false;
            estadoAtual = Estado.Investindo;
        }
    }

    void Investir()
    {
        float novoX = transform.position.x + direcao * velocidadeInvestida * Time.deltaTime;
        transform.position = new Vector3(novoX, posY, 0f);

        bool chegouEsquerdo = direcao == -1 && transform.position.x <= limiteEsquerdo;
        bool chegouDireito  = direcao ==  1 && transform.position.x >= limiteDireito;

        if (chegouEsquerdo || chegouDireito)
        {
            float x = Mathf.Clamp(transform.position.x, limiteEsquerdo, limiteDireito);
            transform.position = new Vector3(x, posY, 0f);

            direcao *= -1;
            timer = tempoRecuperacao;
            estadoAtual = Estado.Recuperando;
        }
    }

    void Recuperar()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            danoJaCausado = false;
            timer = tempoEspera;
            estadoAtual = Estado.Esperando;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.gameObject.CompareTag("Player")) return;
        if (danoJaCausado) return;

        PlayerHealth ph = col.gameObject.GetComponent<PlayerHealth>();
        if (ph != null)
        {
            ph.TomarDano(2);
            danoJaCausado = true;
        }
    }

    void OnDrawGizmos()
    {
        float y = transform.position.y;
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(new Vector3(limiteEsquerdo, y - 4f), new Vector3(limiteEsquerdo, y + 4f));
        Gizmos.DrawLine(new Vector3(limiteDireito,  y - 4f), new Vector3(limiteDireito,  y + 4f));

        Gizmos.color = new Color(0f, 1f, 1f, 0.1f);
        Gizmos.DrawCube(
            new Vector3((limiteEsquerdo + limiteDireito) / 2f, y),
            new Vector3(limiteDireito - limiteEsquerdo, 2f, 0f)
        );
    }
}
