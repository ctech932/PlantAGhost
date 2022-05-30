using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [HideInInspector] public bool canGrabGhosts { get; private set; }

    [SerializeField] private GameObject ghost1, ghost2, ghost3;
    private SnappableObject snappableGhost1, snappableGhost2, snappableGhost3;
    private bool resultGhost1, resultGhost2, resultGhost3;

    [SerializeField] private GameObject wallClosed, wallOpen;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip wallOpensSound;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        wallClosed.SetActive(true);
        wallOpen.SetActive(false);

        snappableGhost1 = ghost1.GetComponent<SnappableObject>();
        snappableGhost2 = ghost2.GetComponent<SnappableObject>();
        snappableGhost3 = ghost3.GetComponent<SnappableObject>();

        canGrabGhosts = true;

    }

    // Update is called once per frame
    void Update()
    {
        resultGhost1 = snappableGhost1.ghostMatchPlanter;
        resultGhost2 = snappableGhost2.ghostMatchPlanter;
        resultGhost3 = snappableGhost3.ghostMatchPlanter;

        if (resultGhost1 && resultGhost2 && resultGhost3)
        {
            // GAME SOLVED
            audioSource.PlayOneShot(wallOpensSound, 0.4f);
            wallOpen.SetActive(true);
            wallClosed.SetActive(false);            
            canGrabGhosts = false;
        }
    }
}
