﻿using System;
using System.Drawing;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.AutomationElements.Infrastructure;
using FlaUI.Core.Conditions;
using FlaUI.Core.Definitions;
using FlaUI.Core.EventHandlers;
using FlaUI.Core.Exceptions;
using FlaUI.Core.Identifiers;

namespace FlaUI.Core
{
    /// <summary>
    /// Base class for a framework specific automation element.
    /// </summary>
    public abstract partial class FrameworkAutomationElementBase
    {
        /// <summary>
        /// Create a framework automation element with the given <see cref="AutomationBase"/>.
        /// </summary>
        /// <param name="automation">The <see cref="AutomationBase"/>.</param>
        protected FrameworkAutomationElementBase(AutomationBase automation)
        {
            Automation = automation;
        }

        /// <summary>
        /// Provides access to all <see cref="PropertyId"/>s.
        /// </summary>
        public IAutomationElementPropertyIds PropertyIdLibrary => Automation.PropertyLibrary.Element;

        /// <summary>
        /// Underlying <see cref="AutomationBase" /> object where this element belongs to.
        /// </summary>
        public AutomationBase Automation { get; }

        /// <summary>
        /// Gets the desired property value. Throws an exception if the property is not supported or is not cached during a cached request.
        /// </summary>
        /// <param name="property">The <see cref="PropertyId"/> of the property to get the value from.</param>
        /// <returns>The value of the property.</returns>
        public object GetPropertyValue(PropertyId property)
        {
            return GetPropertyValue<object>(property);
        }

        /// <summary>
        /// Gets the desired property value as the desired type. Throws an exception if the property is not supported or is not cached during a cached request.
        /// </summary>
        /// <typeparam name="T">The type of the value to get.</typeparam>
        /// <param name="property">The <see cref="PropertyId"/> of the property to get the value from.</param>
        /// <returns>The value of the property.</returns>
        public T GetPropertyValue<T>(PropertyId property)
        {
            if (Equals(property, PropertyId.NotSupportedByFramework))
            {
                throw new NotSupportedByFrameworkException();
            }
            var isCacheActive = CacheRequest.IsCachingActive;
            try
            {
                var value = InternalGetPropertyValue(property.Id, isCacheActive, false);
                if (value == Automation.NotSupportedValue)
                {
                    throw new PropertyNotSupportedException(property);
                }
                return property.Convert<T>(Automation, value);
            }
            catch (Exception ex)
            {
                if (isCacheActive)
                {
                    var cacheRequest = CacheRequest.Current;
                    if (!cacheRequest.Properties.Contains(property))
                    {
                        throw new PropertyNotCachedException(property, ex);
                    }
                }
                // Should actually never come here
                throw;
            }
        }

        /// <summary>
        /// Tries to get the property value. Throws an exception if the property is not cached during a cached request.
        /// </summary>
        /// <param name="property">The <see cref="PropertyId"/> of the property to get the value from.</param>
        /// <param name="value">The out object where the value should be put. Is the default if the property is not supported.</param>
        /// <returns>True if the property is supported and false otherwise.</returns>
        public bool TryGetPropertyValue(PropertyId property, out object value)
        {
            return TryGetPropertyValue<object>(property, out value);
        }

        /// <summary>
        /// Tries to get the property value as the desired type. Throws an exception if the property is not cached during a cached request.
        /// </summary>
        /// <typeparam name="T">The type of the value to get.</typeparam>
        /// <param name="property">The <see cref="PropertyId"/> of the property to get the value from.</param>
        /// <param name="value">The out object where the value should be put. Is the default if the property is not supported.</param>
        /// <returns>True if the property is supported and false otherwise.</returns>
        public bool TryGetPropertyValue<T>(PropertyId property, out T value)
        {
            if (Equals(property, PropertyId.NotSupportedByFramework))
            {
                throw new NotSupportedByFrameworkException();
            }
            var isCacheActive = CacheRequest.IsCachingActive;
            try
            {
                var internalValue = InternalGetPropertyValue(property.Id, isCacheActive, false);
                if (internalValue == Automation.NotSupportedValue)
                {
                    value = default(T);
                    return false;
                }
                value = property.Convert<T>(Automation, internalValue);
                return true;
            }
            catch (Exception ex)
            {
                if (isCacheActive)
                {
                    var cacheRequest = CacheRequest.Current;
                    if (!cacheRequest.Properties.Contains(property))
                    {
                        throw new PropertyNotCachedException(property, ex);
                    }
                }
                // Should actually never come here
                throw;
            }
        }

        /// <summary>
        /// Gets the native pattern object. Throws an exception if the pattern is not supported or is not cached during a cached request.
        /// </summary>
        /// <typeparam name="T">The type of the pattern to get.</typeparam>
        /// <param name="pattern">The <see cref="PatternId"/> of the pattern to get.</param>
        /// <returns>The native pattern.</returns>
        public T GetNativePattern<T>(PatternId pattern)
        {
            if (Equals(pattern, PatternId.NotSupportedByFramework))
            {
                throw new NotSupportedByFrameworkException();
            }
            var isCacheActive = CacheRequest.IsCachingActive;
            try
            {
                var nativePattern = InternalGetPattern(pattern.Id, isCacheActive);
                if (nativePattern == null)
                {
                    throw new InvalidOperationException("Native pattern is null");
                }
                return (T)nativePattern;
            }
            catch (Exception ex)
            {
                if (isCacheActive)
                {
                    var cacheRequest = CacheRequest.Current;
                    if (!cacheRequest.Patterns.Contains(pattern))
                    {
                        throw new PatternNotCachedException(pattern, ex);
                    }
                }
                throw new PatternNotSupportedException(pattern, ex);
            }
        }

        /// <summary>
        /// Tries to get the native pattern.
        /// </summary>
        /// <typeparam name="T">The type of the pattern to get.</typeparam>
        /// <param name="pattern">The <see cref="PatternId"/> of the pattern to get.</param>
        /// <param name="nativePattern">The out object where the pattern should be put. Is null if the pattern is not supported.</param>
        /// <returns>True if the pattern is supported and false otherwise.</returns>
        public bool TryGetNativePattern<T>(PatternId pattern, out T nativePattern)
        {
            try
            {
                nativePattern = GetNativePattern<T>(pattern);
                return true;
            }
            catch (PatternNotSupportedException)
            {
                nativePattern = default(T);
                return false;
            }
        }

        /// <summary>
        /// Gets the clickable point.
        /// </summary>
        /// <returns>The found clickable point.</returns>
        public Point GetClickablePoint()
        {
            if (!TryGetClickablePoint(out Point point))
            {
                throw new NoClickablePointException();
            }
            return point;
        }

        /// <summary>
        /// Sets the focus to the element.
        /// </summary>
        public abstract void SetFocus();

        /// <summary>
        /// Gets the desired property value.
        /// </summary>
        /// <param name="propertyId">The id of the property to get.</param>
        /// <param name="cached">Flag to indicate if the cached or current value should be fetched.</param>
        /// <param name="useDefaultIfNotSupported"> Flag to indicate, if the default value should be used if the property is not supported.</param>
        /// <returns>The value / default value of the property or <see cref="AutomationBase.NotSupportedValue" />.</returns>
        protected abstract object InternalGetPropertyValue(int propertyId, bool cached, bool useDefaultIfNotSupported);

        /// <summary>
        /// Gets the desired pattern.
        /// </summary>
        /// <param name="patternId">The id of the pattern to get.</param>
        /// <param name="cached">Flag to indicate if the cached or current pattern should be fetched.</param>
        /// <returns>The pattern or null if it was not found / cached.</returns>
        protected abstract object InternalGetPattern(int patternId, bool cached);

        /// <summary>
        /// Finds all elements in the given scope with the given condition.
        /// </summary>
        /// <param name="treeScope">The scope to search.</param>
        /// <param name="condition">The condition to use.</param>
        /// <returns>The found elements or an empty list if no elements were found.</returns>
        public abstract AutomationElement[] FindAll(TreeScope treeScope, ConditionBase condition);

        /// <summary>
        /// Finds the first element in the given scope with the given condition.
        /// </summary>
        /// <param name="treeScope">The scope to search.</param>
        /// <param name="condition">The condition to use.</param>
        /// <returns>The found element or null if no element was found.</returns>
        public abstract AutomationElement FindFirst(TreeScope treeScope, ConditionBase condition);

        /// <summary>
        /// Find all matching elements in the specified order.
        /// </summary>
        /// <param name="treeScope">A combination of values specifying the scope of the search.</param>
        /// <param name="condition">A condition that represents the criteria to match.</param>
        /// <param name="traversalOptions">Value specifying the tree navigation order.</param>
        /// <param name="root">An element with which to begin the search.</param>
        /// <returns>The found elements or an empty list if no elements were found.</returns>
        public abstract AutomationElement[] FindAllWithOptions(TreeScope treeScope, ConditionBase condition, TreeTraversalOptions traversalOptions, AutomationElement root);

        /// <summary>
        /// Finds the first matching element in the specified order.
        /// </summary>
        /// <param name="treeScope">A combination of values specifying the scope of the search.</param>
        /// <param name="condition">A condition that represents the criteria to match.</param>
        /// <param name="traversalOptions">Value specifying the tree navigation order.</param>
        /// <param name="root">An element with which to begin the search.</param>
        /// <returns>The found element or null if no element was found.</returns>
        public abstract AutomationElement FindFirstWithOptions(TreeScope treeScope, ConditionBase condition, TreeTraversalOptions traversalOptions, AutomationElement root);

        /// <summary>
        /// Finds the element with the given index with the given condition.
        /// </summary>
        /// <param name="treeScope">The scope to search.</param>
        /// <param name="index">The index of the element to return (0-based).</param>
        /// <param name="condition">The condition to use.</param>
        /// <returns>The found element or null if no element was found.</returns>
        public abstract AutomationElement FindIndexed(TreeScope treeScope, int index, ConditionBase condition);

        /// <summary>
        /// Tries to get a clickable point.
        /// </summary>
        public abstract bool TryGetClickablePoint(out Point point);

        /// <summary>
        /// Registers a method that handles the given automation event.
        /// </summary>
        public abstract AutomationEventHandlerBase RegisterAutomationEvent(EventId @event, TreeScope treeScope, Action<AutomationElement, EventId> action);

        /// <summary>
        /// Registers a method that handles a property changed event.
        /// </summary>
        public abstract PropertyChangedEventHandlerBase RegisterPropertyChangedEvent(TreeScope treeScope, Action<AutomationElement, PropertyId, object> action, PropertyId[] properties);

        /// <summary>
        /// Registers a method that handles a structure changed event.
        /// </summary>
        public abstract StructureChangedEventHandlerBase RegisterStructureChangedEvent(TreeScope treeScope, Action<AutomationElement, StructureChangeType, int[]> action);

        /// <summary>
        /// Registers a method that handles a notification event.
        /// </summary>
        public abstract NotificationEventHandlerBase RegisterNotificationEvent(TreeScope treeScope, Action<AutomationElement, NotificationKind, NotificationProcessing, string, string> action);

        /// <summary>
        /// Registers a method that handles a text edit text changed event.
        /// </summary>
        public abstract TextEditTextChangedEventHandlerBase RegisterTextEditTextChangedEventHandler(TreeScope treeScope, TextEditChangeType textEditChangeType, Action<AutomationElement, TextEditChangeType, string[]> action);

        /// <summary>
        /// Unregisters the given automation event handler.
        /// </summary>
        public abstract void UnregisterAutomationEventHandler(AutomationEventHandlerBase eventHandler);

        /// <summary>
        /// Unregisters the given property changed event handler.
        /// </summary>
        public abstract void UnregisterPropertyChangedEventHandler(PropertyChangedEventHandlerBase eventHandler);

        /// <summary>
        /// Unregisters the given structure changed event handler.
        /// </summary>
        public abstract void UnregisterStructureChangedEventHandler(StructureChangedEventHandlerBase eventHandler);

        /// <summary>
        /// Unregisters the given notification event handler.
        /// </summary>
        public abstract void UnregisterNotificationEventHandler(NotificationEventHandlerBase eventHandler);

        /// <summary>
        /// Unregisters the given text edit text changed event handler.
        /// </summary>
        public abstract void UnregisterTextEditTextChangedEventHandler(TextEditTextChangedEventHandlerBase eventHandler);

        public abstract PatternId[] GetSupportedPatterns();
        public abstract PropertyId[] GetSupportedProperties();
        public abstract AutomationElement GetUpdatedCache();
        public abstract AutomationElement[] GetCachedChildren();
        public abstract AutomationElement GetCachedParent();
        public abstract object GetCurrentMetadataValue(PropertyId targetId, int metadataId);
    }
}
