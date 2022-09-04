using System;
using UnityEngine;

public class HomeCollision : MonoBehaviour
{
    private bool _isThiefInHome;

    public event Action<bool> SignalizationChanged;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Thief _))
        {
            _isThiefInHome = true;
            SignalizationChanged?.Invoke(_isThiefInHome);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Thief _))
        {
            _isThiefInHome = false;
            SignalizationChanged?.Invoke(_isThiefInHome);
        }
    }
}