using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RuneController : MonoBehaviour
{
    [SerializeField]
    GameObject floatingText;
    GameObject flTxtObj;

    GameObject player;
    bool isActive;

    public delegate void IncreaserDelegate(int amnt);

    public void IncreaseMaxHp(int amount)
    {
        player.GetComponent<PlayerStats>().IncreaseMaxHealth(amount);
    }

    public void IncreaseDmg(int amount)
    {
        player.GetComponent<PlayerSpells>().IncreaseSpellDamage(amount);
    }


    public void IncreasePlayerStat(int amount, IncreaserDelegate increaseOption)
    {
        var kWord = "";
        increaseOption(amount);
        flTxtObj = Instantiate(floatingText);
        flTxtObj.transform.SetParent(FindObjectOfType<Canvas>().transform);
        flTxtObj.GetComponent<RectTransform>().position = new Vector3(Screen.width / Random.Range(2.5f,3.5f), Screen.height / Random.Range(1.5f, 2.5f), 0);
        if (transform.name.Contains("Rune1"))
        {
            flTxtObj.GetComponent<Text>().color = Color.green;
            kWord = " hp!";
        }
        else
        {
            flTxtObj.GetComponent<Text>().color = Color.yellow;
            kWord = " dmg!";
        }
        flTxtObj.GetComponent<Text>().text = "+ " + amount.ToString() + kWord;
    }

    IEnumerator MakeActive()
    {
        yield return new WaitForSeconds(2.5f);
        isActive = true;
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(MakeActive());
    }

    void Update()
    {
        transform.Rotate(transform.up * Time.deltaTime * 150);
        if (Vector3.Distance(player.transform.position, gameObject.transform.position) < 2f && isActive)
        {
            if (gameObject.transform.name.Contains("Rune1"))
            {
                IncreasePlayerStat(Random.Range(12, 18), IncreaseMaxHp);
                Destroy(gameObject);
            }

            if (gameObject.transform.name.Contains("Rune2"))
            {
                IncreasePlayerStat(Random.Range(12, 18), IncreaseDmg);
                Destroy(gameObject);
            }
        }
    }
}
