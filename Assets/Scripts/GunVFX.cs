using UnityEngine;

public class GunVFX : MonoBehaviour
{
    public AudioClip shootSound;
    public ParticleSystem shootParticle;
    private AudioSource audioSource;
    private void Awake()
    {
        if(TryGetComponent<AudioSource>(out AudioSource audio)) audioSource = audio;
    }
    public void PlayShootSound()
    {
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.PlayOneShot(shootSound);
    }
    public void PlayShootParticle() => shootParticle?.Play();
}
