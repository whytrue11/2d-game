using NUnit.Framework;

public class CoinTests
{
    [Test]
    public void GetCoins()
    {
        int expectedValue = 101;
        Coin coin = new Coin(expectedValue);
        
        Assert.AreEqual(expectedValue, coin.GetCoins());
    }
    
    [Test]
    public void SetCoins()
    {
        int expectedValue = 101;
        Coin coin = new Coin(0);
        
        coin.SetCoins(expectedValue);
        
        Assert.AreEqual(expectedValue, coin.GetCoins());
    }
    
    [Test]
    public void AddCoins()
    {
        int value = 101;
        int addCoins = 10;
        Coin coin = new Coin(value);
        int expectedValue = value + addCoins;
        
        coin.AddCoins(addCoins);
        
        Assert.AreEqual(expectedValue, coin.GetCoins());
    }
    
    [Test]
    public void AddNegativeCoins()
    {
        int value = 101;
        int addCoins = -10;
        Coin coin = new Coin(value);
        int expectedValue = value + addCoins;
        
        coin.AddCoins(addCoins);
        
        Assert.AreEqual(expectedValue, coin.GetCoins());
    }
    
    [Test]
    public void RemoveCoins()
    {
        int value = 101;
        int removeCoins = 10;
        Coin coin = new Coin(value);
        int expectedValue = value - removeCoins;
        
        coin.RemoveCoins(removeCoins);
        
        Assert.AreEqual(expectedValue, coin.GetCoins());
    }
    
    [Test]
    public void RemoveNegativeCoins()
    {
        int value = 101;
        int removeCoins = -10;
        Coin coin = new Coin(value);
        int expectedValue = value - removeCoins;
        
        coin.RemoveCoins(removeCoins);
        
        Assert.AreEqual(expectedValue, coin.GetCoins());
    }
}
