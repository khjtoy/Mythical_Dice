using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetNumber : MonoBehaviour
{
    [SerializeField]
    private List<Sprite> number;

    private SpriteRenderer spriteRenderer;

    private int index = 0;

    public bool isSurple = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        index = 0;
    }

    public IEnumerator SurpleNumber()
    {
        while (isSurple)
        {
            if (index == number.Count) index = 0;

            spriteRenderer.sprite = number[index];
            index++;
            yield return new WaitForSeconds(0.01f);
        }
    }

    public void SettingNumber(int _index)
    {
        spriteRenderer.sprite = number[_index];
    }
}
