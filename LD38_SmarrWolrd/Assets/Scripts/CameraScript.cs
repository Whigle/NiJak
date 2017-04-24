using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public AudioClip wind;
    public AudioClip ambient;
    public AudioClip music;
    public AudioClip join;
    AudioSource windAudio;
    AudioSource ambientAudio;
    AudioSource musicAudio;
    AudioSource joinAudio;
    public float cameraSpeed = 0.25f;
    public float zoomSpeed = 5f;
    static public float maxZoom = 2f;
    static public float minZoom = 30f;
    static public float zoom;
    public float edgeMargin = 25f;
    private float shakeDuration = 0f;
    private bool toShake = false;
    private bool flaga = false;
    private Vector3 position;
    static float pitch;
    float windVol, ambVol, musVol, joinVol, windPitch, ambPitch, musPitch, joinPitch;
     [SerializeField]
    private bool enableShaker = true;
    // Use this for initialization
    void Start () {
        windAudio = GetComponents<AudioSource>()[0];
        ambientAudio = GetComponents<AudioSource>()[1];
        musicAudio = GetComponents<AudioSource>()[2];
        joinAudio = GetComponents<AudioSource>()[3];
        windAudio.clip = wind;
        ambientAudio.clip = ambient;
        musicAudio.clip = music;
        joinAudio.clip = join;
        windVol = windAudio.volume;
        ambVol = ambientAudio.volume;
        musVol = musicAudio.volume;
        joinVol = joinAudio.volume;
        windPitch = windAudio.pitch;
        ambPitch = ambientAudio.pitch;
        musPitch = musicAudio.pitch;
        joinPitch = joinAudio.pitch;


        zoom = transform.position.z;
        pitch = (zoom / (maxZoom - minZoom));

    }

    // Update is called once per frame
    void Update()
    {
        float temp = (((joinPitch + 3) * (1 - pitch)));
        if (temp < joinPitch)
            joinAudio.pitch = temp;
        else joinAudio.pitch = joinPitch;
        temp = joinVol * (1 - pitch);
        if (temp > 0)
        {
            joinAudio.volume = temp;
        }
        else joinAudio.volume = joinVol;
        print(temp);
        windAudio.pitch = ((windPitch+3) * pitch)-4f;
        temp= windVol * pitch - 0.75f;
        windAudio.volume = temp;
        if (temp > 0)
        {
            musicAudio.volume = musVol - temp;
        }
        else musicAudio.volume = musVol;


        Vector3 mousePos = Input.mousePosition;
        if (mousePos.x >= 0f && mousePos.x <= Screen.width && mousePos.y >= 0f && mousePos.y <= Screen.height)
        {
            if (mousePos.x <= edgeMargin)
            {
                transform.Translate(new Vector3(-1f, 0f, 0f) * cameraSpeed,Space.World);
            }
            if (mousePos.y <= edgeMargin)
            {
                transform.Translate(new Vector3(0f, -1f, 0f) * cameraSpeed, Space.World);
            }
            if (mousePos.x >= Screen.width - edgeMargin)
            {
                transform.Translate(new Vector3(1f, 0f, 0f) * cameraSpeed, Space.World);
            }
            if (mousePos.y >= Screen.height - edgeMargin)
            {
                transform.Translate(new Vector3(0f, 1f, 0f) * cameraSpeed, Space.World);
            }
            if (Input.mouseScrollDelta.y > 0)
            {
                if (Mathf.Abs(transform.position.z) > maxZoom)
                {
                    transform.Translate(new Vector3(0f, 0f, 1f) * zoomSpeed);
                    zoom = transform.position.z;
                    pitch = (zoom / (maxZoom - minZoom));
                }
            }
            if (Input.mouseScrollDelta.y < 0)
            {
                if (Mathf.Abs(transform.position.z) < minZoom)
                {
                    transform.Translate(new Vector3(0f, 0f, -1f) * zoomSpeed);
                    zoom = transform.position.z;
                    pitch = (zoom / (maxZoom - minZoom));
                }
            }
        }
        if (enableShaker)
        {
            if (toShake)
            {
                shakeDuration -= Time.deltaTime;
                transform.position += new Vector3 (Random.Range (-0.5f, 0.5f), Random.Range (-0.3f, 0.3f), 0f);
            }
            if (shakeDuration < 0f && flaga)
            {
                toShake = false;
                flaga = false;
                transform.position = position;
            }
        }
    }

    public void SHAKE ()
    { 
        joinAudio.PlayOneShot(joinAudio.clip);
        shakeDuration = .5f;
        toShake = true;
        flaga = true;
        position = transform.position;
    }
}
