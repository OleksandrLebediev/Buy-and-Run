using UnityEngine;
using UnityEngine.Events;

public class FinishLine : MonoBehaviour
{
    public event UnityAction<Player> Finished;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            Finished?.Invoke(player);
        }
    }

}
