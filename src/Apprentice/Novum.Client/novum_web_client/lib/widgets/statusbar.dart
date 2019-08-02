import 'package:flutter/material.dart';
import 'package:font_awesome_flutter/font_awesome_flutter.dart';

class StatusBar extends StatefulWidget {
  @override
  StatusBarState createState() => StatusBarState();
}

class StatusBarState extends State<StatusBar> {
  var batteryPercent = 0;
  bool killswitch = false;
  Icon icon =
      Icon(FontAwesomeIcons.batteryEmpty, color: Colors.white, size: 20);
  @override
  Widget build(BuildContext context) {
    double heigth = MediaQuery.of(context).size.height;
    double width = MediaQuery.of(context).size.width;

    return Container(
      height: heigth * 0.0469,
      color: Colors.black,
      child: Column(
        children: <Widget>[
          Row(
            mainAxisSize: MainAxisSize.max,
            children: <Widget>[
              Icon(
                Icons.person,
                color: Colors.white,
              ),
              Text(
                "Kassa 1",
                style: TextStyle(color: Colors.white),
              ),
            ],
          ),
        ],
      ),
    );
  }
}
