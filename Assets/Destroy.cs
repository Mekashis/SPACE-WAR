using UnityEngine;

public class destroy : MonoBehaviour
{
    private Animator animator;
    private bool hasPlayedOnce = false;

    void Start()
    {
       
        animator = GetComponent<Animator>();
    }

    void Update()
    {
       
        if (!hasPlayedOnce && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            
            hasPlayedOnce = true;

            
            DestroyObject();
        }
    }

    void DestroyObject()
    {
        
        Destroy(gameObject);
    }
}
