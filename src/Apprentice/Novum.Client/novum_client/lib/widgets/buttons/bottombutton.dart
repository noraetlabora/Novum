import 'package:flutter/material.dart';
import 'package:auto_size_text/auto_size_text.dart';
import 'package:novum_client/screens/functions.dart';
import 'package:novum_client/screens/tablescreen.dart';
import 'package:novum_client/services/authenticationService.dart';
import 'package:novum_client/utils/utils.dart';
import 'package:novum_client/widgets/pinpad.dart';

class BottomButton extends StatelessWidget {
  final String text;
  final String id;

  static double heigth;
  double width;

  BottomButton({@required this.text, @required this.id});

  Widget build(BuildContext context) {
    heigth = MediaQuery.of(context).size.height;
    return ButtonTheme(
      buttonColor: Utils.colorScheme.primary,
      minWidth: width,
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

                break;
            }
          } else if (id == "table") {
            switch (text) {
              case "OK":
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
        shape: RoundedRectangleBorder(borderRadius: new BorderRadius.circular(0.0)),
        child: Text(text, style: TextStyle(fontSize: 23),),
      ),
    );
  }

  void reset() {
    PinPad.pin = "";
    PinPad.tfController.text = "";
  }
}
