using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private GameManager gm;
    private Transform canvasRoot;
    private Text statsText;
    private GameObject agePanel;
    private GameObject gamePanel;
    private Transform challengeList;
    private Transform gardenRow;
    private Text gardenLabel;
    private Text moodText;
    private Button buyButton;

    public void Build(GameManager manager)
    {
        gm = manager;
        var canvasGo = new GameObject("UICanvas");
        var canvas = canvasGo.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        var scaler = canvasGo.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1080, 1920);
        canvasGo.AddComponent<GraphicRaycaster>();
        DontDestroyOnLoad(canvasGo);
        canvasRoot = canvasGo.transform;

        var root = CreatePanel("Root", canvasRoot, new Color(0.98f, 0.97f, 0.92f, 1f));
        Stretch(root.transform);
        var rootLayout = root.AddComponent<VerticalLayoutGroup>();
        rootLayout.childAlignment = TextAnchor.UpperCenter;
        rootLayout.spacing = 20;
        rootLayout.padding = new RectOffset(36, 36, 48, 48);
        rootLayout.childControlWidth = true;
        rootLayout.childControlHeight = false;
        rootLayout.childForceExpandWidth = true;
        rootLayout.childForceExpandHeight = false;

        var title = CreateText(root.transform, "Title", GameConstants.AppName, 72, FontStyle.Bold, TextAnchor.MiddleCenter, new Color(0.42f, 0.25f, 0.62f));
        SetHeight(title.gameObject, 110);

        statsText = CreateText(root.transform, "Stats", "", 40, FontStyle.Normal, TextAnchor.MiddleCenter, new Color(0.25f, 0.25f, 0.3f));
        SetHeight(statsText.gameObject, 60);

        agePanel = CreatePanel("AgePanel", root.transform, new Color(1f, 1f, 1f, 0.9f));
        Flex(agePanel);
        var ageLayout = agePanel.AddComponent<VerticalLayoutGroup>();
        ageLayout.spacing = 20;
        ageLayout.padding = new RectOffset(30, 30, 30, 30);
        ageLayout.childControlWidth = true;
        ageLayout.childControlHeight = false;
        ageLayout.childForceExpandWidth = true;
        ageLayout.childForceExpandHeight = false;
        var question = CreateText(agePanel.transform, "Question", "How old are you?", 46, FontStyle.Bold, TextAnchor.MiddleCenter, Color.black);
        SetHeight(question.gameObject, 70);
        CreateButton(agePanel.transform, "Ages 5 - 7", () => gm.SetAgeBand(0));
        CreateButton(agePanel.transform, "Ages 8 - 10", () => gm.SetAgeBand(1));
        CreateButton(agePanel.transform, "Ages 11 - 13", () => gm.SetAgeBand(2));

        gamePanel = CreatePanel("GamePanel", root.transform, new Color(1f, 1f, 1f, 0f));
        Flex(gamePanel);
        var gameLayout = gamePanel.AddComponent<VerticalLayoutGroup>();
        gameLayout.spacing = 18;
        gameLayout.childControlWidth = true;
        gameLayout.childControlHeight = false;
        gameLayout.childForceExpandWidth = true;
        gameLayout.childForceExpandHeight = false;

        var challengesHeader = CreateText(gamePanel.transform, "ChallengesHeader", "Today's kind missions", 48, FontStyle.Bold, TextAnchor.MiddleCenter, new Color(0.2f, 0.45f, 0.3f));
        SetHeight(challengesHeader.gameObject, 64);

        var listGo = Child(gamePanel.transform, "ChallengeList");
        challengeList = listGo.transform;
        var listLayout = listGo.AddComponent<VerticalLayoutGroup>();
        listLayout.spacing = 16;
        listLayout.childControlWidth = true;
        listLayout.childControlHeight = false;
        listLayout.childForceExpandWidth = true;
        listLayout.childForceExpandHeight = false;
        listGo.AddComponent<LayoutElement>().flexibleHeight = 1f;

        var moodHeader = CreateText(gamePanel.transform, "MoodHeader", "How do you feel today? Tap a face.", 44, FontStyle.Bold, TextAnchor.MiddleCenter, Color.black);
        SetHeight(moodHeader.gameObject, 60);
        var moodRow = Child(gamePanel.transform, "MoodRow");
        var moodLayout = moodRow.AddComponent<HorizontalLayoutGroup>();
        moodLayout.spacing = 24;
        moodLayout.childAlignment = TextAnchor.MiddleCenter;
        moodLayout.childControlWidth = false;
        moodLayout.childControlHeight = false;
        moodLayout.childForceExpandWidth = false;
        moodLayout.childForceExpandHeight = false;
        CreateButton(moodRow.transform, "\U0001F604", () => gm.RecordMood(2));
        CreateButton(moodRow.transform, "\U0001F610", () => gm.RecordMood(1));
        CreateButton(moodRow.transform, "\U0001F622", () => gm.RecordMood(0));
        moodText = CreateText(gamePanel.transform, "MoodState", "", 34, FontStyle.Italic, TextAnchor.MiddleCenter, new Color(0.4f, 0.4f, 0.45f));
        SetHeight(moodText.gameObject, 44);

        var gardenHeader = CreateText(gamePanel.transform, "GardenHeader", "Your kindness garden", 44, FontStyle.Bold, TextAnchor.MiddleCenter, new Color(0.2f, 0.45f, 0.3f));
        SetHeight(gardenHeader.gameObject, 60);
        var gardenGo = Child(gamePanel.transform, "GardenRow");
        gardenRow = gardenGo.transform;
        var gardenLayout = gardenGo.AddComponent<HorizontalLayoutGroup>();
        gardenLayout.spacing = 8;
        gardenLayout.childAlignment = TextAnchor.MiddleCenter;
        gardenLayout.childControlWidth = false;
        gardenLayout.childControlHeight = false;
        gardenLayout.childForceExpandWidth = false;
        gardenLayout.childForceExpandHeight = false;
        gardenLabel = CreateText(gamePanel.transform, "GardenLabel", "", 36, FontStyle.Normal, TextAnchor.MiddleCenter, new Color(0.25f, 0.25f, 0.3f));
        SetHeight(gardenLabel.gameObject, 48);

        buyButton = CreateButton(gamePanel.transform, "Get the Rainbow Pack", OnBuyClicked);
    }

    public void Refresh()
    {
        bool hasAge = gm.Profile.AgeBand >= 0;
        agePanel.SetActive(!hasAge);
        gamePanel.SetActive(hasAge);
        if (!hasAge)
            return;
        statsText.text = "Kind acts: " + gm.TotalDeeds
            + "   Streak: " + gm.Streak
            + "   Garden level: " + gm.GardenLevel
            + (gm.Profile.RainbowPackOwned ? "   Rainbow Pack owned" : "");
        RefreshChallenges();
        RefreshMood();
        RefreshGarden();
        RefreshBuyButton();
    }

    public void RefreshMood()
    {
        var today = GameManager.Today();
        MoodRecord todayMood = null;
        foreach (var m in gm.Profile.Moods)
            if (m.Date == today)
                todayMood = m;
        if (todayMood == null)
            moodText.text = "Not checked in yet today.";
        else if (todayMood.Mood == 2)
            moodText.text = "You feel great today!";
        else if (todayMood.Mood == 1)
            moodText.text = "You feel okay today.";
        else
            moodText.text = "It is okay to feel sad. Be kind to yourself.";
    }

    public void RefreshBuyButton()
    {
        if (buyButton != null)
            buyButton.gameObject.SetActive(!gm.Profile.RainbowPackOwned);
    }

    private void RefreshChallenges()
    {
        foreach (Transform child in challengeList)
            Destroy(child.gameObject);
        foreach (var challenge in gm.TodaysChallenges())
        {
            bool done = gm.DeedDoneToday(challenge.Id);
            var card = CreatePanel("Card", challengeList, done ? new Color(0.88f, 0.96f, 0.82f, 1f) : Color.white);
            var cardLayout = card.AddComponent<VerticalLayoutGroup>();
            cardLayout.spacing = 12;
            cardLayout.padding = new RectOffset(24, 24, 18, 18);
            cardLayout.childControlWidth = true;
            cardLayout.childControlHeight = false;
            cardLayout.childForceExpandWidth = true;
            cardLayout.childForceExpandHeight = false;
            var text = CreateText(card.transform, "Text", challenge.Text, 38, FontStyle.Normal, TextAnchor.MiddleCenter, Color.black);
            SetHeight(text.gameObject, 100);
            var button = CreateButton(card.transform, done ? "Done!" : "I did it!", () => gm.RecordDeed(challenge.Id));
            button.interactable = !done;
            if (done)
                button.GetComponent<Image>().color = new Color(0.65f, 0.85f, 0.55f, 1f);
        }
    }

    private void RefreshGarden()
    {
        foreach (Transform child in gardenRow)
            Destroy(child.gameObject);
        int shown = Mathf.Min(gm.TotalDeeds, 60);
        for (int i = 0; i < shown; i++)
            CreateFlower(gardenRow, Color.HSVToRGB((i * 0.137f) % 1f, 0.65f, 0.95f));
        gardenLabel.text = gm.TotalDeeds == 0
            ? "Do a kind act to plant your first flower!"
            : gm.TotalDeeds + (gm.TotalDeeds == 1 ? " kind act has" : " kind acts have") + " grown this garden.";
    }

    private void OnBuyClicked()
    {
        var overlay = CreatePanel("GateOverlay", canvasRoot, new Color(0f, 0f, 0f, 0.65f));
        Stretch(overlay.transform);
        var box = CreatePanel("GateBox", overlay.transform, Color.white);
        var boxRect = (RectTransform)box.transform;
        boxRect.anchorMin = new Vector2(0.5f, 0.5f);
        boxRect.anchorMax = new Vector2(0.5f, 0.5f);
        boxRect.pivot = new Vector2(0.5f, 0.5f);
        boxRect.sizeDelta = new Vector2(760, 560);
        var boxLayout = box.AddComponent<VerticalLayoutGroup>();
        boxLayout.spacing = 24;
        boxLayout.padding = new RectOffset(40, 40, 40, 40);
        boxLayout.childControlWidth = true;
        boxLayout.childControlHeight = false;
        boxLayout.childForceExpandWidth = true;
        boxLayout.childForceExpandHeight = false;
        var gateText = CreateText(box.transform, "GateText", "A grown-up must confirm this purchase.\nPress and HOLD the heart for 3 seconds.", 38, FontStyle.Bold, TextAnchor.MiddleCenter, Color.black);
        SetHeight(gateText.gameObject, 180);
        var holdGo = new GameObject("HoldButton", typeof(RectTransform), typeof(Image), typeof(Button));
        holdGo.transform.SetParent(box.transform, false);
        var holdImage = holdGo.GetComponent<Image>();
        holdImage.color = new Color(0.9f, 0.35f, 0.45f, 1f);
        holdImage.type = Image.Type.Filled;
        holdImage.fillMethod = Image.FillMethod.Horizontal;
        holdImage.fillAmount = 0f;
        var gate = holdGo.AddComponent<ParentalGate>();
        gate.SetFillImage(holdImage);
        gate.onConfirmed.AddListener(() =>
        {
            gm.Store.BuyRainbowPack();
            Destroy(overlay);
        });
        ((RectTransform)holdGo.transform).sizeDelta = new Vector2(0, 140);
        var holdText = CreateText(holdGo.transform, "HoldText", "\u2764  HOLD", 44, FontStyle.Bold, TextAnchor.MiddleCenter, Color.white);
        Stretch(holdText.transform);
        CreateButton(box.transform, "Cancel", () => Destroy(overlay));
    }

    private void CreateFlower(Transform parent, Color color)
    {
        var go = new GameObject("Flower", typeof(RectTransform), typeof(Image));
        go.transform.SetParent(parent, false);
        go.GetComponent<Image>().color = color;
        ((RectTransform)go.transform).sizeDelta = new Vector2(48, 48);
    }

    private GameObject CreatePanel(string name, Transform parent, Color color)
    {
        var go = new GameObject(name, typeof(RectTransform), typeof(Image));
        go.transform.SetParent(parent, false);
        go.GetComponent<Image>().color = color;
        return go;
    }

    private GameObject Child(Transform parent, string name)
    {
        var go = new GameObject(name, typeof(RectTransform));
        go.transform.SetParent(parent, false);
        return go;
    }

    private Text CreateText(Transform parent, string name, string content, int size, FontStyle style, TextAnchor anchor, Color color)
    {
        var go = new GameObject(name, typeof(RectTransform), typeof(Text));
        go.transform.SetParent(parent, false);
        var text = go.GetComponent<Text>();
        text.text = content;
        text.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        text.fontSize = size;
        text.fontStyle = style;
        text.alignment = anchor;
        text.color = color;
        text.horizontalOverflow = HorizontalWrapMode.Wrap;
        text.verticalOverflow = VerticalWrapMode.Overflow;
        return text;
    }

    private Button CreateButton(Transform parent, string label, UnityAction onClick)
    {
        var go = new GameObject("Button", typeof(RectTransform), typeof(Image), typeof(Button));
        go.transform.SetParent(parent, false);
        var image = go.GetComponent<Image>();
        image.color = new Color(0.55f, 0.75f, 0.45f, 1f);
        var button = go.GetComponent<Button>();
        button.onClick.AddListener(onClick);
        ((RectTransform)go.transform).sizeDelta = new Vector2(0, 110);
        var text = CreateText(go.transform, "Label", label, 40, FontStyle.Bold, TextAnchor.MiddleCenter, Color.white);
        Stretch(text.transform);
        return button;
    }

    private void Stretch(Transform t)
    {
        var rt = (RectTransform)t;
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.offsetMin = Vector2.zero;
        rt.offsetMax = Vector2.zero;
    }

    private void SetHeight(GameObject go, float height)
    {
        ((RectTransform)go.transform).sizeDelta = new Vector2(0, height);
    }

    private void Flex(GameObject go)
    {
        go.AddComponent<LayoutElement>().flexibleHeight = 1f;
    }
}
