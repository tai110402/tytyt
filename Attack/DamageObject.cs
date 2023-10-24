using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageObject : MonoBehaviour
{
    [SerializeField] private int _damage = 1;
    //private Collider _collider;

    // Start is called before the first frame update
    void Start()
    {
        //_collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        DamageableObject damageableObject = collision.collider.GetComponent<DamageableObject>();

        if (damageableObject != null)
        {
            damageableObject.Damage(_damage);
            Debug.Log("take damge");
        }

        //gameObject.SetActive(false);
    }
}
