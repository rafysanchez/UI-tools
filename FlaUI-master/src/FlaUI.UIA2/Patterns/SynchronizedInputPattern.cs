﻿#if !NET35
using FlaUI.Core;
using FlaUI.Core.Definitions;
using FlaUI.UIA2.Identifiers;
using UIA = System.Windows.Automation;
#endif
using FlaUI.Core.Identifiers;
using FlaUI.Core.Patterns;

namespace FlaUI.UIA2.Patterns
{
#if !NET35
    public class SynchronizedInputPattern : SynchronizedInputPatternBase<UIA.SynchronizedInputPattern>
    {
        public static readonly PatternId Pattern = PatternId.Register(AutomationType.UIA2, UIA.SynchronizedInputPattern.Pattern.Id, "SynchronizedInput", AutomationObjectIds.IsSynchronizedInputPatternAvailableProperty);
        public static readonly EventId DiscardedEvent = EventId.Register(AutomationType.UIA2, UIA.SynchronizedInputPattern.InputDiscardedEvent.Id, "Discarded");
        public static readonly EventId ReachedOtherElementEvent = EventId.Register(AutomationType.UIA2, UIA.SynchronizedInputPattern.InputReachedOtherElementEvent.Id, "ReachedOtherElement");
        public static readonly EventId ReachedTargetEvent = EventId.Register(AutomationType.UIA2, UIA.SynchronizedInputPattern.InputReachedTargetEvent.Id, "ReachedTarget");

        public SynchronizedInputPattern(FrameworkAutomationElementBase frameworkAutomationElement, UIA.SynchronizedInputPattern nativePattern) : base(frameworkAutomationElement, nativePattern)
        {
        }
        
        public override void Cancel()
        {
            NativePattern.Cancel();
        }

        public override void StartListening(SynchronizedInputType inputType)
        {
            NativePattern.StartListening((UIA.SynchronizedInputType)inputType);
        }
    }

    public class SynchronizedInputPatternEventIds : ISynchronizedInputPatternEventIds
    {
        public EventId DiscardedEvent => SynchronizedInputPattern.DiscardedEvent;
        public EventId ReachedOtherElementEvent => SynchronizedInputPattern.ReachedOtherElementEvent;
        public EventId ReachedTargetEvent => SynchronizedInputPattern.ReachedTargetEvent;
    }
#else
    public class SynchronizedInputPatternEventIds : ISynchronizedInputPatternEventIds
    {
        public EventId DiscardedEvent => EventId.NotSupportedByFramework;
        public EventId ReachedOtherElementEvent => EventId.NotSupportedByFramework;
        public EventId ReachedTargetEvent => EventId.NotSupportedByFramework;
    }
#endif
}
