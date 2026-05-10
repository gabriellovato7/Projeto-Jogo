using UnityEngine;

public class Parallax : MonoBehaviour
{
    [Header("Fator de Parallax (0 = move junto com a camera, 1 = fixo)")]
    [Range(0f, 1f)]
    public float fatorParallax = 0.5f;

    private Transform cam;
    private Vector3 ultimaPosicaoCamera;

    void Start()
    {
        cam = Camera.main.transform;
        ultimaPosicaoCamera = cam.position;
    }

    void LateUpdate()
    {
        Vector3 delta = cam.position - ultimaPosicaoCamera;

        transform.position += new Vector3(delta.x * (1f - fatorParallax), 0f, 0f);

        ultimaPosicaoCamera = cam.position;
    }
}
