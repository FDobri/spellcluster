using UnityEngine;

public class SpellHomingMissile : MonoBehaviour {

    string spellName;
    PlayerTargets playerTargets;
    GameObject player;
    GameObject target;
    int travellingSpeed = 10;

    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        playerTargets = player.GetComponent<PlayerTargets>();
        target = playerTargets.GetTarget();
	}

    void Update () {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, travellingSpeed * Time.fixedDeltaTime);
        transform.LookAt(target.transform);
	}
}
