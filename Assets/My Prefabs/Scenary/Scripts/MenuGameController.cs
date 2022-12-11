using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuGameController : MonoBehaviour
{
    public AudioSource allAudioSources;
    private Rigidbody playerRigBody;

    public void Awake()
    {
        playerRigBody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        playerRigBody.constraints = RigidbodyConstraints.FreezePosition;

        //////if (allAudioSources.Length == 0) 
        //////{
        //////    throw new UnityException("Unable to find audio source");
        //////}
    }

    public void ReiniciarCena()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        return;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        playerRigBody.constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        Time.timeScale = 1;

    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
            Application.Quit();
    }

    public void PlayAudio()
    {
        allAudioSources.Play();
    }

    public void PauseAllAudio()
    {
        //allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        //foreach (AudioSource audioS in allAudioSources)
        //{
        allAudioSources.Pause();
        //}
    }

    public void StopAudio()
    {
        //allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        //foreach (AudioSource audioS in allAudioSources)
        //{
        allAudioSources.Stop();
        //}
    }
}
