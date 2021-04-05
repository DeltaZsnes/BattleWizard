using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    private enum Mode
    {
        Move,
        Mana,
        Target,
    }

    public GameObject manaPrefab;
    public GameObject starPrefab;
    public ContactFilter2D contactFilter;
    
    private Mode mode = Mode.Move;
    private GameObject starObject;
    private List<GameObject> manaList = new List<GameObject>();
    private List<GameObject> hillConstellation = new List<GameObject>();

    
    
    // Start is called before the first frame update
    void Start()
    {
        var star1 = Instantiate(starPrefab, new Vector3(-1, 0, 0), this.transform.rotation);
        var star2 = Instantiate(starPrefab, new Vector3(0, +1, 0), this.transform.rotation);
        var star3 = Instantiate(starPrefab, new Vector3(+1, 0, 0), this.transform.rotation);

        hillConstellation.Add(star1);
        hillConstellation.Add(star2);
        hillConstellation.Add(star3);
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

            mode = Mode.Mana;
        }

        if (Input.GetMouseButtonUp(0))
        {
            mode = Mode.Move;

            if(Constellation(hillConstellation))
            {
                Debug.Log("HillSpell");
            }

            foreach(var manaObject in manaList)
            {
                Destroy(manaObject);
            }
        }

        if(mode == Mode.Mana)
        {
            var p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var manaObject = Instantiate(manaPrefab, new Vector3(p.x, p.y, 0), this.transform.rotation);
            manaList.Add(manaObject);
        }
    }

    bool Constellation(List<GameObject> constellation)
    {
        foreach(var star in constellation)
        {
            var colliders = new Collider2D[1];
            var overlaps = star.GetComponent<Collider2D>().OverlapCollider(contactFilter, colliders);
            
            if(overlaps == 0)
            {
                return false;
            }
        }

        return true;
    }
}
