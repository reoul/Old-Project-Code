using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Reflection;
using Random = UnityEngine.Random;

public class DataManager : Singleton<DataManager>
{
    private const string _settingFilePath = "./setting.csv";
    private const string _scoreFilePath = "./score.csv";
    public SettingData Data;
    private List<Score> _scores;
    private int _lastPlayerIndex = 100;

    void Awake()
    {
        Data = new SettingData();
        _scores = new List<Score>();
        SettingLoad();
        if (PlayerPrefs.HasKey("ScoreCount"))
        {
            _lastPlayerIndex = PlayerPrefs.GetInt("ScoreCount");
        }
        else
        {
            _lastPlayerIndex = 100;
            PlayerPrefs.SetInt("ScoreCount", 100);
        }
    }

    // 반드시 게임 시작시 무조건 호출해서
    // 각 플레이 요소에 로드된 세팅값을 적용해야한다.
    /// <summary> 세팅값 로드 및 적용 </summary>
    private void SettingLoad()
    {
        try
        {
            using (StreamReader reader = new StreamReader(_settingFilePath))
            {
                while (!reader.EndOfStream)
                {
                    string[] data = { };
                    string line = reader.ReadLine();
                    try
                    {
                        data = line.Split(',');
                        switch (data[2].Trim().ToLower())
                        {
                            case "int32":
                            case "int":
                                ApplyData(data[0].Trim(), Convert.ToInt32(data[1].Trim()));
                                break;
                            case "single":
                            case "float":
                                ApplyData(data[0].Trim(), Convert.ToSingle(data[1].Trim()));
                                break;
                            case "bool":
                                ApplyData(data[0].Trim(), Convert.ToBoolean(data[1].Trim()));
                                break;
                            default:
                                Debug.LogError($"{data[0]} 변수의 자료형인 {data[2]} 타입은 지원하지 않는 자료형입니다");
                                break;
                        }
                    }
                    catch (IndexOutOfRangeException)
                    {
                        Debug.LogError($"IndexOutOfRangeException : {data[0]} 변수의 데이터 타입이 없습니다");
                    }
                    catch (FormatException)
                    {
                        Debug.LogError($"FormatException : {data[0]} 변수의 {data[1]} 값이 {data[2]} 타입에 맞지 않습니다");
                    }
                    catch (Exception e)
                    {
                        Debug.LogError($"{data[0]} 변수가 클래스에 없습니다");
                    }
                }
            }
        }
        catch (Exception e) //파일이 없음
        {
            return;
        }
    }

    void ApplyData<T>(string fieldName, T value)
    {
        FieldInfo fieldInfo = Type.GetType("SettingData").GetField(fieldName);
        if (fieldInfo.FieldType != typeof(T))
        {
            //Debug.LogError($"{fieldName} 변수의 타입 {fieldInfo.FieldType}이 입력 데이터 타입 {typeof(T)}이랑 같지 않습니다");
            return;
        }

        fieldInfo.SetValue(Data, value);
        //Debug.Log($"{fieldName}의 값이 {value}로 변경되었습니다");
    }

    public Score SaveNewScore()
    {
        _scores.Add(new Score($"HUNTER{_lastPlayerIndex.ToString()}", ScoreSystem.SumScore));
        using (StreamWriter writer = new StreamWriter(_scoreFilePath, true))
        {
            string data = $"HUNTER{(_lastPlayerIndex++).ToString()},{ScoreSystem.SumScore.ToString()}\n";
            writer.Write(data);
            writer.Flush();
        }

        return new Score(name, ScoreSystem.SumScore);
    }

    public void SaveNewScoreToPlayerPrefs()
    {
        string hunterName = $"HUNTER{_lastPlayerIndex.ToString()}";
        _scores.Add(new Score(hunterName, ScoreSystem.SumScore));
        PlayerPrefs.SetInt("ScoreCount", ++_lastPlayerIndex);
        PlayerPrefs.SetInt(hunterName, ScoreSystem.SumScore);
    }

    public List<Score> GetScoreToPlayerPrefs()
    {
        _scores.Clear();
        for (int i = 100; i < _lastPlayerIndex; i++)
        {
            string hunterName = $"HUNTER{i.ToString()}";
            int score = PlayerPrefs.GetInt(hunterName, 0);
            if(score == 0)
            {
                _scores.Add(new Score("NO_DATA", 0));
            }
            else
            {
                _scores.Add(new Score(hunterName, score));
            }
        }

        for (int emptyCnt = 10 - _scores.Count; emptyCnt > 0; --emptyCnt)
        {
            _scores.Add(new Score("NO_DATA", 0));
        }

        return _scores;
    }

    public List<Score> GetScore()
    {
        _scores.Clear();
        try
        {
            using (StreamReader reader = new StreamReader(_scoreFilePath))
            {
                string[] data = {"99", "99"};
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (line.Length > 6)
                    {
                        data = line.Split(',');
                        _scores.Add(new Score(data[0], Convert.ToInt32(data[1])));
                        data[0] = data[0].Substring(6);
                    }
                }

                _lastPlayerIndex = Convert.ToInt32(data[0]) + 1;
            }
        }
        catch (Exception e) //파일이 없음
        {
            // ignored
        }

        return _scores;
    }
}
