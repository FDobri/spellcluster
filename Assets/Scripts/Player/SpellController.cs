using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SpellController : MonoBehaviour {

    string spellName;
    int damage;

    public ParticleSystem particle;
    public GameObject afterEffect;
    PlayerSpells pSpells;

    GameObject afterEffectGO;

    private void Start()
    {
        pSpells = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSpells>();
        damage = pSpells.GetSpellDamage();
        StartCoroutine(DestroyObject(2f));
    }

    IEnumerator DestroyObject(float time)
    {
        yield return new WaitForSeconds(time);
        if (gameObject)
        {
            Destroy(gameObject);
            //Play particle upon destruction
        }
    }

    private void Update()
    {
        particle.transform.Rotate(Vector3.right * Time.deltaTime * 100);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.GetComponent<MobStats>())
        {
            collision.transform.GetComponent<MobStats>().TakeDamage(damage);
        }
        afterEffectGO = Instantiate(afterEffect, transform.position, transform.rotation);
        Destroy(afterEffectGO, 4f);
        Destroy(gameObject);
    }
}
