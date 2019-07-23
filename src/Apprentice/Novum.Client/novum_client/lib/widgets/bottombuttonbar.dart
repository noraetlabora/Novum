import 'package:flutter/material.dart';
import 'package:novum_client/screens/funktions.dart';

class BottomButtonBar extends StatelessWidget {
  Widget build(BuildContext context) {
    double width = MediaQuery.of(context).size.width;
    double heigth = MediaQuery.of(context).size.height;
    return Row(
      mainAxisSize: MainAxisSize.max, // this will take space as minimum as posible(to center)
      children: <Widget>[
        ButtonTheme(
          buttonColor: Colors.yellow,
          minWidth: width / 2,
          height: heigth * 0.1015,
          child: RaisedButton(
            onPressed: () {
              Navigator.push(
                context,
                MaterialPageRoute(builder: (context) => FClient()),
              );
            },
            child: Text("Funktionen"),
          ),
        ),
        ButtonTheme(
          buttonColor: Colors.yellow,
          minWidth: width / 2,
          height: heigth * 0.1015,
          child: RaisedButton(
            shape: new ContinuousRectangleBorder(),
            onPressed: () {},
            child: Text("OK"),
          ),
        ),
      ],
    );
  }
}
