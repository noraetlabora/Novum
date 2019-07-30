import 'package:flutter/material.dart';
import 'package:auto_size_text/auto_size_text.dart';
import 'package:novum_client/screens/functions.dart';
import 'package:novum_client/screens/tablescreen.dart';
import 'package:novum_client/widgets/table.dart' as t;
import 'package:novum_client/widgets/pinpad.dart';
import 'package:novum_client/widgets/bottombuttonbar.dart';
import 'package:novum_client/widgets/tablebutton.dart';

class BottomButton extends StatelessWidget {
  final int amount;
  final String text;
  final BottomButtonBar bar;
  BottomButton(
      {@required this.amount, @required this.text, @required this.bar});

  Widget build(BuildContext context) {
    var id = bar.getId();
    double width = MediaQuery.of(context).size.width;
    double heigth = MediaQuery.of(context).size.height;
    return ButtonTheme(
      buttonColor: Colors.yellow,
      minWidth: width / amount,
      height: heigth * 0.1015,
      child: RaisedButton(
        onPressed: () {
          if (id == "login") {
            switch (text) {
              case "OK":
                if (PinPad.pin == "1234") {
                  reset();
                  Navigator.push(
                    context,
                    MaterialPageRoute(builder: (context) => TableScreen()),
                  );
                }

                break;
              case "Funktionen":
                Navigator.push(
                  context,
                  MaterialPageRoute(builder: (context) => Functions()),
                );
                break;
            }
          } else if (id == "table") {
            switch (text) {
              case "OK":
                t.Table.add(new TableButton(
                  height: heigth,
                  name: PinPad.pin,
                  price: 5.00,
                ));
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
