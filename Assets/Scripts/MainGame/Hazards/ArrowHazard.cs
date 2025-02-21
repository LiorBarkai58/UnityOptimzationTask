using System;
using UnityEngine;

public class ArrowHazard : MonoBehaviour
{
    [SerializeField] private ArrowObject arrowPrefab;
    [SerializeField] float shootInterval;
    private float shootIntervalLeft;
    private const float lifeTime = 3;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        shootIntervalLeft = shootInterval;
    }

    // Update is called once per frame
    void Update()
    {
        shootIntervalLeft -= Time.deltaTime;
        if (shootIntervalLeft <= 0)
        {
            ArrowObject arrow = Instantiate(arrowPrefab,transform.position,Quaternion.identity);
            Destroy(arrow, lifeTime);
            arrow.transform.forward = transform.forward;
            shootIntervalLeft = shootInterval;
        }
    }
}
