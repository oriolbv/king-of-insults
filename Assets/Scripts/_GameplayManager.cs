using UnityEngine;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour
{
    public Text HistoryText;
    public Transform AnswersParent;
    public GameObject ButtonAnswerPrefab;

    private StoryNode currentNode;

    private void Start()
    {
        currentNode = StoryFiller.FillStory();
        HistoryText.text = string.Empty;
        FillUi();
    }

    void FillUi()
    {
        HistoryText.text += "\n\n" + currentNode.History;

        foreach (Transform child in AnswersParent.transform)
        {
            Destroy(child.gameObject);
        }

        var isLeft = true;
        var height = 50.0f;
        var index = 0;
        foreach (var answer in currentNode.Answers)
        {
            var buttonAnswerCopy = Instantiate(ButtonAnswerPrefab, AnswersParent, true);

            var x = buttonAnswerCopy.GetComponent<RectTransform>().rect.x * 1.3f;
            buttonAnswerCopy.GetComponent<RectTransform>().localPosition = new Vector3(isLeft ? x : -x, height, 0);

            if (!isLeft)
                height += buttonAnswerCopy.GetComponent<RectTransform>().rect.y * 3.0f;
            isLeft = !isLeft;

            FillListener(buttonAnswerCopy.GetComponent<Button>(), index);

            buttonAnswerCopy.GetComponentInChildren<Text>().text = answer;

            index++;
        }
    }

    private void FillListener(Button button, int index)
    {
        button.onClick.AddListener(() => { AnswerSelected(index); });
    }

    private void AnswerSelected(int index)
    {
        HistoryText.text += "\n" + currentNode.Answers[index];

        if (!currentNode.IsFinal)
        {
            currentNode = currentNode.NextNode[index];

            currentNode.OnNodeVisited?.Invoke();

            FillUi();
        }
        else
        {
            HistoryText.text += "\n" + "CLICA ESCAPE PER CONTINUAR";
        }
    }
}
