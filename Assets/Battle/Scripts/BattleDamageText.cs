using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleDamageText : MonoBehaviour
{
    private RectTransform rect;
    private Text text;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        text = transform.GetChild(0).GetComponent<Text>();

        Invoke("Destroy", 0.5f);
    }

    public void OnDamaged(Vector2 characterPos, float damage)
    {
        text.text = damage.ToString("N0");

        Vector2 position;
        position = Camera.main.WorldToScreenPoint(characterPos);
        rect.position = new Vector2(position.x, position.y + 150f);
    }

    private void Destroy()
    {
        Destroy(rect.gameObject);
    }
}
