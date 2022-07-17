using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int ID
    {
        get { return id; }
        set { id = value; }
    }
    [SerializeField] private int id;

    public Image Image
    {
        get { return image; }
        set { image = value; }
    }
    private Image image;

    [SerializeField] private AudioClip flipSound;
    private AudioSource audioSource;

    private bool isCardOpened;

    private GameManager gameManager;
    private Animator animator;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        isCardOpened = false;
        animator = GetComponent<Animator>();
        image = transform.Find("OpenCard").GetComponent<Image>();
    }

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void ShowCard()
    {
        if (!isCardOpened && gameManager.AreCardsActive)
        {
            PlayClip(flipSound);
            isCardOpened = true;
            animator.SetTrigger("open");
            gameManager.CheckCards(this);
        }
    }

    public void HideCard()
    {
        PlayClip(flipSound);
        isCardOpened = false;
        animator.SetTrigger("close");
    }

    public void LeaveOpen()
    {
        //мб сыграть анимацию, типо ура! совпало!
    }

    public void RestartCard()
    {
        isCardOpened = false;
        animator.SetTrigger("close");
    }

    private void PlayClip(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
