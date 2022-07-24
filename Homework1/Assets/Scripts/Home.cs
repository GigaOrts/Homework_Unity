using System.Collections;
using UnityEngine;

public class Home : MonoBehaviour
{
    private AudioSource _audioSource;
    private float _minVolume = 0f;
    private float _maxVolume = 1f;

    private void Awake()
    {
        if (TryGetComponent(out AudioSource audioSource))
        {
            _audioSource = audioSource;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Thief>())
        {
            StopCoroutine(DecreaseSirenaVolume());
            StartCoroutine(IncreaseSirenaVolume());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Thief>())
        {
            StopCoroutine(IncreaseSirenaVolume());
            StartCoroutine(DecreaseSirenaVolume());
        }
    }

    private IEnumerator IncreaseSirenaVolume()
    {
        while (_audioSource.volume <= 1f)
        {
            yield return new WaitForSeconds(0.1f);
            _audioSource.volume += Mathf.MoveTowards(_minVolume, _maxVolume, 1f);
            Debug.Log("UP: " + _audioSource.volume);
        }
    }

    private IEnumerator DecreaseSirenaVolume()
    {
        while (_audioSource.volume >= 0f)
        {
            yield return new WaitForSeconds(0.1f);
            _audioSource.volume += Mathf.MoveTowards(_maxVolume, _minVolume, 1f);
            Debug.Log("DOWN: " + _audioSource.volume);
        }
    }
}
