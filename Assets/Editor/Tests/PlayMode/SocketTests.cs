using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;

public class SocketTests
{
    // A Test behaves as an ordinary method
    [Test]
    public void BottleSocketHasCorrectBottle()
    {
        var gameObject = new GameObject();
        var socket = gameObject.AddComponent<SocketWithBottleCheck>();

        socket.Initialize();

        GameObject go = new GameObject();

        socket.SetSelectedGameObject(go);
        socket.SetTargetGameObject(go);

        socket.OnSelectEnter();

        Assert.AreEqual(true, socket.HasCorrectBottle());
    }

    [Test]
    public void BottleSocketHasIncorrectBottle()
    {
        var gameObject = new GameObject();
        var socket = gameObject.AddComponent<SocketWithBottleCheck>();

        socket.Initialize();

        GameObject go = new GameObject();
        GameObject go2 = new GameObject();

        socket.SetSelectedGameObject(go);
        socket.SetTargetGameObject(go2);

        socket.OnSelectEnter();

        Assert.AreEqual(false, socket.HasCorrectBottle());
    }

    [Test]
    public void BottleSocketHasBottlePlaced()
    {
        var gameObject = new GameObject();
        var socket = gameObject.AddComponent<SocketWithBottleCheck>();

        socket.Initialize();

        GameObject go = new GameObject();

        socket.SetSelectedGameObject(go);

        Assert.AreEqual(true, socket.HasBottlePlaced());
    }

    [Test]
    public void CardSocketHasCardInserted()
    {
        var gameObject = new GameObject();
        var socket = gameObject.AddComponent<SocketWithCardCheck>();

        socket.Initialize();

        socket.OnSelectEnter();

        Assert.AreEqual(true, socket.IsCardInserted());
    }

    [Test]
    public void KeySocketHasKeyInserted()
    {
        var gameObject = new GameObject();
        var socket = gameObject.AddComponent<SocketWithKeyCheck>();

        socket.Initialize();

        socket.OnSelectEnter();

        Assert.AreEqual(true, socket.IsKeyInserted());
    }

    [Test]
    public void KeySocketChangesKeyCollider()
    {
        var gameObject = new GameObject();
        var socket = gameObject.AddComponent<SocketWithKeyCheck>();
        
        GameObject keyChild = new GameObject();
        keyChild.AddComponent<MeshCollider>();

        GameObject key = new GameObject();

        keyChild.transform.parent = key.transform;

        keyChild.GetComponent<MeshCollider>().convex = true;    

        socket.Initialize();
        socket.SetTargetKey(key);
        socket.OnHoverEnter();

        Assert.AreEqual(true, keyChild.GetComponent<MeshCollider>().isTrigger);
    
        socket.OnHoverExit();

        Assert.AreEqual(false, keyChild.GetComponent<MeshCollider>().isTrigger);
    }
}
