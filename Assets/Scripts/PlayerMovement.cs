using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movimento")]
    public float velocidade = 5f;
    public float forcaPulo = 10f;

    private Rigidbody2D rb;
    private Animator anim;
    private bool noChao;
    private float movimentoHorizontal;
    private Vector3 escalaOriginal;
    private int contatosChao = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
        escalaOriginal = transform.localScale;
    }

    void Update()
    {
        movimentoHorizontal = Input.GetAxisRaw("Horizontal");
        noChao = contatosChao > 0;

        if (Input.GetButtonDown("Jump") && noChao)
        {
            rb.velocity = new Vector2(rb.velocity.x, forcaPulo);
        }

        if (movimentoHorizontal > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(escalaOriginal.x), escalaOriginal.y, escalaOriginal.z);
        }
        else if (movimentoHorizontal < 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(escalaOriginal.x), escalaOriginal.y, escalaOriginal.z);
        }

        if (anim != null)
        {
            bool estaCorrendo = movimentoHorizontal != 0;
            anim.SetBool("correndo", estaCorrendo);

            anim.SetBool("pulando", !noChao);
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(movimentoHorizontal * velocidade, rb.velocity.y);
    }

    void OnCollisionEnter2D(Collision2D col) => contatosChao++;
    void OnCollisionExit2D(Collision2D col)
    {
        contatosChao--;
        if (contatosChao < 0) contatosChao = 0;
    }
}