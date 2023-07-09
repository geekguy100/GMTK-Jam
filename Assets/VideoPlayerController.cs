/*********************************
 * Author:          Kyle Grenier
 * Date Created:     
 /********************************/
using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerController : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private string fileName;
    
    private void OnEnable()
    {
        videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath,$"{fileName}.mp4");
        videoPlayer.Stop();
        videoPlayer.Play();
    }
}