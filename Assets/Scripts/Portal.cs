using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [Header("Próxima Fase")]
    public string nomeCenaDestino = "Scene2";

    [Header("Tags dos Inimigos da Fase")]
    public string[] tagsInimigos = { "Enemy1", "Enemy2", "Enemy3" };

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player")) return;

        if (TodosInimigosMortos())
        {
            SceneManager.LoadScene(nomeCenaDestino);
        }
        else
        {
            Debug.Log("Ainda há inimigos vivos! Elimine todos para passar.");
        }
    }

    bool TodosInimigosMortos()
    {
        foreach (string tag in tagsInimigos)
        {
            if (GameObject.FindGameObjectsWithTag(tag).Length > 0)
                return false;
        }
        return true;
    }
}
