﻿using FlaUI.Core;
using FlaUI.Core.Identifiers;
using FlaUI.Core.Patterns;
using FlaUI.Core.Tools;
using FlaUI.UIA3.Identifiers;
using UIA = Interop.UIAutomationClient;

namespace FlaUI.UIA3.Patterns
{
    /// <summary>
    /// Class for an UIA3 <see cref="IInvokePattern"/>.
    /// </summary>
    public class InvokePattern : InvokePatternBase<UIA.IUIAutomationInvokePattern>
    {
        /// <summary>
        /// The <see cref="PatternId"/> for this pattern.
        /// </summary>
        public static readonly PatternId Pattern = PatternId.Register(AutomationType.UIA3, UIA.UIA_PatternIds.UIA_InvokePatternId, "Invoke", AutomationObjectIds.IsInvokePatternAvailableProperty);

        /// <summary>
        /// The <see cref="EventId"/> for the invoked event.
        /// </summary>
        public static readonly EventId InvokedEvent = EventId.Register(AutomationType.UIA3, UIA.UIA_EventIds.UIA_Invoke_InvokedEventId, "Invoked");

        /// <summary>
        /// Creates an UIA3 <see cref="IInvokePattern"/>.
        /// </summary>
        public InvokePattern(FrameworkAutomationElementBase frameworkAutomationElement, UIA.IUIAutomationInvokePattern nativePattern) : base(frameworkAutomationElement, nativePattern)
        {
        }

        /// <inheritdoc />
        public override void Invoke()
        {
            Com.Call(() => NativePattern.Invoke());
        }
    }

    /// <summary>
    /// Class for UIA3 <see cref="IInvokePatternEventIds"/>.
    /// </summary>
    public class InvokePatternEventIds : IInvokePatternEventIds
    {
        /// <inheritdoc />
        public EventId InvokedEvent => InvokePattern.InvokedEvent;
    }
}
