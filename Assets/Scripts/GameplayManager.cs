using UnityEngine;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour
{
    public Transform OptionButtons;
    public GameObject ButtonPrefab;

    private void Start()
    {
        int player = GetRandomPlayer();
        Debug.Log("RandomPlayer: " + player.ToString());

        // Initialization of the state machine
        GameplayState gs = new GameplayState();
        if (player == 1)
        {
            // Transition to PlayerTurnState
            gs.actualGameplayState.ToPlayerTurnState();
        }
        else
        {
            // Transition to EnemyTurnState
            gs.actualGameplayState.ToEnemyTurnState();
        }

        Debug.Log("Turn: " + gs.actualGameplayState.ToString());

        FillUI();
    }

    public void FillUI()
    {

        //foreach (Transform child in OptionButtons.transform)
        //{
        //    Destroy(child.gameObject);
        //}

        var isLeft = true;
        var height = 50.0f;
        var index = 0;
        var insults = InsultFiller.FillInsults();
        foreach (var insult in insults)
        {
            Debug.Log(insult.Answer);

            var buttonAnswerCopy = Instantiate(ButtonPrefab, OptionButtons, true);

            var x = buttonAnswerCopy.GetComponent<RectTransform>().rect.x * 1.3f;
            buttonAnswerCopy.GetComponent<RectTransform>().localPosition = new Vector3(isLeft ? x : -x, height, 0);

            if (!isLeft)
                height += buttonAnswerCopy.GetComponent<RectTransform>().rect.y * 3.0f;
            isLeft = !isLeft;

            FillListener(buttonAnswerCopy.GetComponent<Button>(), index);

            buttonAnswerCopy.GetComponentInChildren<Text>().text = insult.Answer;

            index++;
        }
    }

    private void FillListener(Button button, int index)
    {
        button.onClick.AddListener(() => { InsultSelected(index); });
    }

    private void InsultSelected(int index)
    {
        Debug.Log("Index button: " + index.ToString());
    }


    private int GetRandomPlayer()
    {
        // Return a random integer number between 1 [inclusive] and 3 [exclusive]
        return Random.Range(1, 3);
    }

}

