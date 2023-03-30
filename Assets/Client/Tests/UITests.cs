using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;
using Object = UnityEngine.Object; 
public class UITests
{
        [UnityTest]
        public IEnumerator BackButtonSelectTest()
        {
            GameObject utils = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Utils.prefab");
            utils = GameObject.Instantiate(utils);
            GameObject canvas = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/Canvas.prefab");
            canvas = GameObject.Instantiate(canvas);
            BackButtonClick btn = canvas.transform.GetChild(4).transform.GetChild(3).GetComponent<BackButtonClick>();
            
            btn.OnButtonSelect();
            
            Assert.IsTrue(btn.GetComponent<Animation>().IsPlaying("Back_button_select"));
            yield return new WaitForSeconds(0.5f);
            Object.Destroy(utils);
            Object.Destroy(canvas);
        }  
        
        [UnityTest]
        public IEnumerator BackButtonDeSelectTest()
        {
            GameObject utils = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Utils.prefab");
            utils = GameObject.Instantiate(utils);
            GameObject canvas = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/Canvas.prefab");
            canvas = GameObject.Instantiate(canvas);
            BackButtonClick btn = canvas.transform.GetChild(4).transform.GetChild(3).GetComponent<BackButtonClick>();
            
            btn.OnButtonDeselect();
            
            Assert.IsTrue(btn.GetComponent<Animation>().IsPlaying("Back_button_deselect"));
            yield return new WaitForSeconds(0.5f);
            Object.Destroy(utils);
            Object.Destroy(canvas);
        } 
        
        /*[UnityTest]
        public IEnumerator BackPauseButtonClickTest()
        {
            GameObject utils = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Utils.prefab");
            utils = GameObject.Instantiate(utils);
            GameObject canvas = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/InGameCanvas.prefab");
            canvas = GameObject.Instantiate(canvas);
            
            yield return new WaitForSeconds(0.1f);
            canvas.transform.GetChild(4).transform.GetChild(3).GetComponent<BackPauseButtonClick>().OnButtonSelect();

            Assert.IsTrue(canvas.transform.GetChild(4).transform.GetChild(3).GetComponent<Animator>().GetBool("button_select"));
            yield return new WaitForSeconds(0.5f);
            Object.Destroy(utils);
            Object.Destroy(canvas);
        } */
        
        /*[UnityTest]
        public IEnumerator ContinueButtonClickTest()
        {
            GameObject utils = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Utils.prefab");
            utils = GameObject.Instantiate(utils);
            GameObject canvas = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/InGameCanvas.prefab");
            canvas = GameObject.Instantiate(canvas);
            ContinueButtonClick btn = canvas.transform.GetChild(2).transform.GetChild(1).GetComponent<ContinueButtonClick>();
            
            
            btn.OnButtonSelect();
            yield return new WaitForSeconds(0.1f);
            Assert.IsTrue(btn.GetComponent<Animator>().GetBool("button_select"));
            yield return new WaitForSeconds(0.5f);
            Object.Destroy(utils);
            Object.Destroy(canvas);
        }*/
        
        [UnityTest]
        public IEnumerator ExitButtonSelectTest()
        {
            GameObject utils = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Utils.prefab");
            utils = GameObject.Instantiate(utils);
            GameObject canvas = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/Canvas.prefab");
            canvas = GameObject.Instantiate(canvas);
            ExitButtonClick btn = canvas.transform.GetChild(2).transform.GetChild(4).GetComponent<ExitButtonClick>();
            
            btn.OnButtonSelect();
            
            Assert.IsTrue(btn.GetComponent<Animation>().IsPlaying("Exit_button_select"));
            yield return new WaitForSeconds(0.5f);
            Object.Destroy(utils);
            Object.Destroy(canvas);
        } 
        
        [UnityTest]
        public IEnumerator ExitButtonDeSelectTest()
        {
            GameObject utils = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Utils.prefab");
            utils = GameObject.Instantiate(utils);
            GameObject canvas = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/Canvas.prefab");
            canvas = GameObject.Instantiate(canvas);
            ExitButtonClick btn = canvas.transform.GetChild(2).transform.GetChild(4).GetComponent<ExitButtonClick>();
            
            btn.OnButtonDeselect();
            
            Assert.IsTrue(btn.GetComponent<Animation>().IsPlaying("Exit_button_deselect"));
            yield return new WaitForSeconds(0.5f);
            Object.Destroy(utils);
            Object.Destroy(canvas);
        } 
        
        [UnityTest]
        public IEnumerator MainMenuButtonSelectTest()
        {
            GameObject utils = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Utils.prefab");
            utils = GameObject.Instantiate(utils);
            GameObject canvas = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/EndGameCanvas.prefab");
            canvas = GameObject.Instantiate(canvas);
            canvas.transform.GetChild(6).transform.GetChild(3).GetComponent<MainMenuButtonClick>().OnButtonSelect();
            
            Assert.IsTrue(canvas.transform.GetChild(6).transform.GetChild(3).GetComponent<Animation>().IsPlaying("Menu_button_select"));
            yield return new WaitForSeconds(0.5f);
            Object.Destroy(utils);
            Object.Destroy(canvas);
        } 
        
        [UnityTest]
        public IEnumerator MainMenuButtonDeSelectTest()
        {
            GameObject utils = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Utils.prefab");
            utils = GameObject.Instantiate(utils);
            GameObject canvas = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/EndGameCanvas.prefab");
            canvas = GameObject.Instantiate(canvas);
            canvas.transform.GetChild(6).transform.GetChild(3).GetComponent<MainMenuButtonClick>().OnButtonDeselect();
            
            Assert.IsTrue(canvas.transform.GetChild(6).transform.GetChild(3).GetComponent<Animation>().IsPlaying("Menu_button_deselect"));
            yield return new WaitForSeconds(0.5f);
            Object.Destroy(utils);
            Object.Destroy(canvas);
        } 
        
        [UnityTest]
        public IEnumerator MainOptionsButtonSelectTest()
        {
            GameObject utils = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Utils.prefab");
            utils = GameObject.Instantiate(utils);
            GameObject canvas = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/Canvas.prefab");
            canvas = GameObject.Instantiate(canvas);
            MainOptionsButtonClick btn = canvas.transform.GetChild(2).transform.GetChild(3).GetComponent<MainOptionsButtonClick>();
            
            btn.OnButtonSelect();
            
            Assert.IsTrue(btn.GetComponent<Animation>().IsPlaying("Options_button_select"));
            yield return new WaitForSeconds(0.5f);
            Object.Destroy(utils);
            Object.Destroy(canvas);
        } 
        
        [UnityTest]
        public IEnumerator MainOptionsButtonDeSelectTest()
        {
            GameObject utils = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Utils.prefab");
            utils = GameObject.Instantiate(utils);
            GameObject canvas = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/Canvas.prefab");
            canvas = GameObject.Instantiate(canvas);
            MainOptionsButtonClick btn = canvas.transform.GetChild(2).transform.GetChild(3).GetComponent<MainOptionsButtonClick>();
            
            btn.OnButtonDeselect();
            
            Assert.IsTrue(btn.GetComponent<Animation>().IsPlaying("Options_button_deselect"));
            yield return new WaitForSeconds(0.5f);
            Object.Destroy(utils);
            Object.Destroy(canvas);
        }
        
        [UnityTest]
        public IEnumerator PlayButtonSelectTest()
        {
            GameObject utils = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Utils.prefab");
            utils = GameObject.Instantiate(utils);
            GameObject canvas = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/Canvas.prefab");
            canvas = GameObject.Instantiate(canvas);
            
            yield return new WaitForSeconds(0.1f);
            canvas.transform.GetChild(2).transform.GetChild(1).GetComponent<PlayButtonClick>().OnButtonSelect();
            
            Assert.IsTrue(canvas.transform.GetChild(2).transform.GetChild(1).GetComponent<Animation>().IsPlaying("Play_button_select"));
            yield return new WaitForSeconds(0.5f);
            
            Object.Destroy(utils);
            Object.Destroy(canvas);
        } 
        
        [UnityTest]
        public IEnumerator PlayButtonDeSelectTest()
        {
            GameObject utils = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Utils.prefab");
            utils = GameObject.Instantiate(utils);
            GameObject canvas = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/Canvas.prefab");
            canvas = GameObject.Instantiate(canvas);
            
            yield return new WaitForSeconds(0.1f);
            canvas.transform.GetChild(2).transform.GetChild(1).GetComponent<PlayButtonClick>().OnButtonDeselect();
            
            Assert.IsTrue(canvas.transform.GetChild(2).transform.GetChild(1).GetComponent<Animation>().IsPlaying("Play_button_deselect"));
            yield return new WaitForSeconds(0.5f);
            
            Object.Destroy(utils);
            Object.Destroy(canvas);
        }
        
        [UnityTest]
        public IEnumerator RetryButtonSelectTest()
        {
            GameObject utils = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Utils.prefab");
            utils = GameObject.Instantiate(utils);
            GameObject canvas = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/EndGameCanvas.prefab");
            canvas = GameObject.Instantiate(canvas);
            
            yield return new WaitForSeconds(0.1f);
            canvas.transform.GetChild(6).transform.GetChild(2).GetComponent<RetryButtonClick>().OnButtonSelect();
            
            Assert.IsTrue(canvas.transform.GetChild(6).transform.GetChild(2).GetComponent<Animation>().IsPlaying("Retry_button_select"));
            yield return new WaitForSeconds(0.5f);
            
            Object.Destroy(utils);
            Object.Destroy(canvas);
        } 
        
        [UnityTest]
        public IEnumerator RetryButtonDeSelectTest()
        {
            GameObject utils = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Utils.prefab");
            utils = GameObject.Instantiate(utils);
            GameObject canvas = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/EndGameCanvas.prefab");
            canvas = GameObject.Instantiate(canvas);
            
            yield return new WaitForSeconds(0.1f);
            canvas.transform.GetChild(6).transform.GetChild(2).GetComponent<RetryButtonClick>().OnButtonDeselect();
            
            Assert.IsTrue(canvas.transform.GetChild(6).transform.GetChild(2).GetComponent<Animation>().IsPlaying("Retry_button_deselect"));
            yield return new WaitForSeconds(0.5f);
            
            Object.Destroy(utils);
            Object.Destroy(canvas);
        }
        
        [UnityTest]
        public IEnumerator ScoresButtonSelectTest()
        {
            GameObject utils = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Utils.prefab");
            utils = GameObject.Instantiate(utils);
            GameObject canvas = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/Canvas.prefab");
            canvas = GameObject.Instantiate(canvas);
            
            yield return new WaitForSeconds(0.1f);
            canvas.transform.GetChild(2).transform.GetChild(2).GetComponent<ScoresButtonClick>().OnButtonSelect();
            
            Assert.IsTrue(canvas.transform.GetChild(2).transform.GetChild(2).GetComponent<Animation>().IsPlaying("Scores_button_select"));
            yield return new WaitForSeconds(0.5f);
            
            Object.Destroy(utils);
            Object.Destroy(canvas);
        } 
        
        [UnityTest]
        public IEnumerator ScoresButtonDeSelectTest()
        {
            GameObject utils = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Utils.prefab");
            utils = GameObject.Instantiate(utils);
            GameObject canvas = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/Canvas.prefab");
            canvas = GameObject.Instantiate(canvas);
            
            yield return new WaitForSeconds(0.1f);
            canvas.transform.GetChild(2).transform.GetChild(2).GetComponent<ScoresButtonClick>().OnButtonDeselect();
            
            Assert.IsTrue(canvas.transform.GetChild(2).transform.GetChild(2).GetComponent<Animation>().IsPlaying("Scores_button_deselect"));
            yield return new WaitForSeconds(0.5f);
            
            Object.Destroy(utils);
            Object.Destroy(canvas);
        }

        [UnityTest]
        public IEnumerator TitreTest()
        {
            GameObject utils = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestUtils.prefab");
            utils = GameObject.Instantiate(utils);
         
            yield return new WaitForSeconds(0.1f);

            Assert.IsFalse(utils.transform.GetChild(0).transform.GetChild(5).gameObject.activeSelf);
            utils.GetComponent<GameManager>().End(false);

            yield return new WaitForSeconds(1.5f);

            Assert.IsTrue(utils.transform.GetChild(0).transform.GetChild(5).gameObject.activeSelf);

            yield return new WaitForSeconds(1.0f);

            Object.Destroy(utils);
        }
}
