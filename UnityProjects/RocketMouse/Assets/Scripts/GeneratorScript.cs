using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorScript : MonoBehaviour
{
    public GameObject[] availableRooms;
    public List<GameObject> currentRooms;

    float screenWidthInPoints;
    const string floor = "Floor";

    private void Start()
    {
        float height = 2.0f * Camera.main.orthographicSize;
        screenWidthInPoints = height * Camera.main.aspect;
    }

    private void FixedUpdate()
    {
        GenerateRoomIfRequired();
    }

    private void AddRoom(float farthestRoomEndX)
    {
        int randomRoonIndex = Random.Range(0, availableRooms.Length);

        GameObject room = Instantiate(availableRooms[randomRoonIndex]);

        float roomWidth = room.transform.Find(floor).localScale.x;
        float roomCenter = farthestRoomEndX + roomWidth / 2;

        room.transform.position = new Vector3(roomCenter, 0, 0);

        currentRooms.Add(room);
    }

    private void GenerateRoomIfRequired()
    {
        List<GameObject> roomsToRemove = new List<GameObject>();
        bool addRooms = true;
        float playerX = transform.position.x;
        float removeRoomX = playerX - screenWidthInPoints;
        float addRoomX = playerX + screenWidthInPoints;
        float farthestRoomEndX = 0.0f;

        foreach (var room in currentRooms)
        {
            float roomWidth = room.transform.Find(floor).localScale.x;
            float roomStartX = room.transform.position.x - roomWidth / 2;
            float roomEndX = roomStartX + roomWidth;

            if(roomStartX > addRoomX)
                addRooms = false;
            
            if(roomEndX < removeRoomX)
                roomsToRemove.Add(room);

            farthestRoomEndX = Mathf.Max(farthestRoomEndX, roomEndX);
        }

        foreach(var room in roomsToRemove)
        {
            currentRooms.Remove(room);
            Destroy(room);
        }

        if (addRooms)
            AddRoom(farthestRoomEndX);
    }
}
