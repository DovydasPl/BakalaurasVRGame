using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;

public class HintTests
{
    // A Test behaves as an ordinary method
    [Test]
    public void HintsGetConfigured()
    {
        var gameObject = new GameObject();
        var hintSystem = gameObject.AddComponent<HintSystem>();
        
        List<string[]> hints = new List<string[]>();

        hints.Add(new string[] {
            "Testas 1", 
            "Testas 2",
            "Testas 3"
        });

        hints.Add(new string[] {
            "Testas 4", 
            "Testas 5",
            "Testas 6"
        });

        hintSystem.Initialize();

        hintSystem.ConfigureHints(hints);

        Assert.AreEqual(hints, hintSystem.Hints());
    }

    [Test]
    public void HintIndexChanges()
    {
        var gameObject = new GameObject();
        var hintSystem = gameObject.AddComponent<HintSystem>();
        
        List<string[]> hints = new List<string[]>();

        hints.Add(new string[] {
            "Testas 1", 
            "Testas 2",
            "Testas 3"
        });

        hints.Add(new string[] {
            "Testas 4", 
            "Testas 5",
            "Testas 6"
        });

        hintSystem.Initialize();

        hintSystem.ConfigureHints(hints);

        hintSystem.SetHintIndex(1);

        Assert.AreEqual(1, hintSystem.HintSectionIndex());
    }

    [Test]
    public void HintShown()
    {
        var gameObject = new GameObject();
        var hintSystem = gameObject.AddComponent<HintSystem>();
        
        List<string[]> hints = new List<string[]>();

        hints.Add(new string[] {
            "Testas 1", 
            "Testas 2",
            "Testas 3"
        });

        hints.Add(new string[] {
            "Testas 4", 
            "Testas 5",
            "Testas 6"
        });

        hintSystem.Initialize();

        hintSystem.ConfigureHints(hints);

        hintSystem.SetHintIndex(0);

        
        for(int i = 0; i < 2; i++){
            hintSystem.ShowHint();
        }

        Assert.AreEqual("Testas 2", hintSystem.CurrentHint());
    }

    [Test]
    public void LastHintShown()
    {
        var gameObject = new GameObject();
        var hintSystem = gameObject.AddComponent<HintSystem>();
        
        List<string[]> hints = new List<string[]>();

        hints.Add(new string[] {
            "Testas 1", 
            "Testas 2",
            "Testas 3"
        });

        hints.Add(new string[] {
            "Testas 4", 
            "Testas 5",
            "Testas 6"
        });

        hintSystem.Initialize();

        hintSystem.ConfigureHints(hints);

        hintSystem.SetHintIndex(1);

        
        for(int i = 0; i < 10; i++){
            hintSystem.ShowHint();
        }

        Assert.AreEqual("Testas 6", hintSystem.CurrentHint());
    }
}
