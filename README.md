# Kindness Counts — a kind-acts game for kids (Android / Unity)

A small Unity game where kids get a few daily "kind missions" (be helpful, say kind things, take care of their own well-being), tap "I did it!" to log them, check in with how they feel, and grow a garden of flowers. Everything is stored **only on the device** under a random ID — no accounts, no names, no emails, no analytics, no ads, no data ever leaves the phone.

Monetization: one non-consumable in-app purchase, the **Rainbow Pack** (`kindness_rainbow_pack`), sold through Google Play Billing behind a parental gate (press-and-hold confirmation). The purchase is handled entirely by Google Play; the app stores only a boolean "owned" flag locally.

## Open and run

1. Install **Unity Hub** and **Unity 2022.3 LTS** (the project targets 2022.3.45f1).
2. In Unity Hub, **Add** this project folder and open it.
3. Open `Assets/Scenes/Main.unity` and press Play — the whole UI is built at runtime, so the scene needs no setup.

## Change the name

- Display name: `Assets/Scripts/Core/GameConstants.cs` (`AppName`) and `ProjectSettings/ProjectSettings.asset` (`productName`).
- Package ID: `bundleIdentifier` in `ProjectSettings/ProjectSettings.asset` (keep it stable once published).

## Project layout

```
Assets/
  Scenes/Main.unity                 entry scene (GameManager bootstrap)
  Scripts/Core/GameConstants.cs     app name constant
  Scripts/Core/PlayerProfile.cs     local data model (no PII, random ID only)
  Scripts/Core/SaveSystem.cs        JSON save to Application.persistentDataPath
  Scripts/Core/GameManager.cs       game state, streaks, garden, daily missions
  Scripts/Data/ChallengeCatalog.cs  all missions by age band (5-7, 8-10, 11-13)
  Scripts/UI/UIManager.cs           builds and refreshes the whole UI at runtime
  Scripts/UI/ParentalGate.cs        press-and-hold grown-up confirmation
  Scripts/IAP/StoreManager.cs       Google Play Billing (Unity IAP 4.x)
  Plugins/Android/AndroidManifest.xml  INTERNET + BILLING permissions
Packages/manifest.json              com.unity.purchasing 4.12.2, com.unity.ugui
```

## Android release checklist (Google Play)

1. **File > Build Settings > Android**, switch platform, set a keystore in Player Settings > Publishing Settings, build an `.aab`.
2. Create the app in [Play Console](https://play.google.com/console):
   - Target API level 34+ (set in Player Settings > Android > Target API Level).
   - Enroll in **Designed for Families** and select the **Kids** category (or mark it as appealing to children).
   - **Data safety**: declare **"No data collected"** — the app collects nothing.
   - Add the privacy policy URL (see `PRIVACY_POLICY.md` — host it on any page you control).
   - Ads: declare **no ads** (the app shows none).
3. **Monetization > Products > In-app products**: create `kindness_rainbow_pack`, type **non-consumable (managed product)**, price it, activate it. Enable **parental gate / purchase approval** expectations for kids' apps in the store settings.
4. Add **license testers** (Setup > License testing) and test the Rainbow Pack purchase with a license-test account before release. In Play mode the real product ID works only after it is active in Play Console.
5. Upload to **Internal testing**, test on real devices, then promote: closed testing > open testing > production.

## Privacy design (why this is safe)

- No sign-up, no login, no free-text input anywhere — kids only tap pre-written missions, so nothing a child types can ever be collected or shared.
- The only identifier is a random UUID generated on first launch, stored in the app's private storage (`Application.persistentDataPath`). It is never transmitted.
- No third-party SDKs: no ad networks, no analytics, no social features, no crash reporters that collect identifiers.
- All progress (missions, mood check-ins, purchases-owned flag) lives in one local JSON file. Uninstalling the app deletes everything.
- COPPA/GDPR-K posture: the app is child-directed and collects no personal data, shows no behavioral ads, and uses only store-native purchasing with a parental gate.
