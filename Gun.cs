using System;
using System.Diagnostics;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour{


    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;
    public float impactForce = 30f;
    [SerializeField] private bool canHoldDown;
    [SerializeField] private ObjPlacement objPl;
    [SerializeField] private bool isPlayer;
    private Animator animator;

    public Camera fpscamera;
    [SerializeField] private Transform shooter;
    public ParticleSystem muzzleflash;
    [SerializeField] private GameObject spotLight;
    [SerializeField] private float turnOffLight;
    [SerializeField] private float turnOnLight;
    [SerializeField] private Image hitMarker;
    [SerializeField] private AudioClip clip;
    [SerializeField] private AudioSource sound;
    [SerializeField] private TrailRenderer bulTrail;
    [SerializeField] private Transform bulSpawnPos;
    [SerializeField] private float trailTime;
    [SerializeField] private Vector3 BulletSpreadVariance = new Vector3(0.1f, 0.1f, 0.1f);
    [SerializeField] private bool AddBulletSpread = true;
    [SerializeField] private float BulletSpeed = 100;
    private Vector3 getDirDir;
    public GameObject impactEffect;

    private float nextTimeToFire = 0f;

    // Update is called once per frame
    void Start ()
    {
        animator = GetComponent<Animator>();
    }
    void Update ()
    {
        if (Time.time >= turnOffLight)
            {
                spotLight.SetActive(false);
                if (hitMarker)
                {
                    hitMarker.color = new Color32(0,0,0,255);
                }
            }
        if (isPlayer)
        {
            if (!canHoldDown && Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire && !objPl.menuOpen) 
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                Shoot(damage);
            }
            else if (canHoldDown && Input.GetButton("Fire1") && Time.time >= nextTimeToFire && !objPl.menuOpen)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                Shoot(damage);
            }
        }

    }

    public void Shoot (float damage)
    {
        muzzleflash.Play();
        sound.PlayOneShot(clip);
        spotLight.SetActive(true);
        animator.SetTrigger("Shoot");
        turnOffLight = Time.time + turnOnLight;

        RaycastHit hit;
        Vector3 direction = GetDirection();
        if (fpscamera)
        {
            if (Physics.Raycast(fpscamera.transform.position, direction, out hit, range))
            {
                StartCoroutine(DrawTrail(hit.point, hit.normal, hit));
            }
            else
            {
                StartCoroutine(DrawTrail(bulSpawnPos.position + GetDirection() * 100, Vector3.zero, hit));
            }
        }
        else if (shooter)
        {
            if (Physics.Raycast(shooter.position, shooter.forward, out hit, range))
            {
                StartCoroutine(DrawTrail(hit.point, hit.normal, hit));
            }
            else
            {
                StartCoroutine(DrawTrail(bulSpawnPos.position + GetDirection() * 100, Vector3.zero, hit));
            }
        }

    }

    private IEnumerator DrawTrail(Vector3 HitPoint, Vector3 HitNormal, RaycastHit hit)
    {
        TrailRenderer Trail = Instantiate(bulTrail, bulSpawnPos.position, Quaternion.identity);
        Vector3 startPosition = Trail.transform.position;
        float distance = Vector3.Distance(Trail.transform.position, HitPoint);
        float remainingDistance = distance;

        while (remainingDistance > 0)
        {
            Trail.transform.position = Vector3.Lerp(startPosition, HitPoint, 1 - (remainingDistance / distance));

            remainingDistance -= BulletSpeed * Time.deltaTime;

            yield return null;
        }
        Trail.transform.position = HitPoint;

        Destroy(Trail.gameObject, Trail.time);
        Target target = null;
        if (hit.transform.GetComponent<Target>())
        {
            target = hit.transform.GetComponent<Target>();
        }
        if (target != null)
            {
                target.TakeDamage(damage);
                if (hitMarker)
                {
                    hitMarker.color = new Color32(255,0,0,255);
                }
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }
    
    }

    private Vector3 GetDirection()
    {
        if (fpscamera)
        {
            getDirDir = fpscamera.transform.forward;
        }
        else if (shooter)
        {
            getDirDir = shooter.transform.forward;
        }
        

        if (AddBulletSpread)
        {
            getDirDir += new Vector3(
                UnityEngine.Random.Range(-BulletSpreadVariance.x, BulletSpreadVariance.x),
                UnityEngine.Random.Range(-BulletSpreadVariance.y, BulletSpreadVariance.y),
                UnityEngine.Random.Range(-BulletSpreadVariance.z, BulletSpreadVariance.z)
            );

            getDirDir.Normalize();
        }

        return getDirDir;
    }
}