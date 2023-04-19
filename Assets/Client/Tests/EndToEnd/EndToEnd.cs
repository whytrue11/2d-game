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

}
