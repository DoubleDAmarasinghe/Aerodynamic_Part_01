using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Video;

public class GameplayNarrations : MonoBehaviour
{
    [SerializeField] private AudioClip[] audioClipsEnglish;
    [SerializeField] private string[] subtitlesEnglish;

    [SerializeField] private AudioClip[] audioClipsOther;
    [SerializeField] private string[] subtitlesOther;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private TMP_Text subtitleText;

    public float typingSpeed = 0.05f; // Adjust the speed by changing this value
    private Coroutine typingCoroutine;

    private int currentIndex = 0;
    [SerializeField] private bool canNarrationsStart = false;

    void Start()
    {
  
    }

    void Update()
    {
        // Check if the current audio has finished playing
        if (canNarrationsStart)
        {
            if (!audioSource.isPlaying)
            {
                switch (currentIndex)
                {
                    //welcoming player
                    case 0:
                        PlayNextAudioAccordingToLanguage();
                        Debug.Log("Voice 1");
                        break;
                    //start the brief introduction
                    case 1:
                        PlayNextAudioAccordingToLanguage();
                        Debug.Log("Voice 2");
                        break;
                    //let player to press decend button
                    case 2:
                        PlayNextAudioAccordingToLanguage();
                        Debug.Log("Voice 3");
                        break;
                    //reach waypoint tutorial decend and let player to press ascend button
                    case 3:
                        PlayNextAudioAccordingToLanguage();
                        Debug.Log("Voice 4");
                        break;
                    //reach waypoint tutorial descend let player to wipe watch
                    
                }
            }
            
            
        }
        
        
    }

    private void PlayNextAudioAccordingToLanguage() 
    {
        PlayNextAudio(audioClipsOther, subtitlesEnglish);
        //switch (LanguageSettings.AppLanguage)
        //{
        //    case LanguageSettings.Languages.PL:
        //        PlayNextAudio(audioClipsOther, subtitlesEnglish);
        //        Debug.Log("Polish");
        //        break;
        //    case LanguageSettings.Languages.EN:
        //        PlayNextAudio(audioClipsEnglish, subtitlesEnglish);
        //        Debug.Log("English");
        //        break;
        //    case LanguageSettings.Languages.ARAB:
        //        PlayNextAudio(audioClipsOther, subtitlesEnglish);
        //        Debug.Log("Arab");
        //        break;
        //    case LanguageSettings.Languages.CZ:
        //        PlayNextAudio(audioClipsOther, subtitlesEnglish);
        //        Debug.Log("CZ");
        //        break;
        //    case LanguageSettings.Languages.UA:
        //        PlayNextAudio(audioClipsOther, subtitlesEnglish);
        //        Debug.Log("UA");
        //        break;
        //    case LanguageSettings.Languages.DE:
        //        PlayNextAudio(audioClipsOther, subtitlesEnglish);
        //        Debug.Log("DE");
        //        break;
        //}
    }

    void PlayNextAudio(AudioClip[] audioClips, string[] subtittles)
    {
        // Check if there are more audio clips to play
        if (currentIndex < audioClips.Length)
        {
            // Play the next audio clip
            audioSource.clip = audioClips[currentIndex];
            audioSource.Play();

            // Display the corresponding subtitle text
            DisplaySubtitle(subtittles[currentIndex]);

            // Move to the next index for the next iteration
            currentIndex++;
        }
        else
        {
            // All audio clips have been played
            Debug.Log("Narration Completed");
            subtitleText.text = "";
        }
    }

    void DisplaySubtitle(string subtitle)
    {
        // Display the subtitle text on a UI element
        //subtitleText.text = subtitle;
        StartTyping(subtitle);
    }

    
    public void StartTyping(string newText)
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        typingCoroutine = StartCoroutine(AnimateText(newText));
    }

    public void StartNarations()
    {
        canNarrationsStart = true;
    }

    IEnumerator AnimateText(string newText)
    {
        subtitleText.text = "";

        foreach (char letter in newText.ToCharArray())
        {
            subtitleText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        typingCoroutine = null;
    }

    
}
