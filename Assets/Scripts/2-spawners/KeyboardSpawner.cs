using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This component spawns the given object whenever the player clicks a given key.
 */
public class KeyboardSpawner: MonoBehaviour {
    [SerializeField] protected KeyCode keyToPress;
    [SerializeField] protected GameObject prefabToSpawn;
    [SerializeField] protected Vector3 velocityOfSpawnedObject;
    [SerializeField] protected GameObject extraLaserShot;
    private bool extraIsActive = false;

    // The player must wait between the laser shots
    [SerializeField]
    private float _cooldown = 0.5f;
    private float _nextLaser = 0.0f;

    protected virtual GameObject spawnObject() {
        // Step 1: spawn the new object.
        GameObject newObject;
        Vector3 positionOfSpawnedObject = transform.position;  // span at the containing object position.
        Quaternion rotationOfSpawnedObject = Quaternion.identity;  // no rotation.
        if (extraIsActive)
        {
            newObject = Instantiate(extraLaserShot, positionOfSpawnedObject, rotationOfSpawnedObject);
        }
        else
        {
            newObject = Instantiate(prefabToSpawn, positionOfSpawnedObject, rotationOfSpawnedObject);
        }

        // Step 2: modify the velocity of the new object.
        Mover newObjectMover = newObject.GetComponent<Mover>();
        if (newObjectMover) {
            newObjectMover.SetVelocity(velocityOfSpawnedObject);
        }

        return newObject;
    }

    private void Update() {
        if (Input.GetKeyDown(keyToPress) && Time.time > _nextLaser) {
            _nextLaser = Time.time + _cooldown;
            spawnObject();
        }
    }

    public void PowerupActive(bool flag)
    {
        extraIsActive = flag;
    }
}
