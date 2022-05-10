using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;

public class DoorTests
{
    GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Door.prefab");

    [UnityTest]
    public IEnumerator DoorOpens()
    {
        GameObject allDoorObject = Object.Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
        GameObject openableDoorObject = GameObject.Find("Parent");
        Rigidbody rb = openableDoorObject.GetComponent<Rigidbody>();

        rb.isKinematic = false;
        rb.AddForce(3f, 0, 0, ForceMode.Impulse);

        yield return new WaitForSeconds(2f);
        
        Assert.AreEqual(true, openableDoorObject.GetComponent<Door>().IsOpened());
    }

    [Test]
    public void DoorLockTurns()
    {
        GameObject allDoorObject = Object.Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
        GameObject openableDoorObject = GameObject.Find("Parent");
        GameObject doorLock = GameObject.Find("sm_lock_03");
       
        openableDoorObject.GetComponent<Door>().Initialize();
        openableDoorObject.GetComponent<Door>().Unlock();
        
        Assert.AreEqual(false, openableDoorObject.GetComponent<Door>().GetAnimator().GetBool("isKeyInserted"));
    }
}
