using System;
using System.Collections.Generic;
using UnityEngine;

public class WasmTest : MonoBehaviour
{
    List<GameObject> _gameObjects = new List<GameObject>();

    private void Start()
    {
        try
        {
            for (int i = 0; i < 576; i++)
            {
                //String name = $"GameObject{i}";
                GameObject go = new GameObject();
                go.transform.parent = transform;
                _gameObjects.Add(go);
                _gameObjects[i].transform.position = new Vector3(i % 24, 0, (float)Math.Floor(i / 24.0));
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
            throw;
        }
    }

    private void Update()
    {
        foreach (var o in _gameObjects)
        {
            Vector3 position = o.transform.position;
            double time = DateTime.UtcNow.Ticks / (double)TimeSpan.TicksPerSecond;
            time += (position.x + position.z) / 10.0d;
            position.y = (float)Math.Sin(time);
            o.transform.position = position;
        }
    }
}