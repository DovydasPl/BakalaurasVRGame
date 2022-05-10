using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class CardTests
{
    [Test]
    public void CardPickedUp()
    {
        var gameObject = new GameObject();
        var card = gameObject.AddComponent<Card>();

        card.PickedUp();

        Assert.AreEqual(true, card.WasPickedUp());
    }
}
