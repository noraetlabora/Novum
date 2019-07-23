import 'package:flutter/material.dart';
import 'package:novum_client/login.dart';

String pin;

class PinPadButton extends StatelessWidget {
  final String buttontext;
  PinPadButton({@required this.buttontext});

  Widget build(BuildContext context) {
    double width = MediaQuery.of(context).size.width;
    double heigth = MediaQuery.of(context).size.height;
    return ButtonTheme(
      minWidth: width / 3,
      height: heigth * 0.1172,
      child: RaisedButton(
        shape: new ContinuousRectangleBorder(),
        textColor: Colors.white,
        onPressed: () {},
        child: Text(
          buttontext,
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
      height: heigth * 0.1172,
      child: GestureDetector(
        onLongPress: () {
          tof = true;
          while (tof == true) {
            if (Login.pin.length == 0) {
              tof = false;
            }
          }
        },
        child: RaisedButton(
            onPressed: () {},
            color: Color.fromRGBO(100, 100, 100, 1.0),
            shape: new ContinuousRectangleBorder(),
            textColor: Colors.white,
            child: Column(
              children: <Widget>[Icon(Icons.backspace)],
            )),
      ),
    );
  }
}
