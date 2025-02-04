using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    

    [SerializeField] private GameObject pillar;
    [SerializeField] private GameObject wallEntrance;
    [SerializeField] private GameObject door;
    [SerializeField] private GameObject floor;
    [SerializeField] private GameObject lamp;
    [SerializeField] private GameObject wall;
    [SerializeField] private GameObject wallEntranceDoor;

    private int roomWidth1;
    private int roomWidth2;

    private int widthWallNumber;
    private int lenghtWallNumber;
    private int height = 10;
    private int wallwidth = 2;

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
        widthWallNumber = Random.Range(6, 11);
        lenghtWallNumber = Random.Range(10, 16);

        Vector3 position1 = new Vector3(playerPosition.x + roomWidth1 / 2 + 1.5f, playerPosition.y, playerPosition.z + 5);
        Vector3 position2 = new Vector3(playerPosition.x - roomWidth1 /2 - 1.5f, playerPosition.y, playerPosition.z + 5);
        Vector3 position3 = new Vector3(playerPosition.x + roomWidth1 , playerPosition.y, playerPosition.z + 5 + roomWidth2/2);
        Vector3 position4 = new Vector3(playerPosition.x - roomWidth1 , playerPosition.y, playerPosition.z + 5 + roomWidth2/2);


        Vector3 startPositionOfRoom = new Vector3(playerPosition.x, playerPosition.y, playerPosition.z + 5);
        Instantiate(wallEntranceDoor, startPositionOfRoom, Quaternion.identity);
        for(int i = -widthWallNumber; i <= widthWallNumber; i++)
        {
            if(i != 0)
            {
                Instantiate(wall, startPositionOfRoom + new Vector3(i * wallwidth, 0, 0), Quaternion.identity);
            }
        }
        Instantiate(pillar, startPositionOfRoom + new Vector3(1 + widthWallNumber* wallwidth, 0, 0), Quaternion.identity);
        Instantiate(pillar, startPositionOfRoom - new Vector3(1 + widthWallNumber * wallwidth, 0, 0), Quaternion.identity);




    }

}
