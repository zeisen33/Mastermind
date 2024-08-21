using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mastermind;
using System;

namespace Mastermind_Tests;

[TestClass]
public class TestCompareGuessToCode
{
    [TestMethod]
    public void ThrowsErrorForBadInput() {
        Assert.ThrowsException<Exception>(() => Mastermind.Game.CompareGuessToCode("7123","5555"));
        Assert.ThrowsException<Exception>(() => Mastermind.Game.CompareGuessToCode("0123","5555"));
        Assert.ThrowsException<Exception>(() => Mastermind.Game.CompareGuessToCode("f","5555"));
        Assert.ThrowsException<Exception>(() => Mastermind.Game.CompareGuessToCode("","5555"));
        Assert.ThrowsException<Exception>(() => Mastermind.Game.CompareGuessToCode("55555","5555"));
        Assert.ThrowsException<Exception>(() => Mastermind.Game.CompareGuessToCode("555","5555"));
    }

    [TestMethod]
    public void FindsCorrectSolutions() {
        Assert.AreEqual(Mastermind.Game.CompareGuessToCode("1234", "1234"), "+ + + + ");
        Assert.AreEqual(Mastermind.Game.CompareGuessToCode("5555", "5555"), "+ + + + ");
    }
    
    [TestMethod]

    public void AllIncorrect() {
        Assert.AreEqual(Mastermind.Game.CompareGuessToCode("1234", "5555"), "");
    }

    [TestMethod]

    public void MinusesOnly() {
        Assert.AreEqual(Mastermind.Game.CompareGuessToCode("5123", "1555"), "- - ");
        Assert.AreEqual(Mastermind.Game.CompareGuessToCode("6543", "3656"), "- - - ");
        Assert.AreEqual(Mastermind.Game.CompareGuessToCode("1234", "4321"), "- - - - ");
        Assert.AreEqual(Mastermind.Game.CompareGuessToCode("1234", "5551"), "- ");
        Assert.AreEqual(Mastermind.Game.CompareGuessToCode("5621", "6515"), "- - - ");   
        Assert.AreEqual(Mastermind.Game.CompareGuessToCode("3651", "5563"), "- - - ");   
    }

    [TestMethod]

    public void PlusesOnlyButFewerThanFour() {
        Assert.AreEqual(Mastermind.Game.CompareGuessToCode("1146", "1556"), "+ + ");
        Assert.AreEqual(Mastermind.Game.CompareGuessToCode("1123", "1423"), "+ + + ");
        Assert.AreEqual(Mastermind.Game.CompareGuessToCode("1423", "1123"), "+ + + ");
        Assert.AreEqual(Mastermind.Game.CompareGuessToCode("1223", "1123"), "+ + + ");
        Assert.AreEqual(Mastermind.Game.CompareGuessToCode("3263", "6563"), "+ + ");
        Assert.AreEqual(Mastermind.Game.CompareGuessToCode("5123", "5555"), "+ ");
        Assert.AreEqual(Mastermind.Game.CompareGuessToCode("5123", "5444"), "+ ");
        Assert.AreEqual(Mastermind.Game.CompareGuessToCode("5663", "5563"), "+ + + ");   
    }

    [TestMethod]

    public void PlusesAndMinuses() {
        Assert.AreEqual(Mastermind.Game.CompareGuessToCode("3262", "3656"), "+ - ");
        Assert.AreEqual(Mastermind.Game.CompareGuessToCode("3263", "3656"), "+ - ");
        Assert.AreEqual(Mastermind.Game.CompareGuessToCode("5663", "6563"), "+ + - - ");
        Assert.AreEqual(Mastermind.Game.CompareGuessToCode("5363", "6563"), "+ + - ");
        Assert.AreEqual(Mastermind.Game.CompareGuessToCode("5366", "6563"), "+ - - - ");
        Assert.AreEqual(Mastermind.Game.CompareGuessToCode("5361", "6563"), "+ - - ");
        Assert.AreEqual(Mastermind.Game.CompareGuessToCode("1142", "4123"), "+ - - ");
        Assert.AreEqual(Mastermind.Game.CompareGuessToCode("4234", "2334"), "+ + - ");
        Assert.AreEqual(Mastermind.Game.CompareGuessToCode("3241", "4321"), "+ - - - ");
    }
}