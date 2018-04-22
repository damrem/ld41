using UnityEngine;

public enum TriggerType { Enter, Exit };

public class DestroyOnTrigger2D : MonoBehaviour
{
    public TriggerType triggerType = TriggerType.Exit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (triggerType == TriggerType.Enter) Destroy(gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (triggerType == TriggerType.Exit) Destroy(gameObject);
    }
}
