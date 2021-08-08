using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaperBehaviour : MonoBehaviour
{
    [SerializeField] TestScroll TS;
    [SerializeField] PaperManager paperManager;
    [SerializeField] GameManager gameManager;
    Rigidbody2D rb = null;
    public RectTransform rt = null;

    public RectTransform neighbor;

    private bool isInstantiated = false;
    private bool isTorn = false;
    public bool isInitial = false;

    private Vector3 lastDownPosition = Vector3.zero;

    [SerializeField] Sprite regular = default;
    [SerializeField] Sprite gold = default;

    private int value = 1;

    private void Awake()
    {
        rt = GetComponent<RectTransform>();
        TS = FindObjectOfType<TestScroll>();
        paperManager = FindObjectOfType<PaperManager>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnEnable()
    {
        Image image = GetComponent<Image>();

        if (Random.Range(1, 101) <= TS.goldPaperChance)
        {
            image.sprite = gold;
            value = 10;
        }
        else
        {
            image.sprite = regular;
            value = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*if(awakeFrames < 20 && !isInitial)
        {
            rt.localPosition = new Vector3(0, neighbor.localPosition.y + 100, 0);
            awakeFrames++;
        }*/

        Scroll();
        CheckPos();
        Fall();
    }

    private void Scroll()
    {
        if (!isTorn)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + TS.ScrollSpeed, transform.position.z);
        }
    }

    private void CheckPos()
    {
        if (rt.localPosition.y < -(Screen.height * 0.5) - 50) Destroy(this.gameObject);
        if (rt.localPosition.y < -50 && !isTorn)
        {
            isTorn = true;
            rb = gameObject.AddComponent<Rigidbody2D>();
            Vector2 moveVector = new Vector2(0, -100 + (300 * TS.ScrollSpeed));
            if (TS.holdingTP) moveVector *= 0.5f;
            rb.velocity = moveVector;
            gameManager.IncrementScore(value);
            // Debug.Log(gameObject.name + rb.velocity.y);
        }
        if (rt.localPosition.y < (Screen.height * 0.5) + 50 && !isInstantiated)
        {
            isInstantiated = true;
            paperManager.AddPaper();
        }
    }

    private void Fall()
    {
        if(rb != null)
            rb.AddForce(new Vector2(0, -100));
    }
}
