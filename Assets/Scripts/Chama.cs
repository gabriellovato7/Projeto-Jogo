using UnityEngine;
using UnityEngine.SceneManagement;

public class Chama : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            PlayerPrefs.SetInt("JogoZerado", 1);
            PlayerPrefs.Save();
            SceneManager.LoadScene("Cutscene2");
        }
    }
}
