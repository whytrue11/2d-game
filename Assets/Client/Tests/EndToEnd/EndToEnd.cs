using Altom.AltDriver;
using NUnit.Framework;
using UnityEngine;
using System.Threading;

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
        
        Debug.Log(altDriver.GetCurrentScene());
        Assert.AreEqual("Level 1", altDriver.GetCurrentScene());
    }

    [Test]
    public void TryDoubleDash()
    {
        altDriver.LoadScene("TestSceneWithShop");
        AltObject dashButton = altDriver.FindObject(By.NAME, "Dash");
        Thread.Sleep(100);
        dashButton.Click();
        Thread.Sleep(500);
        var player = altDriver.FindObject(By.NAME, "Player");
        var position = player.GetWorldPosition();
        dashButton.Click();
        Thread.Sleep(100);
        player = altDriver.FindObject(By.NAME, "Player");
        Assert.AreEqual(player.GetWorldPosition().x, position.x, 0.001f);
        Assert.AreEqual(player.GetWorldPosition().y, position.y, 0.001f);
        Assert.AreEqual(player.GetWorldPosition().z, position.z, 0.001f);
    }


    [Test]
    public void BuyInShop()
    {
        altDriver.LoadScene("TestSceneWithShop");
        Thread.Sleep(100);
        var player = altDriver.FindObject(By.NAME, "Player");
        bool doubleJump = player.GetComponentProperty<bool>("PlayerController", "canDoubleJump", "GameAssembly");
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
        doubleJump = player.GetComponentProperty<bool>("PlayerController", "canDoubleJump", "GameAssembly");

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
        AltObject optionsButton = altDriver.FindObject(By.NAME, "OptionsButton");

        optionsButton.Click();

        AltObject joystick = altDriver.FindObject(By.NAME, "Slider");
        var initialPosition = joystick.GetScreenPosition();
        int fingerId = altDriver.BeginTouch(initialPosition);
        var startAudio = DataHolder.musicVolume;
        Thread.Sleep(2000);
        AltVector2 newPosition = new AltVector2(initialPosition.x - 100, initialPosition.y);
        altDriver.MoveTouch(fingerId, newPosition);
        Thread.Sleep(2000);
        altDriver.EndTouch(fingerId);

        Assert.Less(DataHolder.musicVolume, startAudio);
    }

    [Test]
    public void EditVolumeFromPauseTest()
    {
        altDriver.LoadScene("Menu");
        AltObject playButton = altDriver.FindObject(By.NAME, "PlayButton");

        playButton.Click();

        AltObject pauseButton = altDriver.FindObject(By.NAME, "PauseButton");

        pauseButton.Click();

        
        AltObject optionsButton = altDriver.FindObject(By.NAME, "OptionsButton");

        optionsButton.Click();

        AltObject joystick = altDriver.FindObject(By.NAME, "Slider");
        var initialPosition = joystick.GetScreenPosition();
        int fingerId = altDriver.BeginTouch(initialPosition);
        var startAudio = DataHolder.musicVolume;
        Thread.Sleep(2000);
        AltVector2 newPosition = new AltVector2(initialPosition.x - 500, initialPosition.y);
        altDriver.MoveTouch(fingerId, newPosition);
        Thread.Sleep(2000);
        altDriver.EndTouch(fingerId);
        Thread.Sleep(2000);
        Assert.Less(DataHolder.musicVolume, startAudio);
        
        AltObject backButton = altDriver.FindObject(By.NAME, "BackButton");

        backButton.Click();
        
        AltObject continueButton = altDriver.FindObject(By.NAME, "ContinueButton");

        continueButton.Click();
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

        AltVector2 newPosition = new AltVector2(initialPositionOfJoystick.x + 10, initialPositionOfJoystick.y);
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
    public void KillerJumpCatTest() //done
    {
        altDriver.LoadScene("KillerJumpCatTest");

        Thread.Sleep(100);

        var player = altDriver.FindObject(By.NAME, "Player");

        var position = player.GetWorldPosition();

        AltObject joystick = altDriver.FindObject(By.NAME, "Fixed Joystick");
        var initialPositionOfJoystick = joystick.GetScreenPosition();
        int fingerId = altDriver.BeginTouch(initialPositionOfJoystick);

        AltVector2 newPosition = new AltVector2(initialPositionOfJoystick.x - 10, initialPositionOfJoystick.y);
        altDriver.MoveTouch(fingerId, newPosition);
        AltObject jumpButton = altDriver.FindObject(By.NAME, "Jump");
        jumpButton.Click();
        altDriver.EndTouch(fingerId);
        Thread.Sleep(900);

        newPosition = new AltVector2(initialPositionOfJoystick.x + 10, initialPositionOfJoystick.y);
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
        
        var player = altDriver.FindObject(By.NAME, "Player");
        
        AltObject joystick = altDriver.FindObject(By.NAME, "Fixed Joystick");
        var initialPositionOfJoystick = joystick.GetScreenPosition();
        int fingerId = altDriver.BeginTouch(initialPositionOfJoystick);

        AltVector2 newPosition = new AltVector2(initialPositionOfJoystick.x + 10, initialPositionOfJoystick.y);
        altDriver.MoveTouch(fingerId, newPosition);
        Thread.Sleep(500);
        altDriver.EndTouch(fingerId);

        Assert.AreEqual("Level 2", altDriver.GetCurrentScene());
       

        newPosition = new AltVector2(initialPositionOfJoystick.x, initialPositionOfJoystick.y);
        fingerId = altDriver.BeginTouch(initialPositionOfJoystick);
        altDriver.MoveTouch(fingerId, newPosition);
        altDriver.EndTouch(fingerId);

    }
}
