using UnityEngine;

public class PlataformaMovel : MonoBehaviour
{
    [Header("Movimento")]
    public float distancia = 3f;
    public float velocidade = 2f;

    public enum Direcao { Vertical, Horizontal }
    public Direcao direcao = Direcao.Vertical;

    private Vector3 posicaoInicial;
    private Rigidbody2D rb;

    void Start()
    {
        posicaoInicial = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float offset = Mathf.Sin(Time.time * velocidade) * distancia;

        Vector3 novaPosicao = direcao == Direcao.Vertical
            ? posicaoInicial + new Vector3(0, offset, 0)
            : posicaoInicial + new Vector3(offset, 0, 0);

        rb.MovePosition(novaPosicao);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
            col.transform.SetParent(transform);
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
            col.transform.SetParent(null);
    }
}
