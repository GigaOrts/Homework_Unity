using System;
using UnityEngine;

public class HomeCollision : MonoBehaviour
{
    public event Action<bool> ThiefEntered;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Thief _))
        {
            ThiefEntered?.Invoke(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Thief _))
        {
            ThiefEntered?.Invoke(false);
        }
    }
}