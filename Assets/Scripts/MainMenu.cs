using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Botão Hardcore (desabilitado até zerar o jogo)")]
    public GameObject botaoHardcore;

    void Start()
    {
        bool zerado = PlayerPrefs.GetInt("JogoZerado", 0) == 1;

        if (botaoHardcore != null)
            botaoHardcore.SetActive(zerado);
    }

    public void IniciarJogo()
    {
        PlayerPrefs.SetInt("ModoHardcore", 0);
        SceneManager.LoadScene("Cutscene1");
    }

    public void IniciarHardcore()
    {
        PlayerPrefs.SetInt("ModoHardcore", 1);
        SceneManager.LoadScene("Cutscene1");
    }

    public void Sair()
    {
        Application.Quit();
    }
}