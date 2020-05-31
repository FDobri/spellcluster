using UnityEngine;
using UnityEngine.UI;

public class PlayerTargets : MonoBehaviour {
    GameObject target = null;

    [SerializeField]
    Text targetNameText;

    public GameObject GetTarget()
    {
        return target;
    }

	void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Mob" || hit.transform.tag == "Player")
                {
                    target = hit.transform.gameObject;
                    targetNameText.text = target.name;
                    Debug.Log("Target: " + target.name);
                }
                else
                {
                    target = null;
                    targetNameText.text = null;
                }
            }
        }
	}
}
