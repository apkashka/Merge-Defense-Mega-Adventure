using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    
    public Transform SpawnPoint => _spawnPoint;

    public float Damage = 10; //todo
    // Start is called before the first frame update
    void Start()
    {
        
    }



    public void Shoot()
    {
        
    }
}
