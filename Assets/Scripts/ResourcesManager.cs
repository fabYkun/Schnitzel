using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesManager : MonoBehaviour
{
    public static ResourcesManager instance = null;

    public Sprite[] character_sprites = new Sprite[(int)Emotion.MAX_EMOTIONS];
    [SerializeField]
    private AudioClip music_success;
    [SerializeField]
    private AudioClip music_failure;
    public AudioSource audio_source;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //Awake is always called before any Start functions
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
        


        for (int i = 0; i < (int)Emotion.MAX_EMOTIONS; i++)
        {
            character_sprites[i] = Resources.Load<Sprite>("Graphics/Textures/sprite_" + ((Emotion)i).ToString());
        }

        // TO DO: link with success
        audio_source.clip = music_success;
        audio_source.Play();
    }

    public Sprite getSpriteForEmotion(Emotion emotion)
    {
        return character_sprites[(int)emotion];
    }

    public AudioClip GetAudioClipForEmotion(Emotion emotion)
    {
        return audio_source.clip;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
  
}
