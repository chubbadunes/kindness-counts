using System;
using System.Collections.Generic;

[Serializable]
public class Challenge
{
    public string Id;
    public int MinBand;
    public int MaxBand;
    public string Category;
    public string Text;
    public bool Rainbow;
}

public static class ChallengeCatalog
{
    public static readonly List<Challenge> All = new List<Challenge>
    {
        new Challenge { Id = "smile3", MinBand = 0, MaxBand = 2, Category = "Kindness", Text = "Smile at 3 people today." },
        new Challenge { Id = "share", MinBand = 0, MaxBand = 2, Category = "Sharing", Text = "Share something of yours with someone." },
        new Challenge { Id = "cleanup", MinBand = 0, MaxBand = 2, Category = "Helping", Text = "Help clean up one thing without being asked." },
        new Challenge { Id = "compliment", MinBand = 0, MaxBand = 2, Category = "Kind words", Text = "Give someone a compliment." },
        new Challenge { Id = "listen", MinBand = 0, MaxBand = 2, Category = "Listening", Text = "Listen to someone without interrupting." },
        new Challenge { Id = "thankyou", MinBand = 0, MaxBand = 2, Category = "Gratitude", Text = "Say thank you to someone who helped you." },
        new Challenge { Id = "breathe", MinBand = 0, MaxBand = 2, Category = "Well-being", Text = "Take 5 slow, deep breaths." },
        new Challenge { Id = "wave", MinBand = 0, MaxBand = 2, Category = "Kindness", Text = "Give someone a high-five or a friendly wave." },
        new Challenge { Id = "gofirst", MinBand = 0, MaxBand = 2, Category = "Patience", Text = "Let someone else go first." },
        new Challenge { Id = "kindword", MinBand = 0, MaxBand = 2, Category = "Kind words", Text = "Say one kind thing to a friend." },
        new Challenge { Id = "carry", MinBand = 0, MaxBand = 2, Category = "Helping", Text = "Help carry something for someone." },
        new Challenge { Id = "picture", MinBand = 0, MaxBand = 2, Category = "Creative", Text = "Draw a happy picture for someone." },
        new Challenge { Id = "checkin", MinBand = 1, MaxBand = 2, Category = "Friendship", Text = "Ask a friend who seems quiet how they are doing." },
        new Challenge { Id = "include", MinBand = 1, MaxBand = 2, Category = "Friendship", Text = "Invite someone who is alone to play or join in." },
        new Challenge { Id = "note", MinBand = 1, MaxBand = 2, Category = "Gratitude", Text = "Write or draw a thank-you note for someone." },
        new Challenge { Id = "chore", MinBand = 1, MaxBand = 2, Category = "Helping", Text = "Do a chore for a family member." },
        new Challenge { Id = "standup", MinBand = 1, MaxBand = 2, Category = "Courage", Text = "Stand up for someone who is being treated unkindly." },
        new Challenge { Id = "donate", MinBand = 1, MaxBand = 2, Category = "Generosity", Text = "Pick one toy or book you have outgrown to give away." },
        new Challenge { Id = "patience", MinBand = 1, MaxBand = 2, Category = "Patience", Text = "Be patient with someone who is learning something new." },
        new Challenge { Id = "apology", MinBand = 1, MaxBand = 2, Category = "Kind words", Text = "Say sorry if you hurt someone's feelings." },
        new Challenge { Id = "table", MinBand = 1, MaxBand = 2, Category = "Helping", Text = "Help set or clear the table." },
        new Challenge { Id = "story", MinBand = 1, MaxBand = 2, Category = "Kindness", Text = "Read a story to a younger child." },
        new Challenge { Id = "plant", MinBand = 1, MaxBand = 2, Category = "Nature", Text = "Water a plant or help in a garden." },
        new Challenge { Id = "joke", MinBand = 1, MaxBand = 2, Category = "Joy", Text = "Tell a joke to make someone laugh." },
        new Challenge { Id = "mentor", MinBand = 2, MaxBand = 2, Category = "Helping", Text = "Help a younger kid with something they find hard." },
        new Challenge { Id = "neighbor", MinBand = 2, MaxBand = 2, Category = "Community", Text = "Do one kind thing for a neighbor." },
        new Challenge { Id = "lunch", MinBand = 2, MaxBand = 2, Category = "Friendship", Text = "Sit with someone at lunch who is eating alone." },
        new Challenge { Id = "litter", MinBand = 2, MaxBand = 2, Category = "Community", Text = "Pick up litter in a park or playground." },
        new Challenge { Id = "compliment2", MinBand = 2, MaxBand = 2, Category = "Kind words", Text = "Give a genuine compliment to someone you do not know well." },
        new Challenge { Id = "volunteer", MinBand = 2, MaxBand = 2, Category = "Helping", Text = "Offer to help a teacher or coach with a task." },
        new Challenge { Id = "feelings", MinBand = 2, MaxBand = 2, Category = "Well-being", Text = "Tell someone how you really feel: happy, sad, or worried." },
        new Challenge { Id = "screenfree", MinBand = 2, MaxBand = 2, Category = "Well-being", Text = "Spend 30 minutes without screens doing something kind." },
        new Challenge { Id = "letter", MinBand = 2, MaxBand = 2, Category = "Gratitude", Text = "Write a kind letter to someone in your family." },
        new Challenge { Id = "kindonline", MinBand = 2, MaxBand = 2, Category = "Kind words", Text = "Use kind words in a message or online." },
        new Challenge { Id = "selfcare", MinBand = 2, MaxBand = 2, Category = "Well-being", Text = "Do one thing today just for your own well-being." },
        new Challenge { Id = "plantomorrow", MinBand = 2, MaxBand = 2, Category = "Planning", Text = "Plan one kind act for tomorrow." },
        new Challenge { Id = "r_coupon", MinBand = 0, MaxBand = 2, Category = "Rainbow", Text = "Make a kindness coupon for a family member.", Rainbow = true },
        new Challenge { Id = "r_surprise", MinBand = 0, MaxBand = 2, Category = "Rainbow", Text = "Plan a surprise kind act for a friend.", Rainbow = true },
        new Challenge { Id = "r_rainbow", MinBand = 0, MaxBand = 2, Category = "Rainbow", Text = "Draw a rainbow picture and give it to someone.", Rainbow = true },
        new Challenge { Id = "r_teach", MinBand = 0, MaxBand = 2, Category = "Rainbow", Text = "Teach someone a new game or skill, kindly.", Rainbow = true },
        new Challenge { Id = "r_meal", MinBand = 0, MaxBand = 2, Category = "Rainbow", Text = "Help prepare a meal for someone.", Rainbow = true },
        new Challenge { Id = "r_selfkind", MinBand = 0, MaxBand = 2, Category = "Rainbow", Text = "Say something kind to yourself out loud.", Rainbow = true }
    };

    public static List<Challenge> ForBand(int band, bool rainbowOwned)
    {
        var list = new List<Challenge>();
        foreach (var c in All)
            if (c.MinBand <= band && band <= c.MaxBand && (!c.Rainbow || rainbowOwned))
                list.Add(c);
        return list;
    }

    public static List<Challenge> Todays(int band, bool rainbowOwned, string date)
    {
        var pool = ForBand(band, rainbowOwned);
        var result = new List<Challenge>();
        if (pool.Count == 0)
            return result;
        int seed = 0;
        foreach (char ch in date)
            seed = seed * 31 + ch;
        for (int i = 0; i < 3 && pool.Count > 0; i++)
        {
            int idx = ((seed + i * 7) % pool.Count + pool.Count) % pool.Count;
            result.Add(pool[idx]);
            pool.RemoveAt(idx);
        }
        return result;
    }
}
