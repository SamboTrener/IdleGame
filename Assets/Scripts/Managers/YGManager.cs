using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public static class YGManager
{
    static string lbName = "Leaderboard";

    public static void SaveNewScore(int score)
    {
        YandexGame.NewLeaderboardScores(lbName, score);
    }

    public static string GetLanguageString() => YandexGame.EnvironmentData.language;

    public static void ShowRewarded(int id) => YandexGame.RewVideoShow(id);

    public static void ShowFullAd() => YandexGame.FullscreenShow();

    public static void SendMetrica(string metricaName, int id)
    {
        var metricaParams = new Dictionary<string, string>
                {
                    { metricaName, $"{id}" }
                };
        YandexMetrica.Send(metricaName, metricaParams);
    }
}
