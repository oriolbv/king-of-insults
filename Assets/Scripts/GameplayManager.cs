using UnityEngine;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour
{
    public Transform OptionButtons;
    public GameObject ButtonPrefab;
    public GameObject InsultText;

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
        foreach (Transform child in OptionButtons.transform)
        {
            Destroy(child.gameObject);
        }

        var isLeft = true;
        var height = 50.0f;
        var index = 0;
        var insults = InsultFiller.FillInsults();
        foreach (var insult in insults)
        {

            var insultText = Instantiate(InsultText, OptionButtons, false);
            insultText.transform.SetParent(OptionButtons.transform, false);

            var x = insultText.GetComponent<RectTransform>().rect.x * 2.3f;
            //var y = insultText.GetComponent<RectTransform>().rect.y * 2.3f;
            //height = heig
            insultText.GetComponent<RectTransform>().localPosition = new Vector3(x, height, 0);

            height += insultText.GetComponent<RectTransform>().rect.y * 2.0f;


            FillListener(insultText.GetComponent<Button>(), index);
            

            insultText.GetComponentInChildren<Text>().text = insult.Answer;

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

    void OnMouseOver()
    {
        Debug.Log(gameObject.name);
    }


    //public void OnPointerEnter(PointerEventData eventData)
    //{
    //    theText.color = Color.red; //Or however you do your color
    //}

    //public void OnPointerExit(PointerEventData eventData)
    //{
    //    theText.color = Color.white; //Or however you do your color
    //}
    private int GetRandomPlayer()
    {
        // Return a random integer number between 1 [inclusive] and 3 [exclusive]
        return Random.Range(1, 3);
    }

}

