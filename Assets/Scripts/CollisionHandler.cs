using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip successSFX;
    [SerializeField] AudioClip crashSFX;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticle;
    
    AudioSource audioSource;

    bool isControllable = true;
    bool isCollidable = true;
    
    private void Start() 
    {
        audioSource =GetComponent<AudioSource>();    
    }

    private void Update() {
        ResponfToDebugKeys();
    }
     void ResponfToDebugKeys()
 {
    if(Keyboard.current.lKey.wasPressedThisFrame)
    {
       LoadNextLevel();
    }
    else if(Keyboard.current.cKey.wasPressedThisFrame)
    {
    isCollidable = !isCollidable;
    
    }
    }
 private void OnCollisionEnter(Collision other) 
 { 
    if(!isControllable || !isCollidable){return;}   

    switch (other.gameObject.tag)
    {
        case "Friendly":
            Debug.Log("its friendly");
            break;

        case "Finish":
            StartSuccesSequence();
            break;
        default:
            StartCrashSequence();
            break;
        
      
    } 
 }

    private void StartSuccesSequence()
    {
        isControllable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(successSFX);
        successParticles.Play();
        GetComponent<Movement>().enabled=false;
        Invoke("LoadNextLevel",levelLoadDelay);
    }

    private void StartCrashSequence()
    {
        //tolga add sfx and particles
        isControllable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(crashSFX);
        crashParticle.Play();
        GetComponent<Movement>().enabled=false;
        Invoke("ReloadLevel",levelLoadDelay); 
    }

    void LoadNextLevel()
 {
    int currentScene = SceneManager.GetActiveScene().buildIndex ;
    int nextScene = currentScene +1;
     
    if(nextScene == SceneManager.sceneCountInBuildSettings)
    {
        nextScene = 0;
    }



    SceneManager.LoadScene(nextScene);
 }
 void ReloadLevel()
 {
    int currentScene = SceneManager.GetActiveScene().buildIndex;
    SceneManager.LoadScene(currentScene);
 }

 }
 
 
 

