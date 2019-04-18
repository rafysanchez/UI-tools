﻿using System;
using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.EventHandlers;
using UIA = System.Windows.Automation;

namespace FlaUI.UIA2.EventHandlers
{
    /// <summary>
    /// UIA2 implementation of a focus changed event handler.
    /// </summary>
    public class UIA2FocusChangedEventHandler : FocusChangedEventHandlerBase
    {
        public UIA.AutomationFocusChangedEventHandler EventHandler { get; }

        public UIA2FocusChangedEventHandler(AutomationBase automation, Action<AutomationElement> callAction) : base(automation, callAction)
        {
            EventHandler = HandleFocusChangedEvent;
        }

        private void HandleFocusChangedEvent(object sender, UIA.AutomationFocusChangedEventArgs automationFocusChangedEventArgs)
        {
            var frameworkElement = new UIA2FrameworkAutomationElement((UIA2Automation)Automation, (UIA.AutomationElement)sender);
            var senderElement = new AutomationElement(frameworkElement);
            HandleFocusChangedEvent(senderElement);
        }
    }
}
