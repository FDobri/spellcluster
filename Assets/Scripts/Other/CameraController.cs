using UnityEngine;

public class CameraController : MonoBehaviour
{

    GameObject player;
    Vector3 cameraOffset;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        SetupCamera();
    }

    // Update is called once per frame
    void Update()
    {
        if (!player)
        {
        return;
        }

        transform.position = player.transform.position + cameraOffset;

        if (Input.GetAxis("Mouse ScrollWheel") < 0f && transform.position.y < 25)
        {
            Zoom(2f,-1.35f);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0f && transform.position.y > 5)
        {
            Zoom(-2f,1.35f);
        }
    }

    void SetupCamera()
    {
        transform.rotation = Quaternion.Euler(60, 0, 0);
        cameraOffset = new Vector3(0, 23, -14.5f);
    }

    void Zoom(float yAmount, float zAmount)
    {
        cameraOffset.y += yAmount;
        cameraOffset.z += zAmount;
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit game");
    }


}
