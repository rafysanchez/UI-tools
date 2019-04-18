﻿using FlaUI.Core;
using FlaUI.Core.Identifiers;
using FlaUI.Core.Patterns;
using FlaUI.UIA2.Converters;
using FlaUI.UIA2.Identifiers;
using UIA = System.Windows.Automation;

namespace FlaUI.UIA2.Patterns
{
    public class TablePattern : TablePatternBase<UIA.TablePattern>
    {
        public static readonly PatternId Pattern = PatternId.Register(AutomationType.UIA2, UIA.TablePattern.Pattern.Id, "Table", AutomationObjectIds.IsTablePatternAvailableProperty);
        public static readonly PropertyId ColumnHeadersProperty = PropertyId.Register(AutomationType.UIA2, UIA.TablePattern.ColumnHeadersProperty.Id, "ColumnHeaders").SetConverter(AutomationElementConverter.NativeArrayToManaged);
        public static readonly PropertyId RowHeadersProperty = PropertyId.Register(AutomationType.UIA2, UIA.TablePattern.RowHeadersProperty.Id, "RowHeaders").SetConverter(AutomationElementConverter.NativeArrayToManaged);
        public static readonly PropertyId RowOrColumnMajorProperty = PropertyId.Register(AutomationType.UIA2, UIA.TablePattern.RowOrColumnMajorProperty.Id, "RowOrColumnMajor");

        public TablePattern(FrameworkAutomationElementBase frameworkAutomationElement, UIA.TablePattern nativePattern) : base(frameworkAutomationElement, nativePattern)
        {
        }
    }

    public class TablePatternPropertyIds : ITablePatternPropertyIds
    {
        public PropertyId ColumnHeaders => TablePattern.ColumnHeadersProperty;
        public PropertyId RowHeaders => TablePattern.RowHeadersProperty;
        public PropertyId RowOrColumnMajor => TablePattern.RowOrColumnMajorProperty;
    }
}
