using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public PlayerProfile Profile { get; private set; }
    public UIManager UI { get; private set; }
    public StoreManager Store { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        Profile = SaveSystem.Load();
        Store = gameObject.AddComponent<StoreManager>();
        Store.Init(this);
        UI = gameObject.AddComponent<UIManager>();
        UI.Build(this);
    }

    private void Start()
    {
        UI.Refresh();
    }

    public static string Today()
    {
        return DateTime.Now.ToString("yyyy-MM-dd");
    }

    public List<Challenge> TodaysChallenges()
    {
        if (Profile.AgeBand < 0)
            return new List<Challenge>();
        return ChallengeCatalog.Todays(Profile.AgeBand, Profile.RainbowPackOwned, Today());
    }

    public bool DeedDoneToday(string challengeId)
    {
        var today = Today();
        foreach (var deed in Profile.Deeds)
            if (deed.ChallengeId == challengeId && deed.Date == today)
                return true;
        return false;
    }

    public void RecordDeed(string challengeId)
    {
        if (Profile.AgeBand < 0 || DeedDoneToday(challengeId))
            return;
        Profile.Deeds.Add(new DeedRecord { ChallengeId = challengeId, Date = Today(), AgeBand = Profile.AgeBand });
        SaveSystem.Save(Profile);
        UI.Refresh();
    }

    public void RecordMood(int mood)
    {
        var today = Today();
        MoodRecord existing = Profile.Moods.Find(m => m.Date == today);
        if (existing != null)
            existing.Mood = mood;
        else
            Profile.Moods.Add(new MoodRecord { Date = today, Mood = mood });
        SaveSystem.Save(Profile);
        UI.RefreshMood();
    }

    public void SetAgeBand(int band)
    {
        Profile.AgeBand = band;
        SaveSystem.Save(Profile);
        UI.Refresh();
    }

    public int TotalDeeds
    {
        get { return Profile.Deeds.Count; }
    }

    public int Streak
    {
        get
        {
            var dates = new HashSet<string>();
            foreach (var deed in Profile.Deeds)
                dates.Add(deed.Date);
            int streak = 0;
            var day = DateTime.Now.Date;
            while (dates.Contains(day.ToString("yyyy-MM-dd")))
            {
                streak++;
                day = day.AddDays(-1);
            }
            return streak;
        }
    }

    public int GardenLevel
    {
        get { return TotalDeeds / 5 + 1; }
    }

    public void OnRainbowPackPurchased()
    {
        Profile.RainbowPackOwned = true;
        SaveSystem.Save(Profile);
        UI.Refresh();
    }
}
