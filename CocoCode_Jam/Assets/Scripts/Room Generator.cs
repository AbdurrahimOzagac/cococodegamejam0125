using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private int wallWidthSize = 2;

    private List<Vector3> cornersOfRoom = new List<Vector3>();

    // they are for test. delete after that
    [SerializeField] private GameObject player;
    private bool isGenerateRoom = false;

    void Start()
    {
        GenerateRoom(player.gameObject.transform.position + new Vector3(0, -2, -10));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            GenerateRoom(player.gameObject.transform.position);
        }
    }

    public void GenerateRoom(Vector3 playerPosition) // call it from player
    {
        // random number for room size
        widthWallNumber = Random.Range(4, 8);
        lenghtWallNumber = Random.Range(10, 16);

        // instantiate doors
        Vector3 startPositionOfRoom = new Vector3(playerPosition.x, playerPosition.y, playerPosition.z + 5);
        Instantiate(wallEntranceDoor, startPositionOfRoom, Quaternion.identity);
        Instantiate(wallEntranceDoor, startPositionOfRoom + new Vector3(0, 0, 2 + lenghtWallNumber * wallWidthSize), Quaternion.identity);

        
        // instantiate walls on the x axis (width)
        for (int i = -widthWallNumber; i <= widthWallNumber; i++)
        {
            if(i != 0)
            {
                Instantiate(wall, startPositionOfRoom + new Vector3(i * wallWidthSize, 0, 0), Quaternion.identity); //beginning of the room
                Instantiate(wall, startPositionOfRoom + new Vector3(i * wallWidthSize, 0, 2 + lenghtWallNumber* wallWidthSize), Quaternion.identity); // end of the room
            }

            // instantiate lamps on the wall
            if (i % 2 == 0)
            {
                Instantiate(lamp, startPositionOfRoom + new Vector3(i * wallWidthSize, 2.5f, 0.1f), Quaternion.identity); //beginning of the room
                Instantiate(lamp, startPositionOfRoom + new Vector3(i * wallWidthSize, 2.5f, 2 + lenghtWallNumber * wallWidthSize - 0.1f), Quaternion.Euler(0, -180, 0)); // end of the room
            }
        }
        // instantiate pillars
        Instantiate(pillar, startPositionOfRoom + new Vector3(1 + widthWallNumber* wallWidthSize, 0, 0), Quaternion.identity); // start left corner
        Instantiate(pillar, startPositionOfRoom - new Vector3(1 + widthWallNumber * wallWidthSize, 0, 0), Quaternion.identity); // start right corner
        Instantiate(pillar, startPositionOfRoom + new Vector3(1 + widthWallNumber * wallWidthSize, 0, 2 + lenghtWallNumber * wallWidthSize), Quaternion.identity); // end right corner
        Instantiate(pillar, startPositionOfRoom - new Vector3(1 + widthWallNumber * wallWidthSize, 0, -(2 + lenghtWallNumber * wallWidthSize)), Quaternion.identity); // end left corner
        cornersOfRoom.Clear();
        cornersOfRoom.Add(startPositionOfRoom + new Vector3(1 + widthWallNumber * wallWidthSize, 0, 0));
        cornersOfRoom.Add(startPositionOfRoom - new Vector3(1 + widthWallNumber * wallWidthSize, 0, 0));
        cornersOfRoom.Add(startPositionOfRoom + new Vector3(1 + widthWallNumber * wallWidthSize, 0, 2 + lenghtWallNumber * wallWidthSize));
        cornersOfRoom.Add(startPositionOfRoom - new Vector3(1 + widthWallNumber * wallWidthSize, 0, -(2 + lenghtWallNumber * wallWidthSize)));
        
        

        // instantiate walls on the z axis (lenght)
        for (int i = 0; i <= lenghtWallNumber; i++)
        {            
            Instantiate(wall, startPositionOfRoom + new Vector3(-(widthWallNumber * wallWidthSize + 1), 0, i * wallWidthSize + 1), Quaternion.Euler(0, -270, 0));//left side
            Instantiate(wall, startPositionOfRoom + new Vector3(widthWallNumber * wallWidthSize + 1, 0, i * wallWidthSize + 1), Quaternion.Euler(0, -90, 0)); // right side

            // instantiate lamps on the wall
            if (i%2 == 0)
            {
                Instantiate(lamp, startPositionOfRoom + new Vector3(-(widthWallNumber * wallWidthSize + 1) + 0.1f, 2.5f, i * wallWidthSize + 1), Quaternion.Euler(0, -270, 0)); // left side
                Instantiate(lamp, startPositionOfRoom + new Vector3(widthWallNumber * wallWidthSize + 1 - 0.1f, 2.5f, i * wallWidthSize + 1), Quaternion.Euler(0, -90, 0)); // left side

            }
        }

        // instantiate floor

        for (int i = 0; i <= lenghtWallNumber; i++)
        {
            for (int j = -widthWallNumber; j <= widthWallNumber; j++)
            {
                Instantiate(floor, startPositionOfRoom + new Vector3(j * wallWidthSize, 0, 1 + i * wallWidthSize), Quaternion.identity);
                Instantiate(floor, startPositionOfRoom + new Vector3(j * wallWidthSize, 3, 1 + i * wallWidthSize), Quaternion.identity);
            }
        }

        // finall small room to go other room
        Vector3 exitPosition = startPositionOfRoom + new Vector3(0, 0, 2 + lenghtWallNumber * wallWidthSize);
        Instantiate(wall, exitPosition + new Vector3(2, 0, 1), Quaternion.Euler(0, 90, 0));
        Instantiate(wall, exitPosition + new Vector3(-2, 0, 1), Quaternion.Euler(0, 90, 0));
        Instantiate(floor, exitPosition + new Vector3(1, 0, 1), Quaternion.identity);
        Instantiate(floor, exitPosition + new Vector3(-1, 0, 1), Quaternion.identity);
        Instantiate(floor, exitPosition + new Vector3(1, 3, 1), Quaternion.identity);
        Instantiate(floor, exitPosition + new Vector3(-1, 3, 1), Quaternion.identity);

    }



}
