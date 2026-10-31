using System;
using System.Collections.Generic;

[Serializable]
public class DeedRecord
{
    public string ChallengeId;
    public string Date;
    public int AgeBand;
}

[Serializable]
public class MoodRecord
{
    public string Date;
    public int Mood;
}

[Serializable]
public class PlayerProfile
{
    public string PlayerId;
    public int AgeBand = -1;
    public long CreatedUtc;
    public List<DeedRecord> Deeds = new List<DeedRecord>();
    public List<MoodRecord> Moods = new List<MoodRecord>();
    public bool RainbowPackOwned;
}
