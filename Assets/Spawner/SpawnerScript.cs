using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{

    private Collider _spawner;
    
    [Header("Game Object to Spawn")]
    [SerializeField] GameObject _objectToSpawn;

    [Header("Spawner Settings")]
    public bool _spawnerActive = true;
    [Range(0f, 5f)]
    public float _spawnerFrequency = 1;
    public bool _infinite = true;
    [Range(1f, 100f)]
    public int _spawnLimit = 1;

    [Header("Randomness Settings")]
    public bool _spawnInRandomLocation = false;
    public Vector3 _areaToSpawnIn = new Vector3(1,1,1);
    public Vector3 _areaCenter = new Vector3(0, 0, 0);

    [Header("Gizmo Visualization Settings")]
    [SerializeField]
    private bool _displayGizmos = false;
    [SerializeField]
    private bool _showOnlyWhileSelected = true;
    [SerializeField]
    private Color _gizmoColor = Color.blue;

    private void Awake()
    {
        _spawner = GetComponent<Collider>();
    }

    private void Start()
    {
        StartCoroutine(spawning());
    }

    IEnumerator spawning()
    {
        
        if(_spawnInRandomLocation == false)
        {
            if (_infinite == true)
            {
                while (_spawnerActive == true)
                {
                    yield return new WaitForSeconds(_spawnerFrequency);
                    Instantiate(_objectToSpawn, transform.position, transform.rotation);
                }
            }
            else
            {
                for (int i = 0; i < _spawnLimit; i++)
                {
                    yield return new WaitForSeconds(_spawnerFrequency);
                    Instantiate(_objectToSpawn, transform.position, transform.rotation);
                }
            }
        }
        else
        {
            if (_infinite == true)
            {
                
                while (_spawnerActive == true)
                {
                    yield return new WaitForSeconds(_spawnerFrequency);
                    Vector3 randomSpawnPosition = transform.localPosition + _areaCenter + new Vector3(Random.Range(-_areaToSpawnIn.x/2, _areaToSpawnIn.x/2), Random.Range(-_areaToSpawnIn.y / 2, _areaToSpawnIn.y / 2), Random.Range(-_areaToSpawnIn.z / 2, _areaToSpawnIn.z / 2));
                    Instantiate(_objectToSpawn, randomSpawnPosition, Quaternion.identity);
                }
            }
            else
            {
                
                for (int i = 0; i < _spawnLimit; i++)
                {
                    yield return new WaitForSeconds(_spawnerFrequency);
                    Vector3 randomSpawnPosition = transform.localPosition + _areaCenter + new Vector3(Random.Range(-_areaToSpawnIn.x / 2, _areaToSpawnIn.x / 2), Random.Range(-_areaToSpawnIn.y / 2, _areaToSpawnIn.y / 2), Random.Range(-_areaToSpawnIn.z / 2, _areaToSpawnIn.z / 2));
                    Instantiate(_objectToSpawn, randomSpawnPosition, Quaternion.identity);
                }
            }
        }
        
        
        
    }

    private void OnDrawGizmos()
    {
        if (_displayGizmos == false)
            return;
        if (_showOnlyWhileSelected == true)
            return;

        if (_spawner == null)
        {
            _spawner = GetComponent<Collider>();
        }
        Gizmos.color = _gizmoColor;
        Gizmos.DrawCube(transform.position, _spawner.bounds.size);
    }

    private void OnDrawGizmosSelected()
    {
        if (_displayGizmos == false)
            return;
        if (_showOnlyWhileSelected == false)
            return;

        if (_spawner == null)
        {
            _spawner = GetComponent<Collider>();
        }
        Gizmos.color = _gizmoColor;
        Gizmos.DrawCube(transform.position, _spawner.bounds.size);
        Gizmos.DrawWireCube(transform.localPosition + _areaCenter, _areaToSpawnIn);
        
    }

}
