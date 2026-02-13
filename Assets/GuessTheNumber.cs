using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GuessTheNumber : MonoBehaviour
{
    [SerializeField] private TMP_Text headerText;
    [SerializeField] private TMP_InputField guessInputField;
    [SerializeField] private Button submitButton;
    [SerializeField] private Button resetButton;

    [SerializeField] private int maxAttempts = 3;
    [SerializeField] private int minNumber = 1;
    [SerializeField] private int maxNumber = 10;

    private int targetNumber;
    private int remainingAttempts;
    private bool gameOver;

    private void Start()
    {
        GameSetup();
    }

    //Reset button is pressed
    public void GameSetup()
    {
        targetNumber = Random.Range(minNumber, maxNumber + 1);
        remainingAttempts = maxAttempts;
        gameOver = false;

        headerText.text = string.Format("Guess a number between {0} and {1}. Attempts left: {2}",
                                        minNumber, maxNumber, remainingAttempts);

        guessInputField.text = "";
        guessInputField.interactable = true;
        submitButton.interactable = true;
    }

    //submit button is pressed
    public void SubmitGuess()
    {
        if (gameOver)
            return;

        if (string.IsNullOrWhiteSpace(guessInputField.text))
        {
            headerText.text = "Please enter a number!";
            return;
        }

        int playerGuess;

        if (!int.TryParse(guessInputField.text, out playerGuess))
        {
            headerText.text = "Invalid input! Numbers only.";
            guessInputField.text = "";
            return;
        }

        remainingAttempts--;

        if (playerGuess == targetNumber)
        {
            headerText.text = string.Format("You WIN! The number was {0}!", targetNumber);
            EndGame();
        }
        else if (remainingAttempts > 0)
        {
            headerText.text = string.Format("Wrong guess! Attempts left: {0}", remainingAttempts);
        }
        else
        {
            headerText.text = string.Format("Game Over! The number was {0}.", targetNumber);
            EndGame();
        }

        guessInputField.text = "";
    }

    private void EndGame()
    {
        gameOver = true;
        guessInputField.interactable = false;
        submitButton.interactable = false;
    }
}
