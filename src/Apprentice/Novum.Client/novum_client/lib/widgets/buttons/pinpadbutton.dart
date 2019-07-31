import 'package:flutter/material.dart';
import 'package:novum_client/widgets/pinpad.dart';

class PinPadButton extends StatelessWidget {
  final String buttonValue;
  PinPadButton({@required this.buttonValue});

  Widget build(BuildContext context) {
    double width = MediaQuery.of(context).size.width;
    double heigth = MediaQuery.of(context).size.height;
    return ButtonTheme(
      minWidth: width / 3,
      height: heigth * 0.1,
      child: RaisedButton(
        shape: new ContinuousRectangleBorder(
            side: BorderSide(color: Colors.white, width: 0.5)),
        textColor: Colors.white,
        onPressed: () {
          PinPad.onPressed(buttonValue);
        },
        child: Text(
          buttonValue,
          style: TextStyle(fontSize: 17),
        ),
        color: Color.fromRGBO(100, 100, 100, 1.0),
      ),
    );
  }
}

class PinPadBackspace extends StatelessWidget {
  Widget build(BuildContext context) {
    double width = MediaQuery.of(context).size.width;
    double heigth = MediaQuery.of(context).size.height;
    bool tof;
    return ButtonTheme(
      minWidth: width / 3,
      height: heigth * 0.1,
      child: GestureDetector(
        onLongPress: () {
          tof = true;
          while (tof == true) {
            PinPad.onPressed("BACKSPACE");
            if (PinPad.pin.length == 0) {
              tof = false;
            }
          }
        },
        child: RaisedButton(
            onPressed: () {
              PinPad.onPressed("BACKSPACE");
            },
            color: Color.fromRGBO(100, 100, 100, 1.0),
            shape: new ContinuousRectangleBorder(
                side: BorderSide(color: Colors.white, width: 0.5)),
            textColor: Colors.white,
            child: Column(
              children: <Widget>[Icon(Icons.backspace)],
            )),
      ),
    );
  }
}
