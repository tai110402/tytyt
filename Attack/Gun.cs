using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
    [SerializeField] private Animator _characterAnimator;
    [SerializeField] private CharacterMovement _characterMovement;
    [SerializeField] private Transform _camXZDirection;
    [SerializeField] private Transform _character;
    //public int gunDamage = 1;     
    public float fireRate = 0.25f;  
    public float weaponRange = 50f;   
    //public float hitForce = 100f;
    public Transform gunEnd;

    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _bulletSpeed = 10f;
    [SerializeField] private Camera fpsCam;  
    private WaitForSeconds shotDuration = new WaitForSeconds(0.07f);
    private float nextFire;
    private Vector3 _direction;

    void Start()
    {
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time > nextFire)
        {
            //Debug.Log("fire");

            if (_characterMovement.MoveDirection != Vector2.zero)
            {
                _characterAnimator.CrossFade("MoveShot", 0f, 2);

                // Rotate character to camera foward
                StartCoroutine(RotateToShot(0.5f));
            }
            else
            {
                _characterAnimator.CrossFade("IdleShot", 0f, 2);
                //StartCoroutine(RotateToShot(0.3f));
            }

            nextFire = Time.time + fireRate;
            Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

            // Declare a raycast hit to store information about what our raycast has hit
            RaycastHit hit;

            // Check if our raycast has hit anything
            if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, weaponRange))
            {
                _direction = hit.point - gunEnd.position;                
            } else
            {
                _direction = fpsCam.transform.forward;
            }
            _direction.Normalize();
            StartCoroutine(Shot());
        }
    }

    IEnumerator RotateToShot(float rotateTime)
    {
        float startTime = Time.time;
        while (Time.time - startTime < rotateTime)
        {
            Quaternion toRotation = Quaternion.LookRotation(_camXZDirection.forward, Vector3.up);
            _character.rotation = Quaternion.RotateTowards(_character.rotation, toRotation, 2000f);
            yield return null;
        }
    }

    IEnumerator Shot()
    {
        yield return new WaitForSeconds(0.15f);
        var bullet = Instantiate(_bulletPrefab, gunEnd.position, gunEnd.rotation);
        bullet.GetComponent<Rigidbody>().velocity = _direction * _bulletSpeed * 10;
    }
}