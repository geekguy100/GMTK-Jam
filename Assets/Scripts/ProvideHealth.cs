using UnityEngine;

//
public class ProvideHealth : MonoBehaviour
{
    [SerializeField][Min(0)] private float amount;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        var incomingObject = other.gameObject;
        if (!incomingObject.CompareTag("Fighter"))
        {
            return;
        }

        EnvironmentObject fighter = incomingObject.GetComponent<EnvironmentObject>();
        fighter.AddHealth(amount);
        
        GetComponent<EnvironmentObject>().OnRemove();
    }
}