﻿using FlaUI.Core;
using FlaUI.Core.Definitions;
using FlaUI.Core.Identifiers;
using FlaUI.Core.Patterns;
using FlaUI.UIA2.Identifiers;
using UIA = System.Windows.Automation;

namespace FlaUI.UIA2.Patterns
{
    public class ScrollPattern : ScrollPatternBase<UIA.ScrollPattern>
    {
        public static readonly PatternId Pattern = PatternId.Register(AutomationType.UIA2, UIA.ScrollPattern.Pattern.Id, "Scroll", AutomationObjectIds.IsScrollPatternAvailableProperty);
        public static readonly PropertyId HorizontallyScrollableProperty = PropertyId.Register(AutomationType.UIA2, UIA.ScrollPattern.HorizontallyScrollableProperty.Id, "HorizontallyScrollable");
        public static readonly PropertyId HorizontalScrollPercentProperty = PropertyId.Register(AutomationType.UIA2, UIA.ScrollPattern.HorizontalScrollPercentProperty.Id, "HorizontalScrollPercent");
        public static readonly PropertyId HorizontalViewSizeProperty = PropertyId.Register(AutomationType.UIA2, UIA.ScrollPattern.HorizontalViewSizeProperty.Id, "HorizontalViewSize");
        public static readonly PropertyId VerticallyScrollableProperty = PropertyId.Register(AutomationType.UIA2, UIA.ScrollPattern.VerticallyScrollableProperty.Id, "VerticallyScrollable");
        public static readonly PropertyId VerticalScrollPercentProperty = PropertyId.Register(AutomationType.UIA2, UIA.ScrollPattern.VerticalScrollPercentProperty.Id, "VerticalScrollPercent");
        public static readonly PropertyId VerticalViewSizeProperty = PropertyId.Register(AutomationType.UIA2, UIA.ScrollPattern.VerticalViewSizeProperty.Id, "VerticalViewSize");

        public ScrollPattern(FrameworkAutomationElementBase frameworkAutomationElement, UIA.ScrollPattern nativePattern) : base(frameworkAutomationElement, nativePattern)
        {
        }

        public override void Scroll(ScrollAmount horizontalAmount, ScrollAmount verticalAmount)
        {
            NativePattern.Scroll((UIA.ScrollAmount)horizontalAmount, (UIA.ScrollAmount)verticalAmount);
        }

        public override void SetScrollPercent(double horizontalPercent, double verticalPercent)
        {
            NativePattern.SetScrollPercent(horizontalPercent, verticalPercent);
        }
    }

    public class ScrollPatternPropertyIds : IScrollPatternPropertyIds
    {
        public PropertyId HorizontallyScrollable => ScrollPattern.HorizontallyScrollableProperty;

        public PropertyId HorizontalScrollPercent => ScrollPattern.HorizontalScrollPercentProperty;

        public PropertyId HorizontalViewSize => ScrollPattern.HorizontalViewSizeProperty;

        public PropertyId VerticallyScrollable => ScrollPattern.VerticallyScrollableProperty;

        public PropertyId VerticalScrollPercent => ScrollPattern.VerticalScrollPercentProperty;

        public PropertyId VerticalViewSize => ScrollPattern.VerticalViewSizeProperty;
    }
}
