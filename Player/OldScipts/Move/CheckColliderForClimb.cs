using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckColliderForClimb : MonoBehaviour
{
    [SerializeField] private string _climbTag;
    private bool _canClimb;

    //getters and setters
    public bool CanClimb { get { return _canClimb; } set { _canClimb = value; } }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(_canClimb);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(_climbTag))
        {
            _canClimb = true;
            //Debug.Log(_canClimb);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        _canClimb = false;
        //Debug.Log(_canClimb);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_climbTag))
        {
            _canClimb = true;
            //Debug.Log(_canClimb);
        }
    }

    //public bool CanClimb ()
    //{
    //    return _canClimb;
    //}
}
