import 'package:example_flutter/screens/functions.dart';
import 'package:example_flutter/screens/tablescreen.dart';
import 'package:example_flutter/services/authenticationService.dart';
import 'package:flutter/material.dart';
import 'package:auto_size_text/auto_size_text.dart';

import '../bottombuttonbar.dart';
import '../pinpad.dart';

class BottomButton extends StatelessWidget {
  final int amount;
  final String text;
  final BottomButtonBar bar;

  static double heigth;

  BottomButton(
      {@required this.amount, @required this.text, @required this.bar});

  Widget build(BuildContext context) {
    var id = bar.getId();
    double width = MediaQuery.of(context).size.width;
    heigth = MediaQuery.of(context).size.height;
    return ButtonTheme(
      buttonColor: Colors.yellow,
      minWidth: width / amount,
      height: heigth * 0.1015,
      child: RaisedButton(
        onPressed: () {
          if (id == "login") {
            switch (text) {
              case "OK":
                try {
                  AuthenticationService.login(PinPad.pin);
                  Navigator.push(
                    context,
                    MaterialPageRoute(builder: (context) => TableScreen()),
                  );
                } catch (e) {
                  throw e;
                }

                // if (PinPad.pin == "1234") {
                //   reset();
                //   Navigator.push(
                //     context,
                //     MaterialPageRoute(builder: (context) => TableScreen()),
                //   );
                // }

                break;
            }
          } else if (id == "table") {
            switch (text) {
              case "OK":
                // t.Table.add(new TableButton(
                //   height: heigth,
                //   name: PinPad.pin,
                //   price: 5.00,
                // ));
                reset();
                break;
              case "Funktionen":
                Navigator.push(
                  context,
                  MaterialPageRoute(builder: (context) => Functions()),
                );
                break;
            }
          }
        },
        child: AutoSizeText(text),
      ),
    );
  }

  void reset() {
    PinPad.pin = "";
    PinPad.tfController.text = "";
  }
}
