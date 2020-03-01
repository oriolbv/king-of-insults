using UnityEngine;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour
{
    public Transform OptionButtons;
    public GameObject ButtonPrefab;
    public GameObject InsultText;
    public GameObject PlayerText;
    public GameObject EnemyText;


    private GameplayState _gs;
    private InsultNode[] _insults;
    private int _insultIdx = -1;
    private int _answerIdx = -1;
    private int _firstTurn = -1;

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

        _firstTurn = player;
        
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
        Debug.Log("AND THE WINNER IS....");
        if (_insultIdx == _answerIdx)
        {
            // Wins the one that answered the insult
            if (_firstTurn == 1)
            {
                // Enemy wins
                Debug.Log("The Enemy");
            }
            else
            {
                // Player wins
                Debug.Log("The Player");
            }
        }
        else
        {
            // Wins the one that insulted first
            if (_firstTurn == 1)
            {
                // Player wins
                Debug.Log("The Player");
            }
            else
            {
                // Enemy wins
                Debug.Log("The Enemy");
            }

        }

        _firstTurn = -1;
        _insultIdx = -1;
        _answerIdx = -1;
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

