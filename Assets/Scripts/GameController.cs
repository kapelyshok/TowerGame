using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    CubePos CurrentCube = new CubePos(0,1,0);
    public float ChangeSpeed = 1f;
    public Transform cubeToPlace;
    public GameObject cubeToCreate, allCubes;
    public Rigidbody allCubesRB;
    private Coroutine cor;
    private bool lose;
    bool gameStarted = false;
    public GameObject fingerImage;
    List<Vector3> allBusyPositions = new List<Vector3>
    {
        new Vector3(0,0,0),
        new Vector3(1,0,0),
        new Vector3(-1,0,0),
        new Vector3(0,0,1),
        new Vector3(0,0,-1),
        new Vector3(1,0,1),
        new Vector3(-1,0,1),
        new Vector3(-1,0,-1),
        new Vector3(1,0,-1),
        new Vector3(0,1,0)
    };
    // Start is called before the first frame update
    void Start()
    {
        gameStarted = false;
        allCubesRB = allCubes.GetComponent<Rigidbody>();
        cor=StartCoroutine(ShowCubeToPlace());
    }
    IEnumerator ShowCubeToPlace()
    {
        while(true)
        {
            SpawnPositions();
            yield return new WaitForSeconds(ChangeSpeed);
        }
    }

    void SpawnPositions()
    {
        List<Vector3> ProbablyPlaces = new List<Vector3>();
        if (IsPositionFree( new Vector3(CurrentCube.getPos().x + 1, CurrentCube.getPos().y, CurrentCube.getPos().z)) && CurrentCube.getPos().x + 1 != cubeToPlace.position.x)
            ProbablyPlaces.Add(new Vector3(CurrentCube.getPos().x + 1, CurrentCube.getPos().y, CurrentCube.getPos().z));
        if (IsPositionFree(new Vector3(CurrentCube.getPos().x - 1, CurrentCube.getPos().y, CurrentCube.getPos().z)) && CurrentCube.getPos().x - 1 != cubeToPlace.position.x)
            ProbablyPlaces.Add(new Vector3(CurrentCube.getPos().x - 1, CurrentCube.getPos().y, CurrentCube.getPos().z));
        if (IsPositionFree(new Vector3(CurrentCube.getPos().x, CurrentCube.getPos().y+1, CurrentCube.getPos().z)) && CurrentCube.getPos().y + 1 != cubeToPlace.position.y)
            ProbablyPlaces.Add(new Vector3(CurrentCube.getPos().x, CurrentCube.getPos().y+1, CurrentCube.getPos().z));
        if (IsPositionFree(new Vector3(CurrentCube.getPos().x, CurrentCube.getPos().y-1, CurrentCube.getPos().z)) && CurrentCube.getPos().y - 1 != cubeToPlace.position.y)
            ProbablyPlaces.Add(new Vector3(CurrentCube.getPos().x, CurrentCube.getPos().y-1, CurrentCube.getPos().z));
        if (IsPositionFree(new Vector3(CurrentCube.getPos().x, CurrentCube.getPos().y, CurrentCube.getPos().z+1)) && CurrentCube.getPos().z + 1 != cubeToPlace.position.z)
            ProbablyPlaces.Add(new Vector3(CurrentCube.getPos().x, CurrentCube.getPos().y, CurrentCube.getPos().z+1));
        if (IsPositionFree(new Vector3(CurrentCube.getPos().x, CurrentCube.getPos().y, CurrentCube.getPos().z-1)) && CurrentCube.getPos().z - 1 != cubeToPlace.position.z)
            ProbablyPlaces.Add(new Vector3(CurrentCube.getPos().x, CurrentCube.getPos().y, CurrentCube.getPos().z-1));
       
        cubeToPlace.position = ProbablyPlaces[UnityEngine.Random.Range(0, ProbablyPlaces.Count)];
    }

    bool IsPositionFree(Vector3 PosToCheck)
    {
        if (PosToCheck.y < 1) return false;
        foreach(Vector3 pos in allBusyPositions)
        {
            if (pos.x == PosToCheck.x && pos.y == PosToCheck.y && pos.z == PosToCheck.z) return false;
        }
        return true;
    }

    // Update is called once per frame
    void Update()
    {
        if((Input.GetMouseButtonDown(0) || Input.touchCount>0) && cubeToPlace!=null)
        {
#if !UNITY_EDITOR
if(Input.GetTouch(0).phase != TouchPhase.Began)return;
#endif
            if(!gameStarted)
            {
                gameStarted = true;
                fingerImage.SetActive(false);
            }
            GameObject newCube = Instantiate(cubeToCreate, cubeToPlace.position, Quaternion.identity) as GameObject;
            newCube.transform.SetParent(allCubes.transform);
            CurrentCube.SetPos(cubeToPlace.position);
            allBusyPositions.Add(CurrentCube.getPos());
            allCubesRB.isKinematic = true;
            allCubesRB.isKinematic = false;
            SpawnPositions();
        }
        if(!lose&&allCubesRB.velocity.magnitude>0.1f )
        {
            lose = true;
            Destroy(cubeToPlace.gameObject);
            StopCoroutine(cor);
        }
    }
}

struct CubePos
{
    private Vector3 pos;

    public CubePos(int x,int y,int z)
    {
        this.pos.x = x;
        this.pos.y = y;
        this.pos.z = z;
    }
    public Vector3 getPos()
    {
        return pos;
    }
    public void SetPos(Vector3 vec)
    {
        this.pos.x = vec.x;
        this.pos.y = vec.y;
        this.pos.z = vec.z;
    }
}