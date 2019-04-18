﻿using System;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Input;
using FlaUI.Core.UITests.TestFramework;
using NUnit.Framework;

namespace FlaUI.Core.UITests.Elements
{
    [TestFixture(AutomationType.UIA2, TestApplicationType.WinForms)]
    [TestFixture(AutomationType.UIA2, TestApplicationType.Wpf)]
    [TestFixture(AutomationType.UIA3, TestApplicationType.WinForms)]
    [TestFixture(AutomationType.UIA3, TestApplicationType.Wpf)]
    public class TextBoxTests : UITestBase
    {
        public TextBoxTests(AutomationType automationType, TestApplicationType appType) : base(automationType, appType)
        {
        }

        [Test]
        public void DirectSetTest()
        {
            var window = App.GetMainWindow(Automation);
            var textBox = window.FindFirstDescendant(cf => cf.ByAutomationId("TextBox")).AsTextBox();
            var text = textBox.Text;
            Assert.That(text, Is.Empty);
            var textToSet = "Hello World";
            textBox.Text = textToSet;
            text = textBox.Text;
            Assert.That(text, Is.EqualTo(textToSet));
            textBox.Text = String.Empty;
        }

        [Test]
        public void EnterTest()
        {
            var window = App.GetMainWindow(Automation);
            var textBox = window.FindFirstDescendant(cf => cf.ByAutomationId("TextBox")).AsTextBox();
            var text = textBox.Text;
            Assert.That(text, Is.Empty);
            var textToSet = "Hello World";
            textBox.Enter(textToSet);
            Wait.UntilInputIsProcessed(TimeSpan.FromMilliseconds(500));
            text = textBox.Text;
            Assert.That(text, Is.EqualTo(textToSet));
            textBox.Text = String.Empty;
        }
    }
}
