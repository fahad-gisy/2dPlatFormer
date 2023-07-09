using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    [HideInInspector] public AudioSource audioSource;//audioSourceComponents
    [SerializeField] private AudioClip shootSoundClip,impactShootCilp;//cilps to play 
    [SerializeField] private AudioClip JumpSound, JumpLandSound;//cilps to play 
    [SerializeField] private AudioClip slimeMovementSound, SlimeDeath;//cilps to play 
    [SerializeField] private AudioClip playerDamaged;//cilp to play 

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();//getting the audioSourceComponent
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayPlayerDamagedSound()
    {//when called play player damaged Sound
        audioSource.PlayOneShot(playerDamaged);
    }

    public void PlayJumpSound()
    {//when called play jumpSound
        audioSource.PlayOneShot(JumpSound);
    }

    public void PlayJumpLandSound()
    {//when called play jump Land Sound
        audioSource.PlayOneShot(JumpLandSound);
    }

    public void PlayShootSound()
    {//when called play shoot sound
        audioSource.PlayOneShot(shootSoundClip);
    }

    public void PlayShootImpactSound()
    {////when called play shoot impact sound
        audioSource.PlayOneShot(impactShootCilp);
    }

    public void PlaySlimeMovementSound()
    {
        audioSource.PlayOneShot(slimeMovementSound);
    }

    public void PlaySlimeDeathSound()
    {
        audioSource.PlayOneShot(SlimeDeath);
    }
}
