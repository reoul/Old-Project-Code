using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoardManager : MonoBehaviour
{
    [SerializeField] private Transform[] _rankP;
    [SerializeField] private Transform _myRank;
    
    private List<Score> _ranking;

    private Score _playerScore;
    
    private void Start()
    {
        UpdateScoreData();
    }

    public void UpdateScoreData()
    {
        _ranking = DataManager.Instance.GetScoreToPlayerPrefs();
        _playerScore = _ranking[_ranking.Count - 1];
        UpdateScoreBoard();
    }

    private void UpdateScoreBoard()
    {
        //스코어 순으로 정렬
        _ranking = _ranking.OrderBy(x => x.score).Reverse().ToList();

        //스코어보드 출력
        for (int i = 0; i < _rankP.Length; i++)
        {
            _rankP[i].GetChild(1).GetComponent<Text>().text = _ranking[i].name;
            _rankP[i].GetChild(2).GetComponent<Text>().text = _ranking[i].score.ToString();
        }

        int playerRank = _ranking.IndexOf(_playerScore);
        if (playerRank < 10)
        {
            foreach (var text in _rankP[playerRank].GetComponentsInChildren<Text>())
            {
                text.color = Color.green;
            }
            _rankP[playerRank].GetComponentInChildren<Image>().color = Color.green;
        }

        _myRank.GetChild(0).GetComponent<Text>().text = (playerRank + 1).ToString();
        _myRank.GetChild(1).GetComponent<Text>().text = _playerScore.name;
        _myRank.GetChild(2).GetComponent<Text>().text = _playerScore.score.ToString();
    }

    private int FindPlayerRank(Score myScore)
    {
        int myRank = 0;
        foreach (var score in _ranking.Select((value, index) => (value, index)))
        {
            if (score.value.name == myScore.name && score.value.score == myScore.score)
            {
                myRank = score.index;
            }
        }

        return myRank;
    }
}
