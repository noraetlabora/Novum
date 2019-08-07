import 'package:flutter/material.dart';

import 'package:flutter/widgets.dart';
import 'package:novum_client/utils/utils.dart';

import 'buttons/pinpadbutton.dart';

class PinPad extends StatelessWidget {
  static TextEditingController tfController = TextEditingController();
  static String pin = "";

  PinPad({@required this.hide, @required this.hintText});
  final bool hide;
  final String hintText;

  @override
  Widget build(BuildContext context) {
    double width = MediaQuery.of(context).size.width;
    double heigth = MediaQuery.of(context).size.height;
    try {
      tfController.clear();
    } on Exception catch (e) {
      print("caught exception $e");
    }

    pin = "";

    return Column(
      mainAxisAlignment: MainAxisAlignment.start,
      mainAxisSize: MainAxisSize.min,
      children: <Widget>[
        Row(
          mainAxisAlignment: MainAxisAlignment.center,
          mainAxisSize: MainAxisSize.max,
          children: <Widget>[
            Container(
              color: Utils.colorScheme.secondary,
              width: width,
              height: heigth * 0.0832,
              child: Center(
                  child: TextField(
                      controller: tfController,
                      decoration: InputDecoration(
                          hintText: hintText, border: InputBorder.none),
                      textAlign: TextAlign.center,
                      enabled: false,
                      style: TextStyle(fontSize: 25),
                      obscureText: hide)),
            ),
          ],
        ),
        Column(
          children: <Widget>[
            Row(
              mainAxisAlignment: MainAxisAlignment.center,
              children: <Widget>[
                PinPadButton(buttonValue: "1"),
                PinPadButton(buttonValue: "2"),
                PinPadButton(buttonValue: "3"),
              ],
            ),
            Row(
              mainAxisAlignment: MainAxisAlignment.center,
              children: <Widget>[
                PinPadButton(buttonValue: "4"),
                PinPadButton(buttonValue: "5"),
                PinPadButton(buttonValue: "6"),
              ],
            ),
            Row(
              mainAxisAlignment: MainAxisAlignment.center,
              children: <Widget>[
                PinPadButton(buttonValue: "7"),
                PinPadButton(buttonValue: "8"),
                PinPadButton(buttonValue: "9"),
              ],
            ),
            Row(
              mainAxisAlignment: MainAxisAlignment.center,
              children: <Widget>[
                PinPadButton(buttonValue: ""),
                PinPadButton(buttonValue: "0"),
                PinPadBackspace(),
              ],
            ),
          ],
        )
      ],
    );
  }

  static void onPressed(String buttonValue) {
    pinText(buttonValue);
    tfController.text = pin;
  }

  static void pinText(String c) {
    if (c != "BACKSPACE" && c != null) {
      pin += c;
    } else {
      pin = removeLastCharacter(pin);
    }
  }
}

String removeLastCharacter(String s) {
  if (s.length > 0) {
    if (s.length == 1) {
      return "";
    } else {
      return s.substring(0, s.length - 1);
    }
  }
  return "";
}
