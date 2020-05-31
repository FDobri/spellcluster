using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMovement : MonoBehaviour {

    NavMeshAgent agent;

    Vector3 destination;

    public GameObject walkToward;

    [SerializeField]
    LayerMask movementMask;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        destination = gameObject.transform.position;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) || Input.GetMouseButton(1))
        {
            SetDestinationOnClick();
        }

        if (Input.GetMouseButtonDown(1))
        {
            GameObject goTemp = Instantiate(walkToward, destination + new Vector3(0f,0.5f, -0.5f), transform.rotation);
            Destroy(goTemp, 2f);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            agent.SetDestination(transform.position);
        }
    }

    public Vector3 GetDestination()
    {
        return destination;
    }

    void SetDestinationOnClick()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100, movementMask) && hit.transform.tag == "Walkable")
        {
            //Debug.Log(hit.transform.name);
            destination = hit.point;
            agent.SetDestination(destination);
        }
    }
}
