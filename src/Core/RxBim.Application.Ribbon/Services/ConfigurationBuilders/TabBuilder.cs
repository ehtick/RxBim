﻿namespace RxBim.Application.Ribbon.ConfigurationBuilders
{
    using System;
    using Microsoft.Extensions.Configuration;
    using RxBim.Shared;

    /// <summary>
    /// Represents a tab buileder.
    /// </summary>
    public class TabBuilder : ITabBuilder
    {
        private readonly RibbonBuilder _ribbonBuilder;

        /// <summary>
        /// Initializes a new instance of the <see cref="TabBuilder"/> class.
        /// </summary>
        /// <param name="name">Tab name.</param>
        /// <param name="ribbonBuilder">Ribbon builder.</param>
        public TabBuilder(string name, RibbonBuilder ribbonBuilder)
        {
            _ribbonBuilder = ribbonBuilder;
            BuildingTab.Name = name;
        }

        /// <summary>
        /// The tab to create configuration.
        /// </summary>
        public Tab BuildingTab { get; } = new();

        /// <inheritdoc />
        public IRibbonBuilder ReturnToRibbon()
        {
            return _ribbonBuilder;
        }

        /// <inheritdoc />
        public IPanelBuilder AddPanel(string panelTitle)
        {
            return AddPanelInternal(panelTitle);
        }

        /// <inheritdoc />
        public ITabBuilder AddAboutButton(
            string name,
            AboutBoxContent content,
            Action<IButtonBuilder>? builder = null,
            string? panelName = null)
        {
            var panel = new PanelBuilder(panelName ?? name, _ribbonBuilder, this);
            panel.AddAboutButton(name, content, builder);
            BuildingTab.Panels.Add(panel.BuildingPanel);
            return this;
        }

        /// <summary>
        /// Loads a tab from configuration.
        /// </summary>
        /// <param name="section">Tab config section.</param>
        internal void LoadFromConfig(IConfigurationSection section)
        {
            var panelsSection = section.GetSection(nameof(Tab.Panels));
            if (!panelsSection.Exists())
                return;

            foreach (var panelSection in panelsSection.GetChildren())
            {
                if (!panelsSection.Exists())
                    continue;
                var panelBuilder = AddPanelInternal(panelSection.GetSection(nameof(Panel.Name)).Value);
                panelBuilder.LoadFromConfig(panelSection);
            }
        }

        private PanelBuilder AddPanelInternal(string panelTitle)
        {
            var builder = new PanelBuilder(panelTitle, _ribbonBuilder, this);
            BuildingTab.Panels.Add(builder.BuildingPanel);
            return builder;
        }
    }
}