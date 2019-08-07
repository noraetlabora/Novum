import 'dart:ui';

import 'package:flutter/material.dart';

class Utils {
  static bool isWindows = false;
  static ColorScheme colorScheme = ColorScheme(
    primary: Color(0xFFFFEB3B),
    secondary: Color(0xFFC8C8C8),
    secondaryVariant: Color(0xFF646464),
    background: Color(0xFFFFFFFF),
    surface: Color(0xFF000000),
    onPrimary: Color(0xFF000000),
    onSecondary: Color(0xFFAAAAAA),
    
    onBackground: Colors.black,
    onError: Colors.red,
    error: Colors.black,
    onSurface: Colors.white,
    brightness: Brightness.light,
    primaryVariant: Colors.yellow[800],
    );
  static void setColors(
    int primary1,
    int secondary1,
    int secondaryVariant1,
    int background1,
    int surface1,
    int onPrimary1,
    int onSecondary1,
  ) {
    colorScheme = ColorScheme(
    primary: Color(primary1),
    secondary: Color(secondary1),
    secondaryVariant: Color(secondaryVariant1),
    background: Color(background1),
    surface: Color(surface1),
    onPrimary: Color(onPrimary1),
    onSecondary: Color(onSecondary1),

    onBackground: Colors.black,
    onError: Colors.red,
    error: Colors.black,
    onSurface: Colors.white,
    brightness: Brightness.light,
    primaryVariant: Colors.yellow[800],
    );
  }
}
