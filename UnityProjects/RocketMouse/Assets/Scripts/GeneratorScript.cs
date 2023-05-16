using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorScript : MonoBehaviour
{
    // �߰��� �� �������� �����ϴ� �迭
    public GameObject[] availableRooms;
    // ���� ���ӿ� �߰��Ǿ� �ִ� �� ������Ʈ���� ������ ����Ʈ
    public List<GameObject> currentRooms;

    // ȭ���� ���� ����
    float screenWidthInPoints;
    // �ٴڿ�����Ʈ�� �̸�
    const string floor = "Floor";

    // �߰��� ������, ���� �� �������� �����ϴ� �迭
    public GameObject[] availableObjects;
    // ���� ���ӿ� �߰��Ǿ� �ִ� ������Ʈ���� ������ ����Ʈ
    public List<GameObject> objects;
    // ������Ʈ �� ���� ������ �ּ�, �ִ밪
    public float objectsMinDistance = 5.0f;
    public float objectsMaxDistance = 10.0f;
    // ������Ʈ ���� �� Y�� ����
    public float objectsMinY = -1.4f;
    public float objectsMaxY = 1.4f;
    // ������Ʈ ���� �� ȸ�� �� ����
    public float objectsMinRotation = -45.0f;
    public float objectsMaxRotation = 45.0f;

    private void Start()
    {
        // ȭ���� ���α��̸� ��� (6.4)
        float height = 2.0f * Camera.main.orthographicSize;
        // ȭ���� ���α��̿� ȭ������� �̿��ؼ� ���α��̸� ���
        screenWidthInPoints = height * Camera.main.aspect;
    }

    private void FixedUpdate()
    {
        GenerateRoomIfRequired();
        GenerateObjectsIfRequired();
    }

    // �� ������ ��ġ�� �޾� ���� �����ϴ� �޼ҵ�
    private void AddRoom(float farthestRoomEndX)
    {
        // �������� ������ ���� �ε����� ����
        int randomRoomIndex = Random.Range(0, availableRooms.Length);
        // �� ������Ʈ�� �����ϰ� ������ ������Ʈ�� room������ ����
        GameObject room = Instantiate(availableRooms[randomRoomIndex]);
        // ������ ���� ����ũ�⸦ ������
        float roomWidth = room.transform.Find(floor).localScale.x;
        // ������ ���� �߽� ��ġ�� ����
        float roomCenter = farthestRoomEndX + roomWidth / 2;
        // ���� ��ġ������ ���� ��ġ�� ����
        room.transform.position = new Vector3(roomCenter, 0, 0);
        // ���� ������ �� ��� ����Ʈ�� ���� ������ ���� �߰�
        currentRooms.Add(room);
    }

    // ���� �߰��� �ʿ䰡 �ִٸ� ���� �߰��ϰ�, ������ ���� ������ ���� �������ִ� �޼ҵ�
    private void GenerateRoomIfRequired()
    {
        // ������ ����� �����ϴ� ����Ʈ
        List<GameObject> roomsToRemove = new List<GameObject>();
        // �̹� �����ӿ� ���� �߰��� ���ΰ�
        bool addRooms = true;
        // �÷��̾��� x�� ��ġ
        float playerX = transform.position.x;
        // ������ ���� ���� ��ġ
        float removeRoomX = playerX - screenWidthInPoints;
        // �߰��� ���� ���� ��ġ
        float addRoomX = playerX + screenWidthInPoints;
        // ���� �����ʿ� ��ġ�� ���� ������ �� ��ġ
        float farthestRoomEndX = 0.0f;

        // ���� ���ӿ� �߰��Ǿ� �ִ� ����� �ϳ��� ó��
        foreach (GameObject room in currentRooms)
        {
            // room �� ������Ʈ�� ����ũ�⸦ �ٴڿ�����Ʈ�� �̿��ؼ� ������
            float roomWidth = room.transform.Find(floor).localScale.x;
            // ����ġ���� �氡��ũ���� ���� ���� �� ���� ���� ��ġ�� ���
            float roomStartX = room.transform.position.x - roomWidth / 2;
            // ���� ���� ��ġ���� �氡��ũ�⸦ ���� ������ ��ġ�� ���
            float roomEndX = roomStartX + roomWidth;

            // �� ���� ���� ��ġ���� �����ʿ� ���� �ϳ��� ������ �� ����X
            if (roomStartX > addRoomX)
                addRooms = false;

            // �� ���� ���� ��ġ���� ���ʿ� ��ġ�� ���� ���� ��� ����Ʈ�� �߰�
            if (roomEndX < removeRoomX)
                roomsToRemove.Add(room);

            // ���� �����ʿ� ��ġ�� ���� ������ ��ġ�� �ִ밪���� ����
            farthestRoomEndX = Mathf.Max(farthestRoomEndX, roomEndX);
        }

        // ���� ��� ����Ʈ�� �ִ� ����� ����
        foreach (var room in roomsToRemove)
        {
            // ���� �� ��Ͽ��� ����
            currentRooms.Remove(room);
            // ���� ���ӿ�����Ʈ ����
            Destroy(room);
        }
        
        // ���� �����ؾ� �Ѵٸ� ���� �����ϴ� �޼ҵ� ȣ��
        if (addRooms)
            AddRoom(farthestRoomEndX);
    }

    private void AddObject(float lastObjectX)
    {
        // �������� ������ ������Ʈ�� �ε��� ����
        int randomIndex = Random.Range(0, availableObjects.Length);
        // ������Ʈ�� �����ϰ� obj������ ������ ������Ʈ�� ����
        GameObject obj = Instantiate(availableObjects[randomIndex]);
        // ������Ʈ�� ��ġ�� ��ġ�� ���� �����ʿ� ��ġ�� ������Ʈ�κ��� ����
        float objectPositionX = 
            lastObjectX + Random.Range(objectsMinDistance, objectsMaxDistance);
        // ������Ʈ Y�� ��ġ�� ���� ����
        float randomY = Random.Range(objectsMinY, objectsMaxY);

        // ���� ��ġ������ ������Ʈ�� ��ġ�� ����
        obj.transform.position = new Vector3(objectPositionX, randomY, 0);

        // ȸ�� ���� �������� ����
        float rotation = Random.Range(objectsMinRotation, objectsMaxRotation);

        // ���� ȸ�������� ȸ�� ����
        obj.transform.rotation = Quaternion.Euler(Vector3.forward * rotation);
//        obj.transform.rotation = Quaternion.Euler(0, 0, rotation);
        // ���� �߰��� ������Ʈ ��Ͽ� ���� ������ ������Ʈ�� �߰�
        objects.Add(obj);
    }

    private void GenerateObjectsIfRequired()
    {
        // �ÿ��̾��� X�� ��ġ
        float playerX = transform.position.x;
        // ������ ������Ʈ�� ���� ��ġ
        float removeObjectX = playerX - screenWidthInPoints;
        // �߰��� ������Ʈ�� ���� ��ġ
        float addObjectX = playerX + screenWidthInPoints;
        // ���� �����ʿ� ��ġ�� ������Ʈ�� ��ġ
        float farthestObjectX = 0;

        // ������ ������Ʈ���� �����ϴ� ����Ʈ
        List<GameObject> objectsToRemove = new List<GameObject>();
        
        // ���� ���ӿ� �߰��Ǿ� �ִ� ������Ʈ���� �ϳ��� ó��
        foreach (var obj in objects)
        {
            // ������Ʈ�� X�� ��ġ�� ������
            float objX = obj.transform.position.x;

            // ���� �����ʿ� ��ġ�� ������Ʈ�� ��ġ�� �ִ밪���� ����
            farthestObjectX = Mathf.Max(farthestObjectX, objX);

            // ������Ʈ ���� ���� ��ġ���� ���ʿ� ��ġ�� ������Ʈ�� ���� ����Ʈ�� �߰�
            if (objX < removeObjectX)
                objectsToRemove.Add(obj);
        }

        // ���� ����Ʈ�� �ִ� ������Ʈ���� ����
        foreach (var obj in objectsToRemove)
        {
            // ���� ������Ʈ ��Ͽ��� ����
            objects.Remove(obj);
            // ���� ���ӿ�����Ʈ�� ����
            Destroy(obj);
        }

        // ������Ʈ�� �����ؾ� �Ѵٸ� ������Ʈ ���� �޼ҵ� ȣ��
        if (farthestObjectX < addObjectX)
            AddObject(farthestObjectX);
    }
}
