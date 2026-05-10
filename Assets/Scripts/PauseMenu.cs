using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("Painel de Pausa")]
    public GameObject painelPausa;

    private bool pausado = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pausado) Retomar();
            else Pausar();
        }
    }

    void Pausar()
    {
        pausado = true;
        Time.timeScale = 0f;
        if (painelPausa != null) painelPausa.SetActive(true);
    }

    public void Retomar()
    {
        pausado = false;
        Time.timeScale = 1f;
        if (painelPausa != null) painelPausa.SetActive(false);
    }

    public void VoltarMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("InitialScene");
    }
}
