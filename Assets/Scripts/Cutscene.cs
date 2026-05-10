using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class Cutscene : MonoBehaviour
{
    [Header("Cena de Destino")]
    public string cenaDestino = "InitialScene";

    private VideoPlayer vp;

    void Start()
    {
        vp = GetComponent<VideoPlayer>();

        if (vp != null)
        {
            vp.loopPointReached += EndReached;
        }
    }

    void EndReached(VideoPlayer source)
    {
        CarregarJogo();
    }

    void CarregarJogo()
    {
        SceneManager.LoadScene(cenaDestino);
    }
}