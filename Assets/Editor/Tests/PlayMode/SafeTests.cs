using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;

public class SafeTests
{
    GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Safe.prefab");

    // A Test behaves as an ordinary method
    [Test]
    public void SafeUnlocksWithCorrectCode()
    {
        var gameObject = new GameObject();
        var safe = gameObject.AddComponent<Safe>();

        safe.SetLockCombination("123456");

        safe.ButtonPressed("1");
        safe.ButtonPressed("2");
        safe.ButtonPressed("3");
        safe.ButtonPressed("4");
        safe.ButtonPressed("5");
        safe.ButtonPressed("6");

        Assert.AreEqual(true, safe.IsUnlocked());
    }

    [Test]
    public void SafeDoesNotUnlockWithIncorrectCode()
    {
        var gameObject = new GameObject();
        var safe = gameObject.AddComponent<Safe>();

        safe.SetLockCombination("123456");

        safe.ButtonPressed("5");
        safe.ButtonPressed("6");
        safe.ButtonPressed("2");
        safe.ButtonPressed("1");
        safe.ButtonPressed("7");
        safe.ButtonPressed("4");

        Assert.AreEqual(false, safe.IsUnlocked());
    }

    [Test]
    public void SafeCodeResetsAfterIncorrectCode()
    {
        var gameObject = new GameObject();
        var safe = gameObject.AddComponent<Safe>();

        safe.SetLockCombination("123456");

        safe.ButtonPressed("5");
        safe.ButtonPressed("6");
        safe.ButtonPressed("2");
        safe.ButtonPressed("1");
        safe.ButtonPressed("7");
        safe.ButtonPressed("4");

        Assert.AreEqual(true, safe.GetCurrentCombination().Length == 0);
    }

    [UnityTest]
    public IEnumerator SafeCodeResetsAfterSomeTime() {
        var gameObject = new GameObject();
        var safe = gameObject.AddComponent<Safe>();
        
        safe.SetResetThreshold(1f);
        safe.SetLockCombination("123456");

        safe.ButtonPressed("2");

        Assert.AreEqual(false, safe.GetCurrentCombination().Length == 0);

        yield return new WaitForSeconds(1f);

        Assert.AreEqual(true, safe.GetCurrentCombination().Length == 0);
    }

    [UnityTest]
    public IEnumerator SafeOpensAfterUnlock() {
        GameObject safe = Object.Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
        GameObject safeDoor = GameObject.Find("Safe Door");
        Safe safeScript = safeDoor.GetComponent<Safe>();

        safeScript.lockedObjects = null;
        safeScript.objectsInside = null;

        safeScript.Initialize();


        safeScript.OpenDoor();

        yield return new WaitForSeconds(2f);
        
        Assert.AreEqual(true, safeDoor.transform.rotation.y != 0f);
    }

}
