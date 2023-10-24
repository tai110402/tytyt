using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageObectUseTrigger : MonoBehaviour
{
    [SerializeField] private int _damage = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        DamageableObject damgeableObject = other.GetComponent<DamageableObject>();

        if (damgeableObject != null)
        {
            damgeableObject.Damage(_damage);
            Debug.Log("dame");
        }
    }
}
