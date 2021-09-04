using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PaperManager : MonoBehaviour
{
    [SerializeField] TestScroll testScroll;
    [SerializeField] List<PaperBehaviour> toiletPapers;
    [SerializeField] GameObject prefab;
    [SerializeField] Transform parent;
    [SerializeField] int initialPapers;

    private void Awake()
    {
        for(int i = 0; i < initialPapers; i++)
        {
            GameObject go = Instantiate(prefab);
            go.transform.SetParent(parent, false);
            go.transform.localScale = Vector3.one;
            RectTransform rt = go.GetComponent<RectTransform>();
            rt.localPosition = new Vector3(0, 140 + 233*i, 0);
            PaperBehaviour cur = go.GetComponent<PaperBehaviour>();
            cur.isInitial = true;
            toiletPapers.Add(go.GetComponent<PaperBehaviour>());
        }
    }

    public void AddPaper()
    {
        GameObject go = Instantiate(prefab);
        go.transform.SetParent(parent, false);
        go.transform.localScale = Vector3.one;

        RectTransform rt = toiletPapers.Last().rt;
        PaperBehaviour cur = go.GetComponent<PaperBehaviour>();

        cur.neighbor = rt;
        Vector3 pos = rt.localPosition;

        go.GetComponent<RectTransform>().localPosition = new Vector3(0, pos.y + 140 + testScroll.ScrollSpeed, 0);

        toiletPapers.Add(cur);
    }
}
