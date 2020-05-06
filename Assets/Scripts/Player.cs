using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private int _playersLives = 3;
    private NumberField _numberField;
    private Component[] _spawnEnemy;
    private Component[] _spawnPowerup;
    private bool _isShieldsActive = false;
    [SerializeField]
    private GameObject _shield;

    // Start is called before the first frame update
    void Start()
    {
        _numberField = GameObject.Find("Lives").GetComponent<NumberField>();
        _spawnEnemy = GameObject.Find("EnemySpawners").GetComponentsInChildren<TimedSpawnerRandom>();
        _spawnPowerup = GameObject.Find("PowerupSpawners").GetComponentsInChildren<TimedSpawnerRandom>();
        if (_numberField != null)
        {
            _numberField.SetNumber(_playersLives);
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }

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
    public void ShieldsActive(bool flag)
    {
        _isShieldsActive = flag;
        _shield.SetActive(flag);
    }
    public IEnumerator Fade(float duration)
    {
        Renderer renderer = _shield.GetComponent<Renderer>();
        Color newColor = renderer.material.color;
        float f = 1f;
        float fade = f/duration;
        for (float i = duration; i > 0; i--)
        {
            newColor.a = f;
            renderer.material.color = newColor;
            f -= fade;
            Debug.Log("Shield: " + i + " seconds remaining!");
            yield return new WaitForSeconds(1);
        }
    }
}
