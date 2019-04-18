﻿using FlaUI.Core;
using FlaUI.Core.AutomationElements.Infrastructure;
using FlaUI.Core.Patterns;
using FlaUI.UIA2.Patterns;

namespace FlaUI.UIA2
{
    public class UIA2PropertyLibrary : IPropertyLibrary
    {
        public UIA2PropertyLibrary()
        {
            PatternAvailability = new UIA2AutomationElementPatternAvailabilityPropertyIds();
            Element = new UIA2AutomationElementPropertyIds();
            Annotation = new AnnotationPatternPropertyIds();
            Dock = new DockPatternPropertyIds();
            Drag = new DragPatternPropertyIds();
            DropTarget = new DropTargetPatternPropertyIds();
            ExpandCollapse = new ExpandCollapsePatternPropertyIds();
            GridItem = new GridItemPatternPropertyIds();
            Grid = new GridPatternPropertyIds();
            LegacyIAccessible = new LegacyIAccessiblePatternPropertyIds();
            MultipleView = new MultipleViewPatternPropertyIds();
            RangeValue = new RangeValuePatternPropertyIds();
            Scroll = new ScrollPatternPropertyIds();
            SelectionItem = new SelectionItemPatternPropertyIds();
            Selection = new SelectionPatternPropertyIds();
            Selection2 = new Selection2PatternPropertyIds();
            SpreadsheetItem = new SpreadsheetItemPatternPropertyIds();
            Styles = new StylesPatternPropertyIds();
            TableItem = new TableItemPatternPropertyIds();
            Table = new TablePatternPropertyIds();
            Toggle = new TogglePatternPropertyIds();
            Transform2 = new Transform2PatternPropertyIds();
            Transform = new TransformPatternPropertyIds();
            Value = new ValuePatternPropertyIds();
            Window = new WindowPatternPropertyIds();
        }

        public IAutomationElementPatternAvailabilityPropertyIds PatternAvailability { get; }
        public IAutomationElementPropertyIds Element { get; }
        public IAnnotationPatternPropertyIds Annotation { get; }
        public IDockPatternPropertyIds Dock { get; }
        public IDragPatternPropertyIds Drag { get; }
        public IDropTargetPatternPropertyIds DropTarget { get; }
        public IExpandCollapsePatternPropertyIds ExpandCollapse { get; }
        public IGridItemPatternPropertyIds GridItem { get; }
        public IGridPatternPropertyIds Grid { get; }
        public ILegacyIAccessiblePatternPropertyIds LegacyIAccessible { get; }
        public IMultipleViewPatternPropertyIds MultipleView { get; }
        public IRangeValuePatternPropertyIds RangeValue { get; }
        public IScrollPatternPropertyIds Scroll { get; }
        public ISelectionItemPatternPropertyIds SelectionItem { get; }
        public ISelectionPatternPropertyIds Selection { get; }
        public ISelection2PatternPropertyIds Selection2 { get; }
        public ISpreadsheetItemPatternPropertyIds SpreadsheetItem { get; }
        public IStylesPatternPropertyIds Styles { get; }
        public ITableItemPatternPropertyIds TableItem { get; }
        public ITablePatternPropertyIds Table { get; }
        public ITogglePatternPropertyIds Toggle { get; }
        public ITransform2PatternPropertyIds Transform2 { get; }
        public ITransformPatternPropertyIds Transform { get; }
        public IValuePatternPropertyIds Value { get; }
        public IWindowPatternPropertyIds Window { get; }
    }
}
