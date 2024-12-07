using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HighScoreManager : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;
    public Button sortButton;
    private bool isAscending = true;

    public class Player
    {
        public int ID;
        public string name;
        public int score;

        public Player(int id, string name, int score)
        {
            this.ID = id;
            this.name = name;
            this.score = score;
        }
    }

    private List<Player> players = new List<Player>();

    void Start()
    {
        players.Add(new Player(0, "Player0", Random.Range(0, 100)));
        players.Add(new Player(1, "Player1", Random.Range(0, 100)));
        players.Add(new Player(2, "Player2", Random.Range(0, 100)));
        players.Add(new Player(3, "Player3", Random.Range(0, 100)));
        players.Add(new Player(4, "Player4", Random.Range(0, 100)));
        players.Add(new Player(5, "Player5", Random.Range(0, 100)));
        players.Add(new Player(6, "Player6", Random.Range(0, 100)));
        players.Add(new Player(7, "Player7", Random.Range(0, 100)));
        players.Add(new Player(8, "Player8", Random.Range(0, 100)));
        players.Add(new Player(9, "Player9", Random.Range(0, 100)));
        

        DisplayHighScores();

        if (sortButton != null)
        {
            sortButton.onClick.AddListener(ToggleSort);
        }
    }

    void DisplayHighScores()
    {
        if (highScoreText == null)
        {
            return;
        }

        highScoreText.text = "";

        foreach (Player player in players)
        {
            highScoreText.text += "ID: " + player.ID + "         - Name: " + player.name + "         - Score: " + player.score + "\n";
        }
    }

    void ToggleSort()
    {
        if (isAscending)
        {
            players.Sort((x, y) => x.score.CompareTo(y.score));
        }
        else
        {
            players.Sort((x, y) => y.score.CompareTo(x.score));
        }

        isAscending = !isAscending;
        DisplayHighScores();
    }
}