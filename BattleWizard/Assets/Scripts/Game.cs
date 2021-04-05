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
    public GameObject targetPrefab;
    public GameObject skeletonPrefab;
    public GameObject wizardPrefab;
    public ContactFilter2D contactFilter;
    
    private Mode mode = Mode.Move;
    private GameObject starObject;
    private List<GameObject> manaList = new List<GameObject>();
    private List<GameObject> targetList = new List<GameObject>();
    private List<GameObject> hillConstellation = new List<GameObject>();
    private List<GameObject> fireballConstellation = new List<GameObject>();
    
    // Start is called before the first frame update
    void Start()
    {
        var star1 = Instantiate(starPrefab, new Vector3(-1, 0, 0), this.transform.rotation);
        var star2 = Instantiate(starPrefab, new Vector3(0, +1, 0), this.transform.rotation);
        var star3 = Instantiate(starPrefab, new Vector3(+1, 0, 0), this.transform.rotation);
        var star4 = Instantiate(starPrefab, new Vector3(0, 0, 0), this.transform.rotation);
        var star5 = Instantiate(starPrefab, new Vector3(0, -1, 0), this.transform.rotation);

        hillConstellation.Add(star1);
        hillConstellation.Add(star2);
        hillConstellation.Add(star3);

        fireballConstellation.Add(star1);
        fireballConstellation.Add(star2);
        fireballConstellation.Add(star3);
        fireballConstellation.Add(star4);
        fireballConstellation.Add(star5);

        var wizardObject = Instantiate(wizardPrefab, new Vector3(0, -8, 0), this.transform.rotation);

        for(var i=0; i<10; i++)
        {
            var skeletonObject = Instantiate(skeletonPrefab, new Vector3(Random.value, Random.value + 10, 0), this.transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (mode == Mode.Move && Input.GetMouseButtonDown(0))
        {
            // Debug.Log("mouse down:" + Input.mousePosition);
            var hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            // Debug.Log(hit.point);
            // Debug.Log(hit.collider?.gameObject.transform.position);
            
            if(hit.collider != null)
            {
            }

            mode = Mode.Mana;
        }

        if (mode == Mode.Mana && Input.GetMouseButtonUp(0))
        {
            if(Constellation(fireballConstellation))
            {
                Debug.Log("FireballSpell");
                mode = Mode.Target;
            }
            else if(Constellation(hillConstellation))
            {
                Debug.Log("HillSpell");
                mode = Mode.Target;
            }
            else
            {
                mode = Mode.Move;
            }

            foreach(var manaObject in manaList)
            {
                Destroy(manaObject);
            }
        }

        if (mode == Mode.Target && Input.GetMouseButtonDown(0))
        {
            mode = Mode.Move;
            var p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var targetObject = Instantiate(targetPrefab, new Vector3(p.x, p.y, 0), this.transform.rotation);
            targetList.Add(targetObject);
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
