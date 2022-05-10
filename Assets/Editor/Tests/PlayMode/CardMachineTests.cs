using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;

public class CardMachineTests
{
    GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/CardRegisterSetup.prefab");

    [UnityTest]
    public IEnumerator CardMachineOpens()
    {
        GameObject CardMachine = Object.Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
        GameObject cardReader = GameObject.Find("Card Reader");
        GameObject drawer = GameObject.Find("Drawer Parent");

        CardMachine cardMachineScript = cardReader.GetComponent<CardMachine>();

        cardMachineScript.lockedObjects = null;
        cardMachineScript.objectsInside = null;
        cardMachineScript.Initialize();

        cardMachineScript.CardInserted();

        yield return new WaitForSeconds(2.2f);

        Assert.AreEqual(true, drawer.transform.position.x != 0);
    }
}
