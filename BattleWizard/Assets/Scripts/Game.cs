using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject manaPrefab;
    public GameObject starPrefab;
    public ContactFilter2D contactFilter;
    bool spellCasting = false;
    private GameObject starObject;

    // Start is called before the first frame update
    void Start()
    {
        starObject = Instantiate(starPrefab, new Vector3(0, 0, 0), this.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Debug.Log("mouse down:" + Input.mousePosition);
            var hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            // Debug.Log(hit.point);
            // Debug.Log(hit.collider?.gameObject.transform.position);
            
            if(hit.collider != null)
            {
                Debug.Log("true:" + hit.point);
                Debug.Log(hit.collider.gameObject.tag);
            }

            spellCasting = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            spellCasting = false;
        }

        if(spellCasting)
        {
            var p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var manaObject = Instantiate(manaPrefab, new Vector3(p.x, p.y, 0), this.transform.rotation);
        }

        var colliders = new Collider2D[1];
        var overlaps = starObject.GetComponent<Collider2D>().OverlapCollider(contactFilter, colliders);
        Debug.Log(overlaps);
    }
}
