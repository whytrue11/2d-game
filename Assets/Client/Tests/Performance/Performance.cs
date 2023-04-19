using System.Collections;
using NUnit.Framework;
using Unity.PerformanceTesting;
using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class PerformanceTest
{

    [UnityTest, Performance]
    public IEnumerator Rendering_MenuScene()
    {
        using(Measure.Scope("Setup.LoadScene"))
        {
            SceneManager.LoadScene("Menu");
        }
        yield return null;

        yield return Measure.Frames().Run();
    }
    
    [UnityTest, Performance]
    public IEnumerator Rendering_Level1Scene()
    {
        using(Measure.Scope("Setup.LoadScene"))
        {
            SceneManager.LoadScene("Level 1");
        }
        yield return null;

        yield return Measure.Frames().Run();
    }
    
    [UnityTest, Performance]
    public IEnumerator Rendering_Level2Scene()
    {
        //yield return new WaitForSeconds(1f);
        
        using(Measure.Scope("Setup.LoadScene"))
        {
            SceneManager.LoadScene("Level 2");
        }
        yield return null;

        yield return Measure.Frames().Run();
        
        //yield return new WaitForSeconds(1f);
    }
    
    
    [UnityTest, Performance]
    public IEnumerator Rendering_Level3Scene()
    {
        yield return new WaitForSeconds(1f);
        
        using(Measure.Scope("Setup.LoadScene"))
        {
            SceneManager.LoadScene("Level 3");
        }
        yield return null;

        yield return Measure.Frames().Run();
        
        yield return new WaitForSeconds(1f);
    }
}