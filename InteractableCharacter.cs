using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class InteractableCharacter : MonoBehaviour
{
    public float interactionRadius = 2.0f;
    public string message = "Greetings, traveler! You seem lost in these lands. If you need guidance, fear not—I am here to help.";
    public string duelMessage = "If you'll duel me in a battle of knowledge and win, I will give you the key to the lost scroll. Do you accept? Press 'Y' for Yes, 'N' for No.";
    public float typingSpeed = 0.05f;
    public TMP_Text responseText;
    private Transform player;
    private bool isDisplayingText = false;
    private bool isWaitingForInput = false;
    private bool isWaitingForChoice = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (responseText != null)
        {
            responseText.text = ""; // Ensure the text starts empty
            SetTextAlpha(255); // Ensure text starts fully visible
        }

        // Initialize DuelWon to 0 if not already set
        if (!PlayerPrefs.HasKey("DuelWon"))
        {
            PlayerPrefs.SetInt("DuelWon", 0);
            PlayerPrefs.Save();
        }
    }

    private void Update()
    {
        if (Vector2.Distance(player.position, transform.position) <= interactionRadius)
        {
            if (Input.GetKeyDown(KeyCode.E) && !isDisplayingText && !isWaitingForChoice)
            {
                DisplayMessage(); // Show the initial dialog with invitation to the duel
            }
        }

        if (isWaitingForChoice)
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                AcceptDuel();
            }
            else if (Input.GetKeyDown(KeyCode.N))
            {
                DeclineDuel();
            }
        }

        if (isWaitingForInput && Input.anyKeyDown && !isWaitingForChoice)
        {
            ClearText();
        }
    }

    private void DisplayMessage()
    {
        if (responseText != null)
        {
            StopAllCoroutines();
            SetTextAlpha(255);
            StartCoroutine(TypeText(message));
        }
    }

    private IEnumerator TypeText(string message)
    {
        isDisplayingText = true;
        responseText.text = "";

        foreach (char letter in message.ToCharArray())
        {
            responseText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        if (message == this.message)
        {
            yield return new WaitForSeconds(3f);
            isDisplayingText = false;
            isWaitingForChoice = true;
            StartCoroutine(TypeText(duelMessage));
        }
        else
        {
            isWaitingForInput = true;
        }
    }

    private void AcceptDuel()
    {
        isWaitingForChoice = false;
        responseText.text = "";
        SceneManager.LoadScene(2); // Load the duel scene (build index 2)
    }

    private void DeclineDuel()
    {
        isWaitingForChoice = false;
        responseText.text = "";
    }

    private void ClearText()
    {
        isDisplayingText = false;
        SetTextAlpha(0);
        responseText.text = "";
        isWaitingForInput = false;
    }

    private void SetTextAlpha(float alpha)
    {
        Color color = responseText.color;
        color.a = alpha / 255f;
        responseText.color = color;
    }
}
