using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Configurações de Dano")]
    public float danoAtaque = 1f;
    public float alcanceAtaque = 1.2f;
    public float cooldownAtaque = 0.4f;
    private float proximoAtaque = 0f;

    [Header("Hitbox de Ataque")]
    public Vector2 offsetAtaque = new Vector2(0.6f, 0f);

    [Header("Camada dos Inimigos")]
    public LayerMask camadaInimigos;

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        bool atacarInput = Input.GetMouseButtonDown(0)
                   || Input.GetKeyDown(KeyCode.K)
                   || Input.GetKeyDown(KeyCode.J);

        if (atacarInput && Time.time >= proximoAtaque)
        {
            proximoAtaque = Time.time + cooldownAtaque;
            
            if (anim != null)
            {
                anim.SetTrigger("atacar");
            }
        }
    }

    public void ExecutarDano()
    {
        float direcaoX = transform.localScale.x >= 0 ? 1f : -1f;
        Vector2 centroAtaque = (Vector2)transform.position + new Vector2(offsetAtaque.x * direcaoX, offsetAtaque.y);

        Collider2D[] inimigosAtingidos = Physics2D.OverlapCircleAll(centroAtaque, alcanceAtaque, camadaInimigos);

        foreach (Collider2D inimigo in inimigosAtingidos)
        {
            EnemyHealth hp = inimigo.GetComponent<EnemyHealth>();
            if (hp != null)
            {
                hp.TomarDano(danoAtaque);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        float direcaoX = transform.localScale.x >= 0 ? 1f : -1f;
        Vector2 centroAtaque = (Vector2)transform.position + new Vector2(offsetAtaque.x * direcaoX, offsetAtaque.y);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(centroAtaque, alcanceAtaque);
    }
}