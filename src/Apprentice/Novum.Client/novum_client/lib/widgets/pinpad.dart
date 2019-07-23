import 'package:flutter/material.dart';

import 'package:flutter/widgets.dart';
import 'package:novum_client/widgets/pinpadbutton.dart';

class PinPad extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    TextEditingController tfController = TextEditingController();
    double width = MediaQuery.of(context).size.width;
    double heigth = MediaQuery.of(context).size.height;
    return Column(
      mainAxisAlignment: MainAxisAlignment.start,
      mainAxisSize: MainAxisSize.min,
      children: <Widget>[
        Row(
          mainAxisAlignment: MainAxisAlignment.center,
          mainAxisSize: MainAxisSize.max,
          children: <Widget>[
            Container(
              color: Color.fromRGBO(200, 200, 200, 1.0),
              width: width,
              height: heigth * 0.0832,
              child: Center(
                  child: TextField(
                      controller: tfController,
                      decoration: InputDecoration(
                          hintText: "PIN eingeben", border: InputBorder.none),
                      textAlign: TextAlign.center,
                      enabled: false,
                      style: TextStyle(fontSize: 25),
                      obscureText: true)),
            ),
          ],
        ),
        Column(
          children: <Widget>[
            Row(
              mainAxisAlignment: MainAxisAlignment.center,
              children: <Widget>[
                PinPadButton(buttontext: "1"),
                PinPadButton(buttontext: "2"),
                PinPadButton(buttontext: "3"),
              ],
            ),
            Row(
              mainAxisAlignment: MainAxisAlignment.center,
              children: <Widget>[
                PinPadButton(buttontext: "4"),
                PinPadButton(buttontext: "5"),
                PinPadButton(buttontext: "6"),
              ],
            ),
            Row(
              mainAxisAlignment: MainAxisAlignment.center,
              children: <Widget>[
                PinPadButton(buttontext: "7"),
                PinPadButton(buttontext: "8"),
                PinPadButton(buttontext: "9"),
              ],
            ),
            Row(
              mainAxisAlignment: MainAxisAlignment.center,
              children: <Widget>[
                PinPadButton(buttontext: ""),
                PinPadButton(buttontext: "0"),
                PinPadBackspace(),
              ],
            ),
          ],
        )
      ],
    );
  }
}
