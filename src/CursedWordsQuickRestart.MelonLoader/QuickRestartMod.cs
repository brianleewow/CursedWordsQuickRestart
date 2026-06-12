using MelonLoader;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[assembly: MelonInfo(typeof(CursedWordsQuickRestart.QuickRestartMod), "Cursed Words Quick Restart", "0.1.0", "local")]
[assembly: MelonGame(null, "Cursed Words")]

namespace CursedWordsQuickRestart;

public sealed class QuickRestartMod : MelonMod
{
    private const float RestartHoldSeconds = 1f;

    private float restartHoldTimer;
    private bool restartTriggered;

    public override void OnInitializeMelon()
    {
        MelonLogger.Msg("Cursed Words Quick Restart 0.1.0 loaded.");
    }

    public override void OnUpdate()
    {
        HandleQuickRestart();
    }

    private void HandleQuickRestart()
    {
        if (!Input.GetKey(KeyCode.R))
        {
            restartHoldTimer = 0f;
            restartTriggered = false;
            return;
        }

        if (restartTriggered)
        {
            return;
        }

        restartHoldTimer += Time.unscaledDeltaTime;
        if (restartHoldTimer < RestartHoldSeconds)
        {
            return;
        }

        restartTriggered = true;
        QuickRestartRun();
    }

    private static void QuickRestartRun()
    {
        try
        {
            if (GameStatics.GetPlayer() == null)
            {
                MelonLogger.Msg("Quick restart ignored: no active run.");
                return;
            }

            MelonLogger.Msg("Quick restart triggered.");

            if (TryExistingRetryRun())
            {
                return;
            }

            RestartRunDirectly();
        }
        catch (Exception ex)
        {
            MelonLogger.Error($"Quick restart failed: {ex}");
        }
    }

    private static bool TryExistingRetryRun()
    {
        EncounterController encounterController = UnityEngine.Object.FindAnyObjectByType<EncounterController>();
        if (encounterController != null)
        {
            encounterController.RetryRun();
            return true;
        }

        ShopController shopController = UnityEngine.Object.FindAnyObjectByType<ShopController>();
        if (shopController != null)
        {
            shopController.RetryRun();
            return true;
        }

        SettingsMenuController settingsMenuController = UnityEngine.Object.FindAnyObjectByType<SettingsMenuController>();
        if (settingsMenuController != null)
        {
            settingsMenuController.RetryRun();
            return true;
        }

        return false;
    }

    private static void RestartRunDirectly()
    {
        Player player = GameStatics.GetPlayer();
        Type characterType = player.MyCharacter.GetType();
        Type? challengeRunType = player.CurrentRunProgress.Challenge?.GetType();
        AscensionLevel ascension = player.CurrentRunProgress.Ascension;

        CharacterInfoPanel.SingletonInventoryVisualController?.RemovePanel();
        GameStatics.InitialisePlayerForNewRun(characterType, challengeRunType, ascension);

        string sceneName = GameStatics.GetPlayer().CurrentRunProgress.GoToNextNodeAndGetSceneName();
        TransitionController transitionController = UnityEngine.Object.FindAnyObjectByType<TransitionController>();

        if (transitionController != null)
        {
            transitionController.TransitionToNewScene(sceneName);
        }
        else
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
