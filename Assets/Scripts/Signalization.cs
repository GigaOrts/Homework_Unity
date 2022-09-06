using System.Collections;
using UnityEngine;

public class Signalization : MonoBehaviour
{
    [SerializeField] private HomeCollision _home;
    [SerializeField] private AudioSource _audioSource;

    private Coroutine _changeVolumeCoroutine;
    private WaitForSeconds _waitForSeconds;

    private float _targetVolume;
    private float _changeStep = 0.1f;
    private float _timeToWait = 0.2f;

    public float MinVolume { get; private set; } = 0f;
    public float MaxVolume { get; private set; } = 1f;

    private void Awake()
    {
        _waitForSeconds = new WaitForSeconds(_timeToWait);
    }

    private void OnEnable()
    {
        _home.ThiefEntered += OnThiefEntered;
    }

    private void OnDisable()
    {
        _home.ThiefEntered -= OnThiefEntered;
    }

    private IEnumerator ChangeSirenaVolume(float targetVolume)
    {
        while (_audioSource.volume != targetVolume)
        {
            yield return _waitForSeconds;
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, _changeStep);
        }
    }

    private void OnThiefEntered(bool isThiefInHome)
    {
        _targetVolume = isThiefInHome ? MaxVolume : MinVolume;

        if (_changeVolumeCoroutine != null)
            StopCoroutine(_changeVolumeCoroutine);

        _changeVolumeCoroutine = StartCoroutine(ChangeSirenaVolume(_targetVolume));
    }
}
