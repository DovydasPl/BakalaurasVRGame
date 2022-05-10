using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class BottleTests
{
   
    [Test]
    public void BottlePickedUp()
    {
        var gameObject = new GameObject();
        var bottle = gameObject.AddComponent<Bottle>();

        bottle.PickedUp();

        Assert.AreEqual(bottle.WasPickedUp(), true);
    }

    [Test]
    public void BottleSwitchedLabels()
    {
        var gameObject = new GameObject();
        var bottle = gameObject.AddComponent<Bottle>();

        bottle.regularLabel = new GameObject();
        bottle.letterLabel = new GameObject();

        bottle.SwitchLabels();

        Assert.AreEqual(bottle.regularLabel.activeSelf, false);
        Assert.AreEqual(bottle.letterLabel.activeSelf, true);
    }
}
