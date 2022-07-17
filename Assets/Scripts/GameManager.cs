using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private AudioClip matchClip;
    [SerializeField] private AudioSource musicPlayer;

    [SerializeField] private bool areCardsActive;
    public bool AreCardsActive { get { return areCardsActive; } }

    [SerializeField] private int score;
    [SerializeField] private int curScore;

    [SerializeField] private TMP_Text scoreText;

    [SerializeField] private Sprite[] sprites;
    private List<Sprite> doubleSprites = new List<Sprite>(); //уже удвоенное кол-во карт, будет перемешиваться

    private List<int> cardsID = new List<int>();

    [SerializeField] private List<Card> allCards = new List<Card>(); //карты

    //[SerializeField] private List<Transform> cardsPos = new List<Transform>(); //положения всех карт

    private List<Card> selectedCards = new List<Card>();

    private float waitingTime = .8f;

    private AudioSource audioSource;

    void Awake()
    {
        areCardsActive = true;
        audioSource = GetComponent<AudioSource>();
        DoubleCards();
    }

    void Start()
    {
        score = 2 * sprites.Length;
        curScore = 0;

        ChangeScore();

        SwapCard();
    }

    private void ChangeScore()
    {
        scoreText.text = curScore + "/" + score;
    }

    private void DoubleCards()
    {
        for (int i = 0; i < sprites.Length; i++)
        {
            doubleSprites.Add(sprites[i]);
            doubleSprites.Add(sprites[i]);

            cardsID.Add(i);
            cardsID.Add(i);
        }
    }

    public void CheckCards(Card newCard)
    {
        //если выделено более одной карты (текущая + новая), то можно сравнивать
        if (selectedCards.Count > 0)
        {
            StartCoroutine(CoroutineSOmeName());

            if (selectedCards[0].ID == newCard.ID && selectedCards[0].transform.position != newCard.transform.position)
            {
                //PlayClip(matchClip);
                musicPlayer.PlayOneShot(matchClip);
                StartCoroutine(SetCardsAsCorrect(selectedCards[0], newCard));
            }
            else
            {
                StartCoroutine(SetCardsAsUncorrect(selectedCards[0], newCard));
            }

            selectedCards.RemoveAt(0);
        }
        else
        {
            //выбрана первая карта, то добавляем ее в список выделенных карт
            selectedCards.Add(newCard);
        }
    }

    private IEnumerator CoroutineSOmeName()
    {
        areCardsActive = false;
        yield return new WaitForSeconds(waitingTime);
        areCardsActive = true;
    }

    //установить карты как НЕверные, то есть НЕсовпавшие, и закрыть их
    private IEnumerator SetCardsAsUncorrect(Card card1, Card card2)
    {
        yield return new WaitForSeconds(waitingTime);
        card1.HideCard();
        card2.HideCard();
    }

    //установить карты как верные, то есть совпавшие, и не закрывать их
    private IEnumerator SetCardsAsCorrect(Card card1, Card card2)
    {
        curScore += 2;
        ChangeScore();

        yield return new WaitForSeconds(waitingTime);

        card1.gameObject.SetActive(false);
        card2.gameObject.SetActive(false);

        card1.LeaveOpen();
        card2.LeaveOpen();
    }

    private void SwapCard()
    {
        for (int i = 0; i < doubleSprites.Count; i++)
        {
            int rand = Random.Range(0, doubleSprites.Count);

            Sprite tmpSpite = doubleSprites[i];
            doubleSprites[i] = doubleSprites[rand];
            doubleSprites[rand] = tmpSpite;

            int tmpID = cardsID[i];
            cardsID[i] = cardsID[rand];
            cardsID[rand] = tmpID;

        }

        //в соответствии с измененным списком расставляем карты на поле
        SetCardsImagesAndIDs();
    }

    private void SetCardsImagesAndIDs()
    {
        for (int i = 0; i < allCards.Count; i++)
        {
            allCards[i].Image.sprite = doubleSprites[i];
            allCards[i].ID = cardsID[i];
        }
    }

    public void Restart()
    {
        curScore = 0;
        ChangeScore();

        foreach (Card card in allCards)
        {
            card.gameObject.SetActive(true);
            card.RestartCard();
        }

        if (selectedCards.Count > 0)
            selectedCards.RemoveAt(0);

        SwapCard();
        areCardsActive = true;
    }

    private void PlayClip(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
