﻿using FlaUI.Core;
using FlaUI.Core.Identifiers;
using FlaUI.Core.Patterns;
using FlaUI.Core.Patterns.Infrastructure;
using FlaUI.Core.Tools;
using FlaUI.UIA3.Identifiers;
using UIA = Interop.UIAutomationClient;

namespace FlaUI.UIA3.Patterns
{
    public class ScrollItemPattern : PatternBase<UIA.IUIAutomationScrollItemPattern>, IScrollItemPattern
    {
        public static readonly PatternId Pattern = PatternId.Register(AutomationType.UIA3, UIA.UIA_PatternIds.UIA_ScrollItemPatternId, "ScrollItem", AutomationObjectIds.IsScrollItemPatternAvailableProperty);

        public ScrollItemPattern(FrameworkAutomationElementBase frameworkAutomationElement, UIA.IUIAutomationScrollItemPattern nativePattern) : base(frameworkAutomationElement, nativePattern)
        {
        }

        public void ScrollIntoView()
        {
            Com.Call(() => NativePattern.ScrollIntoView());
        }
    }
}
