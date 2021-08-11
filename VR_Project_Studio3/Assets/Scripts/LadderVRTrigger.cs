using UnityEngine;

public class LadderVRTrigger : MonoBehaviour
{
    public string tagPlayer;
    [HideInInspector]
    public bool collision;
    void OnTriggerEnter(Collider other)
    {
        if (!FindObjectOfType<GameSetup>().isFlatScreen && other.CompareTag(tagPlayer))
        {
            collision = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (!FindObjectOfType<GameSetup>().isFlatScreen && other.CompareTag(tagPlayer))
        {
            collision = false;
        }
    }
}
