# gamedev-06-q1
Contains all the first question answers from lesson 6 homework: [gamedev-5780](https://github.com/erelsgl-at-ariel/gamedev-5780)

**Created by:**

[Chen Ostrovski](https://github.com/ChenOst)

[Enna Grigor](https://github.com/ennagrigor)

In this question we added additional objects to the game we were working on in class.
## Borders
### Added to: KeyBoardMover Script 
##### Flat World
Invisible Borders, The player cannot move up or down beyond the defined y axis coordinate.
The Player can't go over the y value : 3.0f or -3.0f.

```
if (transform.position.y >= 3.0f)
{
    transform.position = new Vector3(transform.position.x, 3.0f, 0);
}
else if (transform.position.y <= -3.0f)
{
    transform.position = new Vector3(transform.position.x, -3.0f, 0);
}
```
##### Round world
The player comes to one side of the world and appears on the other side.

```
if (transform.position.x >= 10.0f)
{
    transform.position = new Vector3(-10.0f, transform.position.y, 0);
}
else if (transform.position.x <= -10.0f)
{
    transform.position = new Vector3(10.0f, transform.position.y, 0);
}
```
### Added to: Mover Script 
If an object leaves the screen boundaries it is deleted.
The player can't leave the boundaries.

Deleted item can be:
- Enemy
- Shield
- Laser
- Extra Shot

```
if (transform.position.y <= -7)
{
    Destroy(this.gameObject);
}
else if (transform.position.y >= 10)
{
    Destroy(this.gameObject);
}
```

## Player Lives
### Added new Script: Player
Contains information about the player,
such as the amount of life, shield and whether he collided 
with an enemy while the shield was activated or not and accordingly send info to `Lives` obejct.

- `Lives` obejct - Located in the right corner of the screen, shows the player at any
 given moment the number of lives he has left.
 
 <img src="Images/lives.png" width=400>
 
When the player dies (Lives = 0), we decied to stop the spawning of the other objects.

```
public void TakingDamage()
{
    if (!_isShieldsActive)
    {
        _playersLives--;
        if (_numberField != null)
        {
            _numberField.SetNumber(_playersLives);
        }
        if (_playersLives == 0)
        {
            foreach (TimedSpawnerRandom tsp in _spawnEnemy)
            {
                Debug.Log(tsp.ToString() + " stoped spawning");
                tsp.StopSpawning();
            }
            foreach (TimedSpawnerRandom tsp in _spawnPowerup)
            {
                Debug.Log(tsp.ToString() + " stoped spawning");
                tsp.StopSpawning();
            }
            Destroy(this.gameObject);
        }
    }
}
```
TakingDamage() is activated when collision is spotted.

## Powerup Spawner
We created `PowerupSpawner` object in which we put the `ShieldSpawner` and
 `ExtraShotSpawner`, in both we use `TimedSpawnerRandom` script in order
to spawn the Shield and Extra Shot. 
## Shield
### Added to: ShieldThePlayer Script 
When collision between Shield and Player is detected, this script
activates the player's shield for 5 seconds, with every second the shield disappears.
 While the player has the shield he is not hit by enemies.
 
```
private void OnTriggerEnter2D(Collider2D other) {
    if (other.tag == "Player") 
    {
        Debug.Log("Shield triggered by player");
        player = other.transform.GetComponent<Player>();
        if (player != null)
        {
            player.ShieldsActive(true);
        }
    ...
```
The function that makes the shield fade:
```
public IEnumerator Fade()
{
    Renderer renderer = _shield.GetComponent<Renderer>();
    Color newColor = renderer.material.color;
    int i = 5;
    for (float f = 1f; f >= 0; f -= 0.20f)
    {
        newColor.a = f;
        renderer.material.color = newColor;
        Debug.Log("Shield: " + i-- + " seconds remaining!");
        yield return new WaitForSeconds(1f);
    }
}
```

 <img src="Images/shield1.png" width=400>  <img src="Images/shield2.png" width=400>
 
## Extra Shot
### Added new Script: ExtraShotSpawner
When collision between ExtraShot and Player is detected, this script
activates the player's extra shot for 5 seconds.

```
private void OnTriggerEnter2D(Collider2D other)
{
    if (other.tag == "Player")
    {
        Debug.Log("ExtraShot triggered by player");
        player = other.transform.GetComponent<KeyboardSpawner>();
        if (player != null)
        {
            player.PowerupActive(true);
        }
    ...
```

The `KeyboardSpawner` is changing the laser from regular to extra super cool laser.
```
if (extraIsActive)
{
    newObject = Instantiate(extraLaserShot, positionOfSpawnedObject, rotationOfSpawnedObject);
}
else
{
    newObject = Instantiate(prefabToSpawn, positionOfSpawnedObject, rotationOfSpawnedObject);
}
```

<img src="Images/laser1.png" width=400>  <img src="Images/laser2.png" width=400>

## Laser cooldown
Another thing we added is that the player can't shoot straight at infinite
 amount but he has to wait a certain time between shot and shot.
 
 ### Added to: KeyboardSpawner Script 
 
```
private void Update() {
if (Input.GetKeyDown(keyToPress) && Time.time > _nextLaser)
{
     _nextLaser = Time.time + _cooldown;
    spawnObject();
    }
}
```