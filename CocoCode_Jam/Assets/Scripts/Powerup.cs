using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerUpType { None, Poison, Health, Damage }
// poison fill your poison bar, health increase your health bar, damage increase your attack damage. Posison and damage come from chest
public class Powerup : MonoBehaviour
{
    private float rotateSpeed = 0.15f;
    private float moveUpDownSpeed = 0.20f;
    public PowerUpType powerUpType;
    void Start()
    {
        
    }

    void Update()
    {
        AnimateObject();
    }

    private void AnimateObject()
    {
        transform.Rotate(0, rotateSpeed * Time.deltaTime, 0, Space.Self);
        transform.position = new Vector3(transform.position.x, Mathf.Sin(Time.time) * moveUpDownSpeed, transform.position.z);
    }

    public void ApplyPowerup(GameObject powerup)
    {
        
        if (powerUpType == PowerUpType.Health)
        {
            // set max health
        }
        else if (powerUpType == PowerUpType.Damage)
        {
            // set damage
        }else if (powerUpType == PowerUpType.Poison)
        {
            // set poison 
        }

    }


}
