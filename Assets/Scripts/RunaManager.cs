using UnityEngine;

public class RunaManager : MonoBehaviour
{
    public static RunaManager Instance { get; private set; }

    [Header("Total de Runas no Jogo")]
    public int totalRunasNoJogo = 9;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void ColetarRuna(string runaId)
    {
        if (RunaJaColetada(runaId)) return;

        PlayerPrefs.SetInt("Runa_" + runaId, 1);
        PlayerPrefs.Save();

        int total = TotalRunasColetadas();

        if (total >= totalRunasNoJogo)
            GaleriaDesbloqueada();
    }

    public bool RunaJaColetada(string runaId)
    {
        return PlayerPrefs.GetInt("Runa_" + runaId, 0) == 1;
    }

    public int TotalRunasColetadas()
    {
        int count = 0;
        foreach (string id in RunaIdsConhecidas())
        {
            if (PlayerPrefs.GetInt("Runa_" + id, 0) == 1)
                count++;
        }
        return count;
    }

    public bool GaleriaLiberada()
    {
        return TotalRunasColetadas() >= totalRunasNoJogo;
    }

    private void GaleriaDesbloqueada()
    {
        PlayerPrefs.SetInt("GaleriaDesbloqueada", 1);
        PlayerPrefs.Save();
    }

    public void ResetarTudo()
    {
        foreach (string id in RunaIdsConhecidas())
            PlayerPrefs.DeleteKey("Runa_" + id);

        PlayerPrefs.DeleteKey("GaleriaDesbloqueada");
        PlayerPrefs.Save();
    }

    private string[] RunaIdsConhecidas()
    {
        return new string[]
        {
            "fase1_runa1",
            "fase1_runa2",
            "fase1_runa3",

            "fase2_runa1",
            "fase2_runa2",
            "fase2_runa3",

            "fase3_runa1",
            "fase3_runa2",
            "fase3_runa3",
        };
    }
}
