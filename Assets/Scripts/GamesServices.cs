using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class GamesServices : MonoBehaviour
{
    public static bool loggedInGamesServices = false;

    //TODO change these IDs!
    public string LeaderboardId, Achi1Id, Achi2Id, Achi3Id, Achi4Id, Achi5Id, Achi6Id;

    void Awake()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
        // enables saving game progress.
        .EnableSavedGames()
        // registers a callback to handle game invitations received while the game is not running.
        .WithInvitationDelegate(null)
        // registers a callback for turn based match notifications received while the
        // game is not running.
        .WithMatchDelegate(null)
        // require access to a player's Google+ social graph (usually not needed)
        .RequireGooglePlus()
        .Build();

        PlayGamesPlatform.InitializeInstance(config);
        // recommended for debugging:
        PlayGamesPlatform.DebugLogEnabled = true;
        // Activate the Google Play Games platform
        PlayGamesPlatform.Activate();
    }

    void Start()
    {
        Social.localUser.Authenticate((bool success) => {
            loggedInGamesServices = success;
        });
    }

    public bool IncrementAchi(int achiIndex = 1)
    {
        bool isSuccess = false;

        switch (achiIndex)
        {
            case (1):
                {
                    PlayGamesPlatform.Instance.IncrementAchievement(
                    Achi1Id, 1, (bool success) =>
                    {
                        isSuccess = success;
                    });
                    break;
                }
            case (2):
                {
                    PlayGamesPlatform.Instance.IncrementAchievement(
                    Achi2Id, 1, (bool success) =>
                    {
                        isSuccess = success;
                    });
                    break;
                }
            case (3):
                {
                    PlayGamesPlatform.Instance.IncrementAchievement(
                    Achi3Id, 1, (bool success) =>
                    {
                        isSuccess = success;
                    });
                    break;
                }
            case (4):
                {
                    PlayGamesPlatform.Instance.IncrementAchievement(
                    Achi4Id, 1, (bool success) =>
                    {
                        isSuccess = success;
                    });
                    break;
                }
            case (5):
                {
                    PlayGamesPlatform.Instance.IncrementAchievement(
                    Achi5Id, 1, (bool success) =>
                    {
                        isSuccess = success;
                    });
                    break;
                }
            case (6):
                {
                    PlayGamesPlatform.Instance.IncrementAchievement(
                    Achi6Id, 1, (bool success) =>
                    {
                        isSuccess = success;
                    });
                    break;
                }
        }

        return isSuccess;
    }

    public bool UpdateLeaderboard(int score)
    {
        bool isSuccess = false;

        PlayGamesPlatform.Instance.ReportScore(
        score, LeaderboardId, (bool success) =>
        {
            isSuccess = success;
        });

        return isSuccess;
    }

    public void ShowLeaderboardUI()
    {
        Social.Active.ShowLeaderboardUI();
    }

    public void ShowAchievements()
    {
        Social.Active.ShowAchievementsUI();
    }
}
