using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestSystem : MonoBehaviour
{
    [SerializeField] private GameObject topPart;
    [SerializeField] private List<GameObject> powerups = new List<GameObject>();
    private Quaternion targetRotation = Quaternion.Euler(-120, 0, 0);
    Powerup powerupScript;


    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V)){
            AnimateChest();
        }
    }

    private void AnimateChest()
    {
        StartCoroutine(OpenChest());
    }

    private IEnumerator OpenChest()
    {
        

        while (Quaternion.Angle(topPart.gameObject.transform.rotation, targetRotation) > 0.01f)
        {
            topPart.gameObject.transform.rotation = Quaternion.Lerp(topPart.gameObject.transform.rotation, targetRotation, 0.50f * Time.deltaTime);
            yield return null;
        }

        topPart.gameObject.transform.rotation = targetRotation;

        InstantiatePowerup();
        
    }

    private GameObject RandomPowerupGenerator()
    {
        int randomIndex = Random.Range(0, powerups.Count+1);
        return powerups[randomIndex];
    }

    private void InstantiatePowerup()
    {
        GameObject powerup = RandomPowerupGenerator();
        Instantiate(powerup, transform.position, Quaternion.identity);

        // take advantages
        powerupScript = powerup.GetComponent<Powerup>();
        powerupScript.ApplyPowerup(powerup);
        Destroy(powerup, 5);
        
    }

    
}
