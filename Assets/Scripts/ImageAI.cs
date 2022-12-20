using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenAI;
using System.Threading.Tasks;
using OpenAI.Images;
using UnityEngine.UI;
using System.IO;
using TMPro;
public class ImageAI : MonoBehaviour
{
    OpenAIClient api;
    [SerializeField]
    Image pic1;  
    [SerializeField]
    Image pic2;  
    Texture2D image;
    List<string> chosenWords = new List<string>();
    [SerializeField] List<Button> buttons = new List<Button>(10);
    int score = 0;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI msgText;
    string adjectivePath = "Assets/english-adjectives.txt";
    string oxfordPath = "Assets/Oxford 3000 Word List No Spaces.txt";
    string nounsPath = "Assets/english-nouns.txt";
    private string adjective;
    private string control;
    private string word;
    private string input;
    private List<string> randomUnchosenWords = new List<string>();

    private void Awake()
    {
        api = new OpenAIClient("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", OpenAI.Engine.Davinci);
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("\u2665".ToString() + " Text after symbol");
        GameSelector();
    }

    void GameSelector()
    {
        //OneWordImage();
        TwoWordsImage();
    }

    private void OneWordImage()
    {
        chosenWords.Clear();
        randomUnchosenWords.Clear();
        chosenWords.Add(GenerateWord());
        randomUnchosenWords.Add(GenerateWord());
        randomUnchosenWords.Add(GenerateWord());
        AssignButtons(chosenWords[0], randomUnchosenWords);
        GenerateImage(chosenWords, "in child drawing style", ImageSize.Medium);
    }
    private void TwoWordsImage()
    {
        chosenWords.Clear();
        randomUnchosenWords.Clear();
        chosenWords.Add(GenerateWord());
        chosenWords.Add(GenerateWord());
        randomUnchosenWords.Add(GenerateWord());
        AssignButtons(randomUnchosenWords[0], chosenWords);
        CreateSentence(chosenWords);
        GenerateImage(chosenWords, "in child drawing style", ImageSize.Medium);
    }

    private async void CreateSentence(List<string> inputWords)
    {
        string query = $"create a funny sentence using the words {inputWords[0]} and {inputWords[1]}";
        var result = await api.CompletionEndpoint.CreateCompletionAsync(query, engine: Engine.Davinci);

        Debug.Log(result);
    }

    private string GenerateWord()
    {
        var lines = File.ReadAllLines(nounsPath);
        var r = new System.Random();
        return lines[r.Next(0, lines.Length - 1)];
    }
    private void AssignButtons(string correctAnswer, List<string> falseAnswers)
    {
        var r = new System.Random();
        int randomButtonIndex = r.Next(0, 2);
        buttons[randomButtonIndex].GetComponentInChildren<TextMeshProUGUI>().text = correctAnswer;
        buttons[randomButtonIndex].onClick.RemoveAllListeners();
        buttons[randomButtonIndex].onClick.AddListener(() => Success());
        int j = 0;
        for (int i = 0; i < 3; i++)
        {
            if (i != randomButtonIndex)
            {
                buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = falseAnswers[j];
                buttons[i].onClick.RemoveAllListeners();
                buttons[i].onClick.AddListener(() => Failure(correctAnswer));
                j++;
            }
        }
    }
    private void Failure(string correctAnswer)
    {
        score--;
        scoreText.text = score.ToString();
        msgText.text = $"The correct answer is {correctAnswer}.";
        Invoke("GameSelector", 3);
    }

    private void Success()
    {
        score++;
        scoreText.text = score.ToString();
        msgText.text = "Great Job!!!";
        Invoke("GameSelector", 3);
    }

    private async void GenerateImage(List<string> inputWords, string style, ImageSize imageSize)
    {
        input = "";
        foreach (string inputWord in inputWords)
        {
            input += inputWord + " ";
        }
        var results = await api.ImageGenerationEndPoint.GenerateImageAsync("house dinosaor" + style, 2, imageSize);
        image = results[0];
        pic1.sprite = Sprite.Create(image, new Rect(0.0f, 0.0f, image.width, image.height), new Vector2(0.5f, 0.5f));
        pic1.SetNativeSize();
        image = results[1];
        pic2.sprite = Sprite.Create(image, new Rect(0.0f, 0.0f, image.width, image.height), new Vector2(0.5f, 0.5f));
        pic2.SetNativeSize();
    }
}
