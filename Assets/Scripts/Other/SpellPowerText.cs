using UnityEngine;
using UnityEngine.UI;

public class SpellPowerText : MonoBehaviour
{

    public static int spDmg;
    Text spText;

    void Awake()
    {
        spText = GetComponent<Text>();
    }

    void Update()
    {
        spText.text = "Spellpower: " + spDmg.ToString();
    }
}
