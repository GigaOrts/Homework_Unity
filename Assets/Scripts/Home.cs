using System.Collections;
using UnityEngine;

public class Home : MonoBehaviour
{
    private readonly float _minVolume = 0f;
    private readonly float _maxVolume = 1f;

    private float _changeStep = 0.1f;
    private float _waitSeconds = 0.2f;

    private AudioSource _audioSource;
    private Coroutine _increaseVolumeCoroutine;
    private Coroutine _decreaseVolumeCoroutine;

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
            if (_decreaseVolumeCoroutine != null)
                StopCoroutine(_decreaseVolumeCoroutine);

            _increaseVolumeCoroutine = StartCoroutine(IncreaseSirenaVolume());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Thief>())
        {
            if (_increaseVolumeCoroutine != null)
                StopCoroutine(_increaseVolumeCoroutine);

            _decreaseVolumeCoroutine = StartCoroutine(DecreaseSirenaVolume());
        }
    }

    private IEnumerator IncreaseSirenaVolume()
    {
        while (_audioSource.volume < _maxVolume)
        {
            yield return new WaitForSeconds(_waitSeconds);
            _audioSource.volume += Mathf.MoveTowards(_minVolume, _maxVolume, _changeStep);
        }
    }

    private IEnumerator DecreaseSirenaVolume()
    {
        while (_audioSource.volume > _minVolume)
        {
            yield return new WaitForSeconds(_waitSeconds);
            _audioSource.volume -= Mathf.MoveTowards(_minVolume, _maxVolume, _changeStep);
        }
    }
}
