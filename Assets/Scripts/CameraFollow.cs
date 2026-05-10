using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Alvo")]
    public Transform alvo;

    [Header("Offset")]
    public Vector3 offset = new Vector3(0f, 1f, -10f);

    [Header("Suavidade")]
    public float suavidade = 5f;

    void LateUpdate()
    {
        if (alvo == null) return;

        Vector3 posicaoAlvo = alvo.position + offset;
        transform.position = Vector3.Lerp(transform.position, posicaoAlvo, suavidade * Time.deltaTime);
    }
}
