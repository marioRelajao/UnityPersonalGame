using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] AudioClip[] Music;
    [SerializeField] GameObject[] SFX;

    private GameObject theSFX;
    private AudioSource audioSource;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayLvlMusic();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayLvlMusic()
    {
        audioSource.clip = Music[Random.Range(0, Music.Length-1)]; //Queremos que la ultima sea si o si la de muerte
        audioSource.Play();
    }

    public void PlayGameOverMusic()
    {
        audioSource.clip = Music[Music.Length - 1];
        audioSource.Play();
    }

    public void PlaySFX(int sfxNumber)
    {
        //AudioSource.PlayClipAtPoint(SFX[sfxNumber], Camera.main.transform.position);
        theSFX = Instantiate(SFX[sfxNumber], Camera.main.transform);
        Invoke("DestroySFXGO", theSFX.GetComponent<AudioSource>().clip.length);
    }

    private void DestroySFXGO()
    {
        Destroy(theSFX);
    }
}
