using UnityEngine;

public class UmbrellaSpawner : MonoBehaviour
{
    public GameObject spritePrefab; // Assign the sprite prefab in the inspector
    public Vector3 offset = new Vector3(0, 1, 0); // Offset above the character
    private GameObject spawnedSprite;

    void Start()
    {
        ToggleSprite();
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToggleSprite();
        }
    }

    void ToggleSprite()
    {
        if (spawnedSprite == null)
        {
            // Spawn the sprite and make it a child of the character
            Vector3 spawnPosition = transform.position + offset;
            spawnedSprite = Instantiate(spritePrefab, spawnPosition, Quaternion.identity);
            spawnedSprite.transform.SetParent(transform);
        }
        else
        {
            // Delete the sprite
            Destroy(spawnedSprite);
        }
    }
}