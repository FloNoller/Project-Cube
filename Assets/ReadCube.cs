using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadCube : MonoBehaviour
{
    public Transform RayU;
    public Transform RayL;
    public Transform RayR;
    public Transform RayF;
    public Transform RayB;
    public Transform RayD;
    public GameObject emptyGO;

    private List<GameObject> fRays = new List<GameObject>();
    private List<GameObject> uRays = new List<GameObject>();
    private List<GameObject> bRays = new List<GameObject>();
    private List<GameObject> dRays = new List<GameObject>();
    private List<GameObject> lRays = new List<GameObject>();
    private List<GameObject> rRays = new List<GameObject>();

    private int layerMask = 1 << 8;
    CubeState cubeState;
    CubeMap cubeMap;
    // Start is called before the first frame update
    void Start()
    {

        SetRayTransforms();

        cubeState = FindObjectOfType<CubeState>();
        cubeMap = FindObjectOfType<CubeMap>();
        ReadState();
        CubeState.started = true;


    }
    // Update is called once per frame
    void Update()
    {

    }

    public void ReadState()
    {
        cubeState = FindObjectOfType<CubeState>();
        cubeMap = FindObjectOfType<CubeMap>();

        cubeState.up = ReadFace(uRays, RayU);
        cubeState.down = ReadFace(dRays, RayD);
        cubeState.left = ReadFace(lRays, RayL);
        cubeState.right = ReadFace(rRays, RayR);
        cubeState.back = ReadFace(bRays, RayB);
        cubeState.front = ReadFace(fRays, RayF);

        cubeMap.Set();
    }

    void SetRayTransforms()
    {
        uRays = BuildRays(RayU, new Vector3(90, 90, 0));
        dRays = BuildRays(RayD, new Vector3(270, 90, 0));
        lRays = BuildRays(RayL, new Vector3(0, 180, 0));
        rRays = BuildRays(RayR, new Vector3(0, 0, 0));
        fRays = BuildRays(RayF, new Vector3(0, 90, 0));
        bRays = BuildRays(RayB, new Vector3(0, 270, 0));

    }

    List<GameObject> BuildRays(Transform rayTransform, Vector3 direction)
    {
        int rayCount = 0;
        List<GameObject> rays = new List<GameObject>();

        for (int y = 1; y > -2; y--)
        {
            for (int x = -1; x < 2; x++)
            {
                Vector3 startPos = new Vector3(rayTransform.localPosition.x + x,
                    rayTransform.localPosition.y + y, rayTransform.localPosition.z);
                GameObject rayStart = Instantiate(emptyGO, startPos, Quaternion.identity, rayTransform);
                rayStart.name = rayCount.ToString();
                rays.Add(rayStart);
                rayCount++;
            }
        }
        rayTransform.localRotation = Quaternion.Euler(direction);
        return rays;
    }

    public List<GameObject> ReadFace(List<GameObject> rayStarts, Transform rayTransform)
    {
        List<GameObject> facesHit = new List<GameObject>();

        foreach (GameObject rayStart in rayStarts)
        {
            Vector3 ray = rayStart.transform.position;
            RaycastHit hit;

            if (Physics.Raycast(ray, rayTransform.forward, out hit, Mathf.Infinity, layerMask))
            {
                Debug.DrawRay(ray, rayTransform.forward * hit.distance, Color.yellow);
                facesHit.Add(hit.collider.gameObject);
                //print(hit.collider.gameObject.name);
            }
            else
            {
                Debug.DrawRay(ray, rayTransform.forward * 1000, Color.green);
            }
        }

        return facesHit;
        cubeMap.Set();
    }
}
