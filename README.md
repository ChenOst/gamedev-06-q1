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
with an enemy while the shield was activated or not and accordingly send info to "Lives" obejct.

- "Lives" obejct - Located in the right corner of the screen, shows the player at any
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