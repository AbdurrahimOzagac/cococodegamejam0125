using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    [SerializeField] private GameObject wall;
    [SerializeField] private GameObject wallWithDoor;
    [SerializeField] private GameObject ground;
    [SerializeField] private GameObject ceil;

    private int roomWidth1;
    private int roomWidth2;
    private int height = 10;

    // they are for test. delete after that
    [SerializeField] private GameObject player;
    private bool isGenerateRoom = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            GenerateRoom(player.gameObject.transform.position);
        }
    }

    public void GenerateRoom(Vector3 playerPosition) // call it from player
    {
        roomWidth1 = Random.Range(10, 21);
        roomWidth2 = Random.Range(10, 21);

        Vector3 position = new Vector3(playerPosition.x + roomWidth1/2 , playerPosition.y, playerPosition.z + 5);
        Vector3 position2 = new Vector3(playerPosition.x - roomWidth1 /2 , playerPosition.y, playerPosition.z + 5);
        Vector3 position3 = new Vector3(playerPosition.x + roomWidth1 , playerPosition.y, playerPosition.z + 5 + roomWidth2/2);
        Vector3 position4 = new Vector3(playerPosition.x - roomWidth1 , playerPosition.y, playerPosition.z + 5 + roomWidth2/2);

         

        Instantiate(wall, position, Quaternion.identity);
        Instantiate(wall, position2, Quaternion.identity);
        Instantiate(wall, position3, Quaternion.identity);
        Instantiate(wall, position4, Quaternion.identity);

    }

}
