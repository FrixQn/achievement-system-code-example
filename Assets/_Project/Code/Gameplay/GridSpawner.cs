using System;
using UnityEngine;

namespace DemoProject.Gameplay
{
    public class GridSpawner : MonoBehaviour
    {
        public T[] SpawnObjects<T>(T original, int width, int height, float spacingX = 1f, float spacingY = 1f, Vector2 offset = default)
            where T : UnityEngine.Object
        {
            if (original == null)
                throw new ArgumentException("The Object you want to instantiate is null.");

            width = Mathf.Clamp(width, 0, int.MaxValue);
            height = Mathf.Clamp(height, 0, int.MaxValue);

            T[] objects = new T[width*height];
            Vector2 startPosition = (Vector2)transform.position - 
                new Vector2((width - 1) * 0.5f * spacingX, (height - 1) * 0.5f * spacingY) + 
                offset;
            Vector2 spawnPosition;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    spawnPosition = startPosition + new Vector2(x * spacingX, y * spacingY);

                    objects[y*width + x] = Instantiate(original, spawnPosition, Quaternion.identity, transform);
                }
            }

            return objects;
        }
    }
}
