﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProgressBarTooltipControl.xaml.cs" company="Soloplan GmbH">
//   Copyright (c) Soloplan GmbH. All rights reserved.
//   Licensed under the MIT License. See License-file in the project root for license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Soloplan.WhatsON.GUI.Common.BuildServer
{
  using System.Windows;
  using System.Windows.Controls;

  /// <summary>
  /// Interaction logic for ProgressInformationControl.xaml.
  /// </summary>
  public partial class ProgressBarTooltipControl : UserControl
  {
    /// <summary>
    /// Dependency property for <see cref="CulpritsProp"/>.
    /// </summary>
    public static readonly DependencyProperty ControlOrientationProperty = DependencyProperty.Register(nameof(ControlOrientation), typeof(Orientation), typeof(ProgressBarTooltipControl), new PropertyMetadata(Orientation.Vertical));

    public static readonly DependencyProperty CompactDisplayProperty = DependencyProperty.Register(nameof(CompactDisplay), typeof(bool), typeof(ProgressBarTooltipControl), new PropertyMetadata(false, new PropertyChangedCallback(OnCurrentReadingChanged)));

    public ProgressBarTooltipControl()
    {
      this.InitializeComponent();
    }

    private static void OnCurrentReadingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      if (d is ProgressBarTooltipControl control)
      {
        control.UpdateTexts();
      }
    }

    public Orientation ControlOrientation
    {
      get => (Orientation)this.GetValue(ControlOrientationProperty);
      set => this.SetValue(ControlOrientationProperty, value);
    }

    public bool CompactDisplay
    {
      get => (bool)this.GetValue(CompactDisplayProperty);
      set => this.SetValue(CompactDisplayProperty, value);
    }

    public void UpdateTexts()
    {
      if (this.CompactDisplay)
      {
        this.CompletionText.Text = string.Empty;
        this.EstimatedRemainingText.Text = " ETA";
        this.PercentSignText.Text = "%/";
      }
      else
      {
        this.CompletionText.Text = "Completion: ";
        this.EstimatedRemainingText.Text = " estimated remaining";
        this.PercentSignText.Text = "% ";
      }
    }
  }
}
