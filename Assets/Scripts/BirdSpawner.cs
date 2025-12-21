using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BirdSpawner : MonoBehaviour
{
    [Header("Prefab")]
    public GameObject birdPrefab;

    [Header("Spawn cadence")]
    public float spawnEvery = 0.5f;
    public Vector2 spawnEveryJitter = new Vector2(-0.5f, 0.5f);

    [Header("Vertical band (world units)")]
    public float minY = -1.0f;   
    public float maxY = 1.0f;

    [Header("Right-edge offset")]
    public float xMargin = 1.0f; 

    float _timer;

    void OnEnable()
    {
        _timer = NextDelay();
    }

    void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0f)
        {
            SpawnOne();
            _timer = NextDelay();
        }
    }
    
    float NextDelay()
    {
        float jitter = Random.Range(spawnEveryJitter.x, spawnEveryJitter.y);
        return Mathf.Max(0.05f, spawnEvery + jitter);
    }

    void SpawnOne()
    {
        var cam = Camera.main;
        if (!cam) return;

        // robust orthographic bounds
        float right = cam.transform.position.x + cam.orthographicSize * cam.aspect;
        float x = right + xMargin;

        // clamp Y band and pick inside it
        float yMin = Mathf.Min(minY, maxY);
        float yMax = Mathf.Max(minY, maxY);
        float y = Random.Range(yMin, yMax);

        // spawn at Z = 0 for 2D
        Vector3 pos = new Vector3(x, y, 0f);

        // Instantiate in world space (no parent) so prefab local offsets don't matter
        Instantiate(birdPrefab, pos, Quaternion.identity);
    }

#if UNITY_EDITOR
    // Gizmo so you can see the vertical band in the Scene view while playing
    void OnDrawGizmosSelected()
    {
        var cam = Camera.main;
        if (!cam) return;
        float right = cam.transform.position.x + cam.orthographicSize * cam.aspect;
        Vector3 a = new Vector3(right + xMargin, minY, 0);
        Vector3 b = new Vector3(right + xMargin, maxY, 0);
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(a, b);
        Gizmos.DrawSphere(a, 0.05f);
        Gizmos.DrawSphere(b, 0.05f);
    }
#endif
}
