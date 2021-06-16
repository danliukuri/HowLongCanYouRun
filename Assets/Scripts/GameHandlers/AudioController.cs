using UnityEngine;
using Utilities;

public class AudioController : MonoBehaviour
{
    void Start()
    {
        AudioManager.FadeInAndPlay("MenuTheme", 0.5f);
    }
    public static void StartGameplay()
    {
        AudioManager.Play("GameplayTheme");
    }
    public static void FinishGameplay()
    {
        AudioManager.FadeOutAndStop("GameplayTheme", 1f);
        AudioManager.Invoke(() => AudioManager.FadeInAndPlay("MenuTheme", 0.5f), 3f);
    }
}