using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class GerenciadorVideo : MonoBehaviour
{
    [Header("Configurações do Vídeo")]
    public VideoPlayer videoPlayer;
    public string nomeDoArquivoVideo;
    
    [Header("Configurações de Próxima Cena")]
    public bool carregarCenaAoTerminar = true;
    public string nomeDaProximaCena;

    void Start()
    {
        if (videoPlayer == null)
            videoPlayer = GetComponent<VideoPlayer>();

        string caminhoVideo = System.IO.Path.Combine(Application.streamingAssetsPath, nomeDoArquivoVideo);
        
        videoPlayer.source = VideoSource.Url;
        videoPlayer.url = caminhoVideo;

        videoPlayer.Prepare();

        videoPlayer.loopPointReached += AoTerminarVideo;
        
        videoPlayer.Play();
    }

    void AoTerminarVideo(VideoPlayer vp)
    {
        if (carregarCenaAoTerminar && !string.IsNullOrEmpty(nomeDaProximaCena))
        {
            SceneManager.LoadScene(nomeDaProximaCena);
        }
    }
}
