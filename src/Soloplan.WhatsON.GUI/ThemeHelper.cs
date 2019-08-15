﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ThemeHelper.cs" company="Soloplan GmbH">
//   Copyright (c) Soloplan GmbH. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Soloplan.WhatsON.GUI
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text.RegularExpressions;
  using System.Windows;
  using System.Windows.Media;
  using MaterialDesignColors;
  using MaterialDesignThemes.Wpf;
  using Soloplan.WhatsON.GUI.Common.VisualConfig;

  /// <summary>
  /// Helper class for managing themes and color adjustments to the UI.
  /// </summary>
  internal class ThemeHelper
  {
    static ThemeHelper()
    {
      MainColor = Color.FromRgb(192, 0, 107);
    }

    public static Color MainColor { get; private set; }

    /// <summary>
    /// Initializes this instance.
    /// </summary>
    public void Initialize(MainColorSettings settings)
    {
      this.ApplySoloplanThemeColors(settings);
    }

    /// <summary>
    /// Applies the light or dark mode.
    /// </summary>
    /// <param name="isDark">if set to <c>true</c> dark mode will be applied.</param>
    public void ApplyLightDarkMode(bool isDark)
    {
      var resourceDictionary = Application.Current.Resources.MergedDictionaries.Where(rd => rd.Source != (Uri)null).SingleOrDefault(rd => Regex.Match(rd.Source.OriginalString, "(\\/MaterialDesignThemes.Wpf;component\\/Themes\\/MaterialDesignTheme\\.)((Light)|(Dark))").Success);
      if (resourceDictionary == null)
      {
        throw new ApplicationException("Unable to find Light/Dark base theme in Application resources.");
      }

      resourceDictionary["MaterialDesignPaper"] = new SolidColorBrush(isDark ? Color.FromRgb(45, 45, 48) : Color.FromRgb(249, 249, 249));
      var paletteHelper = new PaletteHelper();
      paletteHelper.SetLightDark(isDark);
    }

    /// <summary>
    /// Applies our favorite colors to current palette.
    /// </summary>
    private void ApplySoloplanThemeColors(MainColorSettings settings)
    {
      var paletteHelper = new PaletteHelper();

      var newPrimaryHues = new List<Hue>();
      MainColor = settings != null ? settings.GetColor() : Color.FromRgb(192, 0, 107);

      newPrimaryHues.Add(new Hue("Primary50", ChangeColorBrightness(MainColor, 0.5f), Color.FromRgb(255, 255, 255)));
      newPrimaryHues.Add(new Hue("Primary100", ChangeColorBrightness(MainColor, 0.4f), Color.FromRgb(255, 255, 255)));
      newPrimaryHues.Add(new Hue("Primary200", ChangeColorBrightness(MainColor, 0.3f), Color.FromRgb(255, 255, 255)));
      newPrimaryHues.Add(new Hue("Primary300", ChangeColorBrightness(MainColor, 0.2f), Color.FromRgb(255, 255, 255)));
      newPrimaryHues.Add(new Hue("Primary400", ChangeColorBrightness(MainColor, 0.1f), Color.FromRgb(255, 255, 255)));
      newPrimaryHues.Add(new Hue("Primary500", MainColor, Color.FromRgb(255, 255, 255)));
      newPrimaryHues.Add(new Hue("Primary600", ChangeColorBrightness(MainColor, -0.1f), Color.FromRgb(255, 255, 255)));
      newPrimaryHues.Add(new Hue("Primary700", ChangeColorBrightness(MainColor, -0.2f), Color.FromRgb(255, 255, 255)));
      newPrimaryHues.Add(new Hue("Primary800", ChangeColorBrightness(MainColor, -0.3f), Color.FromRgb(255, 255, 255)));
      newPrimaryHues.Add(new Hue("Primary900", ChangeColorBrightness(MainColor, -0.4f), Color.FromRgb(255, 255, 255)));

      var newAccentHues = new List<Hue>();
      newAccentHues.Add(new Hue("Accent100", ChangeColorBrightness(MainColor, 0.85f), Color.FromRgb(255, 255, 255)));
      newAccentHues.Add(new Hue("Accent200", ChangeColorBrightness(MainColor, 0.80f), Color.FromRgb(255, 255, 255)));
      newAccentHues.Add(new Hue("Accent400", ChangeColorBrightness(MainColor, 0.75f), Color.FromRgb(255, 255, 255)));
      newAccentHues.Add(new Hue("Accent700", ChangeColorBrightness(MainColor, 0.70f), Color.FromRgb(255, 255, 255)));

      var swatch = new Swatch("WhatsON", newPrimaryHues, newAccentHues);
      var palette = new Palette(swatch, swatch, 3, 5, 4, 2);
      paletteHelper.ReplacePalette(palette);
    }

    public static Color ChangeColorBrightness(Color color, float correctionFactor)
    {
      float red = (float)color.R;
      float green = (float)color.G;
      float blue = (float)color.B;

      if (correctionFactor < 0)
      {
        correctionFactor = 1 + correctionFactor;
        red *= correctionFactor;
        green *= correctionFactor;
        blue *= correctionFactor;
      }
      else
      {
        red = ((255 - red) * correctionFactor) + red;
        green = ((255 - green) * correctionFactor) + green;
        blue = ((255 - blue) * correctionFactor) + blue;
      }

      return Color.FromRgb((byte)red, (byte)green, (byte)blue);
    }
  }
}