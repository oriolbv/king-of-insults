using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayManager : ExtendedBehaviour
{
    public Transform OptionButtons;
    public GameObject ButtonPrefab;
    public GameObject InsultText;
    public GameObject PlayerText;
    public GameObject EnemyText;

    public GameObject PlayerLivesText;
    public GameObject EnemyLivesText;


    private GameplayState _gs;
    private InsultNode[] _insults;
    private int _insultIdx = -1;
    private int _answerIdx = -1;
    private int _firstTurn = -1;
    private int _roundWinner = -1;

    // Lives
    private int _playerLives = 3;
    private int _enemyLives = 3;


    private void Start()
    {
        int player = GetRandomPlayer();

        // Fill all the insults and answers to use during the duel
        _insults = InsultFiller.FillInsults();

        // Initialization of the state machine
        _gs = new GameplayState();
        _firstTurn = player;
        if (player == 1)
        {
            // Transition to PlayerTurnState
            _gs.actualGameplayState.ToPlayerTurnState();
        }
        else
        {
            // Transition to EnemyTurnState
            _gs.actualGameplayState.ToEnemyTurnState();
            // Enemy will choose randomly his first insult
            WriteEnemyOption();
        }

        FillUI();


    }

    public void FillUI()
    {
        // Fill down space with all the Insults/Answers, depending on the current turn.
        RemoveUI();

        var height = 50.0f;
        var index = 0;
        
        foreach (var insult in _insults)
        {
            var insultText = Instantiate(InsultText, OptionButtons, false);
            insultText.transform.SetParent(OptionButtons.transform, false);

            var x = insultText.GetComponent<RectTransform>().rect.x * 2.3f;
        
            insultText.GetComponent<RectTransform>().localPosition = new Vector3(x, height, 0);

            height += insultText.GetComponent<RectTransform>().rect.y * 2.0f;


            FillListener(insultText.GetComponent<Button>(), index);

            if (_firstTurn == 1) insultText.GetComponentInChildren<Text>().text = insult.Insult;
            else insultText.GetComponentInChildren<Text>().text = insult.Answer;

            index++;
        }
    }

    public void RemoveUI()
    {
        foreach (Transform child in OptionButtons.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void WritePlayerOption(int index)
    {
        PlayerText.GetComponentInChildren<Text>().text = ((_gs.actualGameplayState is PlayerTurnState && _insultIdx == -1) ? _insults[index].Insult : _insults[index].Answer);
    }

    public void WriteEnemyOption() 
    {
        int insultIdx = Random.Range(0, _insults.Length);
        EnemyText.GetComponentInChildren<Text>().text = (_gs.actualGameplayState is EnemyTurnState ? _insults[insultIdx].Insult : _insults[insultIdx].Answer);
        if (_firstTurn == 1)
        {
            _answerIdx = insultIdx;
            CheckRoundWinner();
        }
        else
        {
            _insultIdx = insultIdx;
            _gs.actualGameplayState.ToPlayerTurnState();
            FillUI();
        }
    }

    public void CheckRoundWinner()
    {
        Debug.Log("AND THE WINNER OF THIS ROUND IS....");
        if (_insultIdx == _answerIdx)
        {
            // Wins the one that answered the insult
            if (_firstTurn == 1)
            {
                // Enemy wins
                Debug.Log("The Enemy");
                _playerLives -= 1;
                _roundWinner = 2;
            }
            else
            {
                // Player wins
                Debug.Log("The Player");
                _enemyLives -= 1;
                _roundWinner = 1;
            }
        }
        else
        {
            // Wins the one that insulted first
            if (_firstTurn == 1)
            {
                // Player wins
                Debug.Log("The Player");
                _enemyLives -= 1;
                _roundWinner = 1;
            }
            else
            {
                // Enemy wins
                Debug.Log("The Enemy");
                _playerLives -= 1;
                _roundWinner = 2;
            }

        }
        // Update number of lives in game scene
        PlayerLivesText.GetComponentInChildren<Text>().text = "x" + _playerLives.ToString();
        EnemyLivesText.GetComponentInChildren<Text>().text = "x" + _enemyLives.ToString();

        Debug.Log("Espera 1");
        
        

        // Check if the game has finished
        if (_enemyLives == 0)
        {
            Debug.Log("Enemy wins the game");
            _gs.actualGameplayState.ToEndGameState();
            SceneManager.LoadScene("EndGameScene");
        }
        if (_playerLives == 0)
        {
            Debug.Log("Player wins the game");
            _gs.actualGameplayState.ToEndGameState();
            SceneManager.LoadScene("EndGameScene");
        }

        // Initializate all for the next round
        Wait(2, () => {
            InitRound();
        });
    }
   


    public void InitRound()
    {
        RemoveUI();

        EnemyText.GetComponentInChildren<Text>().text = "";
        PlayerText.GetComponentInChildren<Text>().text = "";

        _firstTurn = -1;
        _insultIdx = -1;
        _answerIdx = -1;
        if (_roundWinner == 1)
        {
            // Player will begin the next round
            _firstTurn = 1;
            _gs.actualGameplayState.ToPlayerTurnState();
        }
        else
        {
            // Enemy will begin the next round
            _firstTurn = 2;
            _gs.actualGameplayState.ToEnemyTurnState();
            // Enemy will choose randomly his first insult
            WriteEnemyOption();
        }
        FillUI();
    }


    private void FillListener(Button button, int index)
    {
        button.onClick.AddListener(() => { InsultSelected(index); });
    }


    private void InsultSelected(int index)
    {
        Debug.Log("Index button: " + index.ToString());
        WritePlayerOption(index);
        if (_insultIdx == -1)
        {
            _insultIdx = index;
            // Transition to EnemyTurnState
            _gs.actualGameplayState.ToEnemyTurnState();
            // Enemy will choose randomly his first insult
            WriteEnemyOption();
        } 
        else
        {
            _answerIdx = index;
            CheckRoundWinner();
        }

    }

    void OnMouseOver()
    {
        Debug.Log(gameObject.name);
    }

    private int GetRandomPlayer()
    {
        // Return a random integer number between 1 [inclusive] and 3 [exclusive]
        return Random.Range(1, 3);
    }

}

