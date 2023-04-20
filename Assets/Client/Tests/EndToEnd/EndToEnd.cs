using Altom.AltDriver;
using NUnit.Framework;
using UnityEngine;
using System.Threading;
using System;

public class EndToEnd
{
    private AltDriver altDriver;

    [OneTimeSetUp]
    public void SetUp()
    {
        altDriver = new AltDriver();
    }
    
    [OneTimeTearDown]
    public void TearDown()
    {
        altDriver.Stop();
    }

    [Test]
    public void PlayButtonTest()
    {
        altDriver.LoadScene("Menu");
        AltObject playButton = altDriver.FindObject(By.NAME,"PlayButton");
        playButton.Click();
        
        Assert.AreEqual("Level 1", altDriver.GetCurrentScene());
        Thread.Sleep(500);
    }

    [Test]
    public void TryDoubleDash()
    {
        altDriver.LoadScene("TestSceneWithShop");
        AltObject dashButton = altDriver.FindObject(By.NAME, "Dash");
        Thread.Sleep(500);
        dashButton.Click();
        Thread.Sleep(500);
        var player = altDriver.FindObject(By.NAME, "Player");
        var position = player.GetWorldPosition();
        dashButton.Click();
        Thread.Sleep(300);
        player = altDriver.FindObject(By.NAME, "Player");
        Assert.AreEqual(player.GetWorldPosition().x, position.x, 0.001f);
        Assert.AreEqual(player.GetWorldPosition().y, position.y, 0.001f);
        Assert.AreEqual(player.GetWorldPosition().z, position.z, 0.001f);
        Thread.Sleep(500);
    }

    [Test]
    public void BuyInShop()
    {
        altDriver.LoadScene("TestSceneWithShop");
        Thread.Sleep(100);
        var player = altDriver.FindObject(By.NAME, "Player");
        int playerStartMaxHealth = player.GetComponentProperty<int>("Health", "maxHealth", "GameAssembly");
        float playerStartRunSpeed = player.GetComponentProperty<float>("PlayerMovement", "runSpeed", "GameAssembly");

        AltObject actionButton = altDriver.FindObject(By.NAME, "PickUp");
        actionButton.Click();
        Thread.Sleep(500);
        AltObject joystick = altDriver.FindObject(By.NAME, "Fixed Joystick");
        var initialPositionOfJoystick = joystick.GetScreenPosition();
        int fingerId = altDriver.BeginTouch(initialPositionOfJoystick);
        AltVector2 newPosition = new AltVector2(initialPositionOfJoystick.x + 20, initialPositionOfJoystick.y);
        altDriver.MoveTouch(fingerId, newPosition);
        altDriver.EndTouch(fingerId);
        Thread.Sleep(300);
        fingerId = altDriver.BeginTouch(initialPositionOfJoystick);
        altDriver.EndTouch(fingerId);
        actionButton.Click();

        Thread.Sleep(1000);
        fingerId = altDriver.BeginTouch(initialPositionOfJoystick);
        newPosition = new AltVector2(initialPositionOfJoystick.x + 20, initialPositionOfJoystick.y);
        altDriver.MoveTouch(fingerId, newPosition);
        altDriver.EndTouch(fingerId);
        Thread.Sleep(300);
        fingerId = altDriver.BeginTouch(initialPositionOfJoystick);
        altDriver.EndTouch(fingerId);
        actionButton.Click();

        Thread.Sleep(1000);
        fingerId = altDriver.BeginTouch(initialPositionOfJoystick);
        newPosition = new AltVector2(initialPositionOfJoystick.x + 20, initialPositionOfJoystick.y);
        altDriver.MoveTouch(fingerId, newPosition);
        altDriver.EndTouch(fingerId);
        Thread.Sleep(300);
        fingerId = altDriver.BeginTouch(initialPositionOfJoystick);
        altDriver.EndTouch(fingerId);
        actionButton.Click();

        player = altDriver.FindObject(By.NAME, "Player");
        int playerMaxHealth = player.GetComponentProperty<int>("Health", "maxHealth", "GameAssembly");
        float playerRunSpeed = player.GetComponentProperty<float>("PlayerMovement", "runSpeed", "GameAssembly");

        Assert.AreEqual(playerMaxHealth, 166, 1);
        Assert.AreEqual(playerRunSpeed, playerStartRunSpeed * 1.2, 0.0001f);

    }

    [Test]
    public void CantBuyInShop()
    {
        altDriver.LoadScene("TestSceneWithShopEnemy");
        Thread.Sleep(100);

        Assert.Throws<NotFoundException>(() => altDriver.FindObject(By.PATH, "//Canvas/GameInterface/Controls/PickUp"));
    }


    [Test]
    public void MoveCharacterWithJoystick()
    {
        altDriver.LoadScene("TestSceneWithShop");
        Thread.Sleep(100);
        var player = altDriver.FindObject(By.NAME, "Player");
        var position = player.GetWorldPosition();
        AltObject joystick = altDriver.FindObject(By.NAME, "Fixed Joystick");
        var initialPositionOfJoystick = joystick.GetScreenPosition();
        int fingerId = altDriver.BeginTouch(initialPositionOfJoystick);
        AltVector2 newPosition = new AltVector2(initialPositionOfJoystick.x + 20, initialPositionOfJoystick.y);
        altDriver.MoveTouch(fingerId, newPosition);
        Thread.Sleep(1000);
        altDriver.EndTouch(fingerId);
        player = altDriver.FindObject(By.NAME, "Player");
        Assert.Greater(player.GetWorldPosition().x, position.x);
        Thread.Sleep(1000);

        position = player.GetWorldPosition();
        fingerId = altDriver.BeginTouch(initialPositionOfJoystick);
        newPosition = new AltVector2(initialPositionOfJoystick.x - 20, initialPositionOfJoystick.y);
        altDriver.MoveTouch(fingerId, newPosition);
        Thread.Sleep(1000);
        altDriver.EndTouch(fingerId);
        player = altDriver.FindObject(By.NAME, "Player");
        fingerId = altDriver.BeginTouch(initialPositionOfJoystick);
        altDriver.EndTouch(fingerId);
        Assert.Less(player.GetWorldPosition().x, position.x);
    }

    [Test]
    public void SoundCheckTest()
    {
        altDriver.LoadScene("Menu");
        Thread.Sleep(500);
        AltObject optionsButton = altDriver.FindObject(By.NAME, "OptionsButton");
        optionsButton.Click();
        Thread.Sleep(300);

        AltObject joystick = altDriver.FindObject(By.NAME, "Slider");
        var initialPosition = joystick.GetScreenPosition();
        int fingerId = altDriver.BeginTouch(initialPosition);
        AltObject audio = altDriver.FindObject(By.NAME, "Audio");
        float startAudio = audio.GetComponentProperty<float>("MusicVolume", "musicVolume", "GameAssembly");
        Thread.Sleep(1000);
        AltVector2 newPosition = new AltVector2(initialPosition.x - 500, initialPosition.y);
        altDriver.MoveTouch(fingerId, newPosition);
        Thread.Sleep(1000);
        altDriver.EndTouch(fingerId);

        Thread.Sleep(300);
        audio = altDriver.FindObject(By.NAME, "Audio");
        float endAudio = audio.GetComponentProperty<float>("MusicVolume", "musicVolume", "GameAssembly");
        Assert.Less(endAudio, startAudio);
    }

    [Test]
    public void EditVolumeFromPauseTest()
    {
        altDriver.LoadScene("Menu");
        Thread.Sleep(500);
        AltObject playButton = altDriver.FindObject(By.NAME, "PlayButton");
        playButton.Click();
        Thread.Sleep(300);

        AltObject pauseButton = altDriver.FindObject(By.NAME, "PauseButton");
        pauseButton.Click();
        Thread.Sleep(300);
        
        AltObject optionsButton = altDriver.FindObject(By.NAME, "OptionsButton");
        optionsButton.Click();
        Thread.Sleep(300);

        AltObject joystick = altDriver.FindObject(By.NAME, "Slider");
        var initialPosition = joystick.GetScreenPosition();
        int fingerId = altDriver.BeginTouch(initialPosition);
        
        AltObject audio = altDriver.FindObject(By.NAME, "Audio");
        float startAudio = audio.GetComponentProperty<float>("MusicVolume", "musicVolume", "GameAssembly");
        
        Thread.Sleep(1000);
        AltVector2 newPosition = new AltVector2(initialPosition.x - 500, initialPosition.y);
        altDriver.MoveTouch(fingerId, newPosition);
        Thread.Sleep(1000);
        altDriver.EndTouch(fingerId);
        
        audio = altDriver.FindObject(By.NAME, "Audio");
        float endAudio = audio.GetComponentProperty<float>("MusicVolume", "musicVolume", "GameAssembly");
        Assert.Less(endAudio, startAudio);
        
        AltObject backButton = altDriver.FindObject(By.NAME, "BackButton");
        backButton.Click();
        Thread.Sleep(300);
        
        AltObject continueButton = altDriver.FindObject(By.NAME, "ContinueButton");
        continueButton.Click();
        Thread.Sleep(300);
    }

    [Test]
    public void HitEnemyTest()
    {
        altDriver.LoadScene("HitEnemyTestScene");

        var enemyHealth = altDriver.FindObject(By.PATH, "//MainRoom/Enemies/EnemyPatrolZone/Enemy");

        var enemyHealthValue = enemyHealth.GetComponentProperty<int>("Health", "health", "GameAssembly");

        AltObject attackButton = altDriver.FindObject(By.NAME, "Attack");

        Thread.Sleep(3000);

        attackButton.Click();
        
        Thread.Sleep(3000);

        var currentHealth = altDriver.FindObject(By.PATH, "//MainRoom/Enemies/EnemyPatrolZone/Enemy");

        var currentHealthValue = currentHealth.GetComponentProperty<int>("Health", "health", "GameAssembly");

        Assert.Less(currentHealthValue, enemyHealthValue);
    }

    [Test]
    public void MoveWithJumpTest() 
    {
        altDriver.LoadScene("MoveWithJumpTest");

        Thread.Sleep(100);

        var player = altDriver.FindObject(By.NAME, "Player");

        var position = player.GetWorldPosition();

        AltObject joystick = altDriver.FindObject(By.NAME, "Fixed Joystick");
        var initialPositionOfJoystick = joystick.GetScreenPosition();
        int fingerId = altDriver.BeginTouch(initialPositionOfJoystick);

        AltVector2 newPosition = new AltVector2(initialPositionOfJoystick.x + 20, initialPositionOfJoystick.y);
        altDriver.MoveTouch(fingerId, newPosition);
        AltObject jumpButton = altDriver.FindObject(By.NAME, "Jump");
        jumpButton.Click();
        Thread.Sleep(500);
        altDriver.EndTouch(fingerId);

        player = altDriver.FindObject(By.NAME, "Player");
        Assert.Greater(player.GetWorldPosition().x, position.x);
        Assert.Greater(player.GetWorldPosition().y, position.y);

        newPosition = new AltVector2(initialPositionOfJoystick.x, initialPositionOfJoystick.y);
        fingerId = altDriver.BeginTouch(initialPositionOfJoystick);
        altDriver.MoveTouch(fingerId, newPosition);
        altDriver.EndTouch(fingerId);
    }

    [Test]
    public void KillerJumpCatTest()
    {
        altDriver.LoadScene("KillerJumpCatTest");

        Thread.Sleep(100);

        var player = altDriver.FindObject(By.NAME, "Player");

        var position = player.GetWorldPosition();

        AltObject joystick = altDriver.FindObject(By.NAME, "Fixed Joystick");
        var initialPositionOfJoystick = joystick.GetScreenPosition();
        int fingerId = altDriver.BeginTouch(initialPositionOfJoystick);

        AltVector2 newPosition = new AltVector2(initialPositionOfJoystick.x - 20, initialPositionOfJoystick.y);
        altDriver.MoveTouch(fingerId, newPosition);
        AltObject jumpButton = altDriver.FindObject(By.NAME, "Jump");
        jumpButton.Click();
        altDriver.EndTouch(fingerId);
        Thread.Sleep(900);

        newPosition = new AltVector2(initialPositionOfJoystick.x + 20, initialPositionOfJoystick.y);
        fingerId = altDriver.BeginTouch(initialPositionOfJoystick);
        altDriver.MoveTouch(fingerId, newPosition);
        altDriver.EndTouch(fingerId);
        Thread.Sleep(900);

        Assert.Throws<NotFoundException>(() => altDriver.FindObject(By.PATH, "//MainRoom/EnemyPatrolZone/Enemy"));
    }
    
    [Test]
    public void DashWithJumpTest() 
    {
        altDriver.LoadScene("DashWithJumpTest");

        Thread.Sleep(100);

        var player = altDriver.FindObject(By.NAME, "Player");

        var position = player.GetWorldPosition();

        
        AltObject jumpButton = altDriver.FindObject(By.NAME, "Jump");
        AltObject dashButton = altDriver.FindObject(By.NAME, "Dash");
        jumpButton.Click();
        Thread.Sleep(400);
        dashButton.Click();
        Thread.Sleep(500);
        

        player = altDriver.FindObject(By.NAME, "Player");
        Assert.Greater(player.GetWorldPosition().x, position.x);
        Assert.Greater(player.GetWorldPosition().y, position.y);
    }
    
 
    [Test]
    public void UsePortalBetweenScenesTest() 
    {
        altDriver.LoadScene("PortalBetweenScenes");

        AltObject joystick = altDriver.FindObject(By.NAME, "Fixed Joystick");
        var initialPositionOfJoystick = joystick.GetScreenPosition();
        int fingerId = altDriver.BeginTouch(initialPositionOfJoystick);

        AltVector2 newPosition = new AltVector2(initialPositionOfJoystick.x + 20, initialPositionOfJoystick.y);
        altDriver.MoveTouch(fingerId, newPosition);
        Thread.Sleep(500);
        altDriver.EndTouch(fingerId);

        Assert.AreEqual("Level 2", altDriver.GetCurrentScene());
        Thread.Sleep(500);

        newPosition = new AltVector2(initialPositionOfJoystick.x, initialPositionOfJoystick.y);
        fingerId = altDriver.BeginTouch(initialPositionOfJoystick);
        altDriver.MoveTouch(fingerId, newPosition);
        altDriver.EndTouch(fingerId);
    }

    [Test]
    public void DodgeBossBulletWithJoystick()
    {
        altDriver.LoadScene("TestSceneWithBoss");
        var player = altDriver.FindObject(By.NAME, "Player");
        int playerStartHealth = player.GetComponentProperty<int>("Health", "health", "GameAssembly");
        AltObject joystick = altDriver.FindObject(By.NAME, "Fixed Joystick");
        var initialPositionOfJoystick = joystick.GetScreenPosition();
        int fingerId = altDriver.BeginTouch(initialPositionOfJoystick);
        AltVector2 newPosition = new AltVector2(initialPositionOfJoystick.x, initialPositionOfJoystick.y - 50);
        altDriver.MoveTouch(fingerId, newPosition);
        Thread.Sleep(3000);
        altDriver.EndTouch(fingerId);
        player = altDriver.FindObject(By.NAME, "Player");
        int playerHealth = player.GetComponentProperty<int>("Health", "health", "GameAssembly");
        Assert.AreEqual(playerHealth, playerStartHealth);
        fingerId = altDriver.BeginTouch(initialPositionOfJoystick);
        altDriver.EndTouch(fingerId);
    }

    [Test]
    public void NewRecordAfterKillingBoss()
    {
        Thread.Sleep(100);
        altDriver.LoadScene("TestSceneForNewRecord");
        var utils = altDriver.FindObject(By.NAME, "Utils");
        int scoresInitialCount = utils.GetComponentProperty<int>("GameManager", "scoresCount", "GameAssembly");
        AltObject attackButton = altDriver.FindObject(By.NAME, "Attack");
        attackButton.Click();
        Thread.Sleep(2000);

        AltObject continueButton = altDriver.FindObject(By.NAME, "ContinueButton");
        continueButton.Click();
        Thread.Sleep(1500);

        AltObject mainMenuButton = altDriver.FindObject(By.NAME, "MainMenuButton");
        mainMenuButton.Click();
        Thread.Sleep(1500);

        AltObject scoresButton = altDriver.FindObject(By.NAME, "ScoresButton");
        scoresButton.Click();
        Thread.Sleep(1500);

        utils = altDriver.FindObject(By.NAME, "Utils");
        int scoresCount = utils.GetComponentProperty<int>("GameManager", "scoresCount", "GameAssembly");
        Assert.Greater(scoresCount, scoresInitialCount);
    }

    [Test]
    public void RestartGameAfterKillingBoss()
    {
        altDriver.LoadScene("TestSceneForNewRecord");
        AltObject attackButton = altDriver.FindObject(By.NAME, "Attack");
        attackButton.Click();
        Thread.Sleep(2000);

        AltObject continueButton = altDriver.FindObject(By.NAME, "ContinueButton");
        continueButton.Click();
        Thread.Sleep(1500);

        AltObject retryButton = altDriver.FindObject(By.NAME, "RetryButton");
        retryButton.Click();
        Thread.Sleep(1500);

        Assert.AreEqual("Level 1", altDriver.GetCurrentScene());
    }

    [Test]
    public void DodgeDamageWithDash()
    {
        altDriver.LoadScene("TestSceneForDashDodge");
        var player = altDriver.FindObject(By.NAME, "Player");
        int playerStartHealth = player.GetComponentProperty<int>("Health", "health", "GameAssembly");
        AltObject dashButton = altDriver.FindObject(By.NAME, "Dash");
        dashButton.Click();
        Thread.Sleep(200);
        player = altDriver.FindObject(By.NAME, "Player");
        int playerHealth = player.GetComponentProperty<int>("Health", "health", "GameAssembly");
        Assert.AreEqual(playerHealth, playerStartHealth);
    }

    [Test]
    public void DoubleJump()
    {
        altDriver.LoadScene("TestSceneForDoubleJumpTest");
        Thread.Sleep(200);
        AltObject actionButton = altDriver.FindObject(By.NAME, "PickUp");
        actionButton.Click();
        AltObject jumpButton = altDriver.FindObject(By.NAME, "Jump");
        Thread.Sleep(200);
        var player = altDriver.FindObject(By.NAME, "Player");
        var startPosition = player.GetWorldPosition();
        jumpButton.Click();
        Thread.Sleep(200);
        player = altDriver.FindObject(By.NAME, "Player");
        var secondPosition = player.GetWorldPosition();
        jumpButton.Click();
        Thread.Sleep(200);
        player = altDriver.FindObject(By.NAME, "Player");
        var thirdPosition = player.GetWorldPosition();
        Assert.Greater(secondPosition.y, startPosition.y);
        Assert.Greater(thirdPosition.y, secondPosition.y);
    }
    
    [Test]
    public void ExitInMenuFromPauseTest()
    {
        altDriver.LoadScene("Menu");
        AltObject playButton = altDriver.FindObject(By.NAME, "PlayButton");
        playButton.Click();
        Thread.Sleep(300);

        AltObject pauseButton = altDriver.FindObject(By.NAME, "PauseButton");
        pauseButton.Click();
        Thread.Sleep(500);

        AltObject backButton = altDriver.FindObject(By.NAME, "MainMenuButton");
        backButton.Click();
        Thread.Sleep(300);
        
        Assert.AreEqual("Menu", altDriver.GetCurrentScene());
    }
    
    [Test]
    public void HitEnemyInJumpTest()
    {
        altDriver.LoadScene("HitInJumpTestScene");
        Thread.Sleep(400);

        var enemyHealth = altDriver.FindObject(By.PATH, "//TestEnemyPatrol/Enemy");
        var enemyHealthValue = enemyHealth.GetComponentProperty<int>("Health", "health", "GameAssembly");

        AltObject attackButton = altDriver.FindObject(By.NAME, "Attack");
        AltObject jumpButton = altDriver.FindObject(By.NAME, "Jump");
        jumpButton.Click();
        Thread.Sleep(200);
        attackButton.Click();

        Assert.Greater(enemyHealthValue, 0);
        Assert.Throws<NotFoundException>(() => altDriver.FindObject(By.PATH, "//TestEnemyPatrol/Enemy"));
        Thread.Sleep(400);
    }
	
	[Test]
    public void RestartAfterDeathTest() 
    {
        altDriver.LoadScene("Level 2");
        
        var player = altDriver.FindObject(By.NAME, "Player");

        player.CallComponentMethod<Int32>("PlayerController", "PlayerDmg", "GameAssembly", new object[] { 100 });
        
        Thread.Sleep(1000);
        
        AltObject playButton = altDriver.FindObject(By.NAME, "RetryButton");

        playButton.Click();

        Assert.AreEqual("Level 1", altDriver.GetCurrentScene());
    }
    
    [Test]
    public void MenuAfterDeathTest() 
    {
        altDriver.LoadScene("Level 2");
        
        var player = altDriver.FindObject(By.NAME, "Player");

        player.CallComponentMethod<Int32>("PlayerController", "PlayerDmg", "GameAssembly", new object[] { 100 });
        
        Thread.Sleep(1000);
        
        AltObject playButton = altDriver.FindObject(By.NAME, "MainMenuButton");

        playButton.Click();

        Assert.AreEqual("Menu", altDriver.GetCurrentScene());
    }
}
