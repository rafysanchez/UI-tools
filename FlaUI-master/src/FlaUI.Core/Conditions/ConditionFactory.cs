﻿using FlaUI.Core.Definitions;

namespace FlaUI.Core.Conditions
{
    /// <summary>
    /// Helper class with some commonly used conditions.
    /// </summary>
    public class ConditionFactory
    {
        private readonly IPropertyLibrary _propertyLibrary;

        /// <summary>
        /// Creates a <see cref="ConditionFactory"/> with the given <see cref="IPropertyLibrary"/>.
        /// </summary>
        public ConditionFactory(IPropertyLibrary propertyLibrary)
        {
            _propertyLibrary = propertyLibrary;
        }

        /// <summary>
        /// Creates a condition to search by an automation id.
        /// </summary>
        public PropertyCondition ByAutomationId(string automationId)
        {
            return new PropertyCondition(_propertyLibrary.Element.AutomationId, automationId);
        }

        /// <summary>
        /// Creates a condition to search by a <see cref="ControlType"/>.
        /// </summary>
        public PropertyCondition ByControlType(ControlType controlType)
        {
            return new PropertyCondition(_propertyLibrary.Element.ControlType, controlType);
        }

        /// <summary>
        /// Creates a condition to search by a class name.
        /// </summary>
        public PropertyCondition ByClassName(string className)
        {
            return new PropertyCondition(_propertyLibrary.Element.ClassName, className);
        }

        /// <summary>
        /// Creates a condition to search by a name.
        /// </summary>
        public PropertyCondition ByName(string name)
        {
            return new PropertyCondition(_propertyLibrary.Element.Name, name);
        }

        /// <summary>
        /// Creates a condition to search by a text (same as <see cref="ByName"/>).
        /// </summary>
        public PropertyCondition ByText(string text)
        {
            return ByName(text);
        }

        /// <summary>
        /// Creates a condition to search by a process id.
        /// </summary>
        public PropertyCondition ByProcessId(int processId)
        {
            return new PropertyCondition(_propertyLibrary.Element.ProcessId, processId);
        }

        /// <summary>
        /// Creates a condition to search by a localized control type.
        /// </summary>
        public PropertyCondition ByLocalizedControlType(string localizedControlType)
        {
            return new PropertyCondition(_propertyLibrary.Element.LocalizedControlType, localizedControlType);
        }

        /// <summary>
        /// Creates a condition to search by a help text.
        /// </summary>
        public PropertyCondition ByHelpText(string helpText)
        {
            return new PropertyCondition(_propertyLibrary.Element.HelpText, helpText);
        }

        /// <summary>
        /// Creates a condition to search by a value.
        /// </summary>
        public PropertyCondition ByValue(string value)
        {
            return new PropertyCondition(_propertyLibrary.Value.Value, value);
        }

        /// <summary>
        /// Searches for a Menu/MenuBar.
        /// </summary>
        public OrCondition Menu()
        {
            return new OrCondition(ByControlType(ControlType.Menu), ByControlType(ControlType.MenuBar));
        }

        /// <summary>
        /// Searches for a DataGrid/List.
        /// </summary>
        public OrCondition Grid()
        {
            return new OrCondition(ByControlType(ControlType.DataGrid), ByControlType(ControlType.List));
        }

        /// <summary>
        /// Searches for a horizontal scrollbar.
        /// </summary>
        public AndCondition HorizontalScrollBar()
        {
            return new AndCondition(
                ByControlType(ControlType.ScrollBar),
                new OrCondition(
                    // WPF
                    ByAutomationId("HorizontalScrollBar"),
                    // WinForms UIA2
                    ByAutomationId("Horizontal ScrollBar"),
                    // WinForms UIA3
                    ByAutomationId("NonClientHorizontalScrollBar")
                )
            );
        }

        /// <summary>
        /// Searches for a vertical scrollbar.
        /// </summary>
        public AndCondition VerticalScrollBar()
        {
            return new AndCondition(
                ByControlType(ControlType.ScrollBar),
                new OrCondition(
                    // WPF
                    ByAutomationId("VerticalScrollBar"),
                    // WinForms UIA2
                    ByAutomationId("Vertical ScrollBar"),
                    // WinForms UIA3
                    ByAutomationId("NonClientVerticalScrollBar")
                )
            );
        }
    }
}
