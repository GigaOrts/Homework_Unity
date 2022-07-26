using UnityEngine;

public class HomeCollision : MonoBehaviour
{
    [SerializeField] private Signalization signalization;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsThief(collision))
            signalization.IncreaseVolume();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (IsThief(collision))
            signalization.DecreaseVolume();
    }

    private bool IsThief(Collider2D collision)
    {
        return collision.gameObject.TryGetComponent(out Thief _);
    }
}