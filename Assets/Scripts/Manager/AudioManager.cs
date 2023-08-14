using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public static GameManager gameManager;

    [Header("Music")]
    public AudioClip musicClip; //The background music
    public AudioClip gameOverClip;

    [Header("Player")]
    public AudioClip marcoDeathClip;

    [Header("Effects")]
    public AudioClip normalShotClip;
    public AudioClip shotHitClip;
    public AudioClip grenadeHitClip;
    public AudioClip collectibleGrabClip;
    public AudioClip grenadeGrabClip;

    [Header("Voice")]
    public AudioClip levelStart;
    public AudioClip levelComplete;
    public AudioClip okayClip;

    [Header("Menu")]
    public AudioClip menuSound; // menu sound
    public AudioClip selectSound; // any button

    [Header("Mixer Groups")]
    public AudioMixerGroup musicGroup; //The music mixer group
    public AudioMixerGroup effectGroup; //The effect mixer group
    public AudioMixerGroup enemyGroup; //The enemy mixer group
    public AudioMixerGroup playerGroup; //The player mixer group
    public AudioMixerGroup voiceGroup; //The voice mixer group

    private AudioSource musicSource; //Reference to the generated music Audio Source
    private AudioSource effectSource; //Reference to the generated effect Audio Source
    private AudioSource enemySource; //Reference to the generated enemy Audio Source
    private AudioSource playerSource; //Reference to the generated player Audio Source
    private AudioSource voiceSource; //Reference to the generated voice Audio Source

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;

        //Generate the Audio Source "channels" for music and effects
        musicSource = gameObject.AddComponent<AudioSource>();
        effectSource = gameObject.AddComponent<AudioSource>();
        enemySource = gameObject.AddComponent<AudioSource>();
        playerSource = gameObject.AddComponent<AudioSource>();
        voiceSource = gameObject.AddComponent<AudioSource>();

        //Assign each audio source to its respective mixer group so that we can adjust volumes
        musicSource.outputAudioMixerGroup = musicGroup;
        effectSource.outputAudioMixerGroup = effectGroup;
        enemySource.outputAudioMixerGroup = enemyGroup;
        playerSource.outputAudioMixerGroup = playerGroup;
        voiceSource.outputAudioMixerGroup = voiceGroup;

        RefreshAudioVolume();
        StartLevelAudio();
    }

    // Update is called once per frame
    void Update() { }

    // ====================================================================================================
    // ====================================================================================================
    // Utility functions

    public static void RefreshAudioVolume()
    {
        if (instance == null)
            return;

        instance.musicSource.volume = gameManager.bgmVolume;
        instance.effectSource.volume = gameManager.sfxVolume;
        instance.enemySource.volume = gameManager.sfxVolume;
        instance.playerSource.volume = gameManager.sfxVolume;
        instance.voiceSource.volume = gameManager.sfxVolume;
    }

    public bool IsPlayingOtherAudio(AudioClip clip, AudioSource source)
    {
        if (source.clip != clip && source.isPlaying)
            return true;
        return false;
    }


    // ====================================================================================================
    // ====================================================================================================
    // Player Audio

    public static void PlayDeathAudio()
    {

        Debug.Log("PlayDeathAudio");
        //If there is no instance AudioManager, exit
        if (instance == null)
            return;

        //Set the clip for music audio, and then tell it to play
        instance.playerSource.clip = instance.marcoDeathClip;
        instance.playerSource.Play();
    }

    public static void PlayLevelCompleteAudio()
    {
        //If there is no instance AudioManager, exit
        if (instance == null)
            return;

        //Play the initial level voice
        instance.voiceSource.clip = instance.levelComplete;
        instance.voiceSource.Play();
    }

    public static void PlayGameOverAudio()
    {
        //If there is no instance AudioManager, exit
        if (instance == null)
            return;

        //Set the clip for music audio, tell it not to loop, and then tell it to play
        instance.musicSource.clip = instance.gameOverClip;
        instance.musicSource.loop = false;
        instance.musicSource.Play();
    }

    public static void PlayNormalShotAudio()
    {
        //If there is no instance AudioManager, exit
        if (instance == null)
            return;

        AudioClip clip = instance.normalShotClip;
        AudioSource source = instance.playerSource;

        //Don't overshadow the other sounds
        if (instance.IsPlayingOtherAudio(clip, source))
            return;

        //Set the clip for music audio, and then tell it to play
        source.clip = clip;
        source.Play();
    }

    public static void PlayShotHitAudio()
    {
        //If there is no instance AudioManager, exit
        if (instance == null)
            return;

        //Set the clip for music audio, and then tell it to play
        instance.playerSource.clip = instance.shotHitClip;
        instance.playerSource.Play();
    }

    public static void PlayGrenadeHitAudio()
    {
        //If there is no instance AudioManager, exit
        if (instance == null)
            return;

        //Set the clip for music audio, and then tell it to play
        instance.playerSource.clip = instance.grenadeHitClip;
        instance.playerSource.Play();
    }

    public static void PlayAmmoGrab()
    {
        if (instance == null)
            return;
        instance.playerSource.clip = instance.grenadeGrabClip;
        instance.playerSource.Play();
    }

    public static void PlayMedKitGrab()
    {
        if (instance == null)
            return;
        instance.playerSource.clip = instance.collectibleGrabClip;
        instance.playerSource.Play();
    }

    // ====================================================================================================
    // ====================================================================================================
    // Menu Audio

    public static void PlayMenuSelect()
    {
        if (instance == null)
            return;
        instance.effectSource.clip = instance.selectSound;
        instance.effectSource.Play();
    }

    public static void PlayMenuBGM()
    {
        if (instance == null)
            return;
        instance.musicSource.clip = instance.menuSound;
        instance.musicSource.loop = true;
        instance.musicSource.Play();
    }

    public static void PlayOkayVoice()
    {
        if (instance == null)
            return;
        instance.voiceSource.clip = instance.okayClip;
        instance.voiceSource.Play();
    }

    // ====================================================================================================
    // ====================================================================================================
    // Level Audio (BGM)

    public static void StartLevelAudio()
    {
        //Set the clip for music audio, tell it to loop, and then tell it to play
        instance.musicSource.clip = instance.musicClip;
        instance.musicSource.loop = true;
        instance.musicSource.Play();
        PlayLevelStartAudio();
    }

    public static void PlayLevelStartAudio()
    {
        //If there is no instance AudioManager, exit
        if (instance == null)
            return;

        //Play the initial level voice
        instance.voiceSource.clip = instance.levelStart;
        instance.voiceSource.Play();
    }
}
