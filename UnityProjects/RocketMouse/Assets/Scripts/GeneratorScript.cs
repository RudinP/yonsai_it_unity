using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorScript : MonoBehaviour
{
    // 추가될 방 프리팹을 저장하는 배열
    public GameObject[] availableRooms;
    // 현재 게임에 추가되어 있는 방 오브젝트들을 저장한 리스트
    public List<GameObject> currentRooms;

    // 화면의 가로 길이
    float screenWidthInPoints;
    // 바닥오브젝트의 이름
    const string floor = "Floor";

    // 추가될 레이저, 동전 등 프리팹을 저장하는 배열
    public GameObject[] availableObjects;
    // 현재 게임에 추가되어 있는 오브젝트들을 저장한 리스트
    public List<GameObject> objects;
    // 오브젝트 간 생성 간격의 최소, 최대값
    public float objectsMinDistance = 5.0f;
    public float objectsMaxDistance = 10.0f;
    // 오브젝트 생성 시 Y축 범위
    public float objectsMinY = -1.4f;
    public float objectsMaxY = 1.4f;
    // 오브젝트 생성 시 회전 값 범위
    public float objectsMinRotation = -45.0f;
    public float objectsMaxRotation = 45.0f;

    private void Start()
    {
        // 화면의 세로길이를 계산 (6.4)
        float height = 2.0f * Camera.main.orthographicSize;
        // 화면의 세로길이와 화면비율을 이용해서 가로길이를 계산
        screenWidthInPoints = height * Camera.main.aspect;
    }

    private void FixedUpdate()
    {
        GenerateRoomIfRequired();
        GenerateObjectsIfRequired();
    }

    // 방 생성할 위치를 받아 방을 생성하는 메소드
    private void AddRoom(float farthestRoomEndX)
    {
        // 랜덤으로 생성할 방의 인덱스를 구함
        int randomRoomIndex = Random.Range(0, availableRooms.Length);
        // 방 오브젝트를 생성하고 생성된 오브젝트를 room변수에 저장
        GameObject room = Instantiate(availableRooms[randomRoomIndex]);
        // 생성된 방의 가로크기를 가져옴
        float roomWidth = room.transform.Find(floor).localScale.x;
        // 생성된 방의 중심 위치를 구함
        float roomCenter = farthestRoomEndX + roomWidth / 2;
        // 구한 위치값으로 방의 위치를 변경
        room.transform.position = new Vector3(roomCenter, 0, 0);
        // 현재 생성된 방 목록 리스트에 새로 생성한 방을 추가
        currentRooms.Add(room);
    }

    // 방을 추가할 필요가 있다면 방을 추가하고, 제거할 방이 있으면 방을 제거해주는 메소드
    private void GenerateRoomIfRequired()
    {
        // 삭제할 방들을 저장하는 리스트
        List<GameObject> roomsToRemove = new List<GameObject>();
        // 이번 프레임에 방을 추가할 것인가
        bool addRooms = true;
        // 플레이어의 x축 위치
        float playerX = transform.position.x;
        // 제거할 방의 기준 위치
        float removeRoomX = playerX - screenWidthInPoints;
        // 추가할 방의 기준 위치
        float addRoomX = playerX + screenWidthInPoints;
        // 가장 오른쪽에 위치한 방의 오른쪽 끝 위치
        float farthestRoomEndX = 0.0f;

        // 현재 게임에 추가되어 있는 방들을 하나씩 처리
        foreach (GameObject room in currentRooms)
        {
            // room 방 오브젝트의 가로크기를 바닥오브젝트를 이용해서 가져옴
            float roomWidth = room.transform.Find(floor).localScale.x;
            // 방위치에서 방가로크기의 반을 빼서 그 방의 왼쪽 위치를 계산
            float roomStartX = room.transform.position.x - roomWidth / 2;
            // 방의 왼쪽 위치에서 방가로크기를 더해 오른쪽 위치를 계산
            float roomEndX = roomStartX + roomWidth;

            // 방 생성 기준 위치보다 오른쪽에 방이 하나라도 있으면 방 생성X
            if (roomStartX > addRoomX)
                addRooms = false;

            // 방 제거 기준 위치보다 왼쪽에 위치한 방은 제거 목록 리스트에 추가
            if (roomEndX < removeRoomX)
                roomsToRemove.Add(room);

            // 가장 오른쪽에 위치한 방의 오른쪽 위치를 최대값으로 구함
            farthestRoomEndX = Mathf.Max(farthestRoomEndX, roomEndX);
        }

        // 제거 목록 리스트에 있는 방들을 제거
        foreach (var room in roomsToRemove)
        {
            // 현재 방 목록에서 제거
            currentRooms.Remove(room);
            // 실제 게임오브젝트 제거
            Destroy(room);
        }
        
        // 방을 생성해야 한다면 방을 생성하는 메소드 호출
        if (addRooms)
            AddRoom(farthestRoomEndX);
    }

    private void AddObject(float lastObjectX)
    {
        // 랜덤으로 생성할 오브젝트의 인덱스 구함
        int randomIndex = Random.Range(0, availableObjects.Length);
        // 오브젝트를 생성하고 obj변수에 생성된 오브젝트를 저장
        GameObject obj = Instantiate(availableObjects[randomIndex]);
        // 오브젝트를 배치할 위치를 가장 오른쪽에 위치한 오브젝트로부터 구함
        float objectPositionX = 
            lastObjectX + Random.Range(objectsMinDistance, objectsMaxDistance);
        // 오브젝트 Y축 위치를 랜덤 구함
        float randomY = Random.Range(objectsMinY, objectsMaxY);

        // 구한 위치값으로 오브젝트의 위치를 변경
        obj.transform.position = new Vector3(objectPositionX, randomY, 0);

        // 회전 값도 랜덤으로 구함
        float rotation = Random.Range(objectsMinRotation, objectsMaxRotation);

        // 구한 회전값으로 회전 적용
        obj.transform.rotation = Quaternion.Euler(Vector3.forward * rotation);
//        obj.transform.rotation = Quaternion.Euler(0, 0, rotation);
        // 현재 추가된 오브젝트 목록에 새로 생성한 오브젝트를 추가
        objects.Add(obj);
    }

    private void GenerateObjectsIfRequired()
    {
        // 플에이어의 X축 위치
        float playerX = transform.position.x;
        // 제거할 오브젝트의 기준 위치
        float removeObjectX = playerX - screenWidthInPoints;
        // 추가할 오브젝트의 기준 위치
        float addObjectX = playerX + screenWidthInPoints;
        // 가장 오른쪽에 위치한 오브젝트의 위치
        float farthestObjectX = 0;

        // 삭제할 오브젝트들을 저장하는 리스트
        List<GameObject> objectsToRemove = new List<GameObject>();
        
        // 현재 게임에 추가되어 있는 오브젝트들을 하나씩 처리
        foreach (var obj in objects)
        {
            // 오브젝트의 X축 위치를 가져옴
            float objX = obj.transform.position.x;

            // 가장 오른쪽에 위치한 오브젝트의 위치를 최대값으로 구함
            farthestObjectX = Mathf.Max(farthestObjectX, objX);

            // 오브젝트 제거 기준 위치보다 왼쪽에 위치한 오브젝트는 제거 리스트에 추가
            if (objX < removeObjectX)
                objectsToRemove.Add(obj);
        }

        // 제거 리스트에 있는 오브젝트들을 제거
        foreach (var obj in objectsToRemove)
        {
            // 현재 오브젝트 목록에서 제거
            objects.Remove(obj);
            // 실제 게임오브젝트도 제거
            Destroy(obj);
        }

        // 오브젝트를 생성해야 한다면 오브젝트 생성 메소드 호출
        if (farthestObjectX < addObjectX)
            AddObject(farthestObjectX);
    }
}
