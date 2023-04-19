using Altom.AltDriver;
using NUnit.Framework;
using UnityEngine;

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
}
