using System.Collections;
using UnityEngine;

public class Signalization : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    private Coroutine _changeVolumeCoroutine;
    private WaitForSeconds _waitForSeconds;
    private float _changeStep = 0.1f;
    private float _timeToWait = 0.2f;

    public float MinVolume { get; private set; } = 0f;
    public float MaxVolume { get; private set; } = 1f;

    private void Awake()
    {
        _waitForSeconds = new WaitForSeconds(_timeToWait);
    }

    public void IncreaseVolume() => ChangeVolume(MaxVolume);

    public void DecreaseVolume() => ChangeVolume(MinVolume);

    private void ChangeVolume(float volume)
    {
        if (_changeVolumeCoroutine != null)
            StopCoroutine(_changeVolumeCoroutine);

        _changeVolumeCoroutine = StartCoroutine(ChangeSirenaVolume(volume));
    }

    private IEnumerator ChangeSirenaVolume(float targetVolume)
    {
        while (_audioSource.volume != targetVolume)
        {
            yield return _waitForSeconds;
            _audioSource.volume += _audioSource.volume < targetVolume ?
                Mathf.MoveTowards(MinVolume, MaxVolume, _changeStep) :
                -Mathf.MoveTowards(MinVolume, MaxVolume, _changeStep);
        }
    }
}