using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    private SpriteRenderer sr;

    [SerializeField] private ItemData itemData;


    private void Start()
    {
        sr =GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("pick" + itemData.name);
        Destroy(gameObject);
    }
}
