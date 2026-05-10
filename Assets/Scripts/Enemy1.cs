using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    [Header("Patrulha")]
    public float velocidade = 2f;
    public float distanciaPatrulha = 3f;

    private Vector2 posicaoInicial;
    private int direcao = 1;
    private SpriteRenderer sr;

    void Start()
    {
        posicaoInicial = transform.position;
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        transform.Translate(Vector2.right * direcao * velocidade * Time.deltaTime, Space.World);

        float deslocamento = transform.position.x - posicaoInicial.x;

        if (deslocamento >= distanciaPatrulha)
            direcao = -1;
        else if (deslocamento <= -distanciaPatrulha)
            direcao = 1;

        if (sr != null)
            sr.flipX = (direcao == 1);
    }
}
