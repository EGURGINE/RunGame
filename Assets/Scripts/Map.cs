using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] private GameObject[] maps= new GameObject[3];
    public void MapSpawn()
    {
        Instantiate(maps[Random.Range(0, maps.Length)]).transform.position = new Vector3(150,0,0);
        Destroy(gameObject);
    }
}
