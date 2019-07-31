import 'dart:async';

import 'package:android_device_info/android_device_info.dart';
import 'package:battery/battery.dart';
import 'package:flutter/material.dart';
import 'package:font_awesome_flutter/font_awesome_flutter.dart';
import 'package:permission_handler/permission_handler.dart';

class StatusBar extends StatefulWidget {
  @override
  StatusBarState createState() => StatusBarState();
}

class StatusBarState extends State<StatusBar> {
  var battery = Battery();
  var batteryPercent = 0;
  bool killswitch = false;
  Icon icon =
      Icon(FontAwesomeIcons.batteryEmpty, color: Colors.white, size: 20);
  @override
  Widget build(BuildContext context) {
    double heigth = MediaQuery.of(context).size.height;
    if (!killswitch) {
      getIcon();
      killswitch = true;
    }

    Timer.periodic(const Duration(minutes: 10), (timer) async {
      getIcon();
    });

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
              Row(
                mainAxisAlignment: MainAxisAlignment.end,
                mainAxisSize: MainAxisSize.max,
                children: <Widget>[
                  icon,
                ],
              )
            ],
          ),
        ],
      ),
    );
  }

  Future<void> getIcon() async {
    try {
      batteryPercent = await battery.batteryLevel;

      //print(batteryPercent);
      if (batteryPercent >= 80) {
        icon = Icon(
          FontAwesomeIcons.batteryFull,
          color: Colors.white,
          size: 20,
        );
      } else if (batteryPercent >= 60) {
        icon = Icon(
          FontAwesomeIcons.batteryThreeQuarters,
          color: Colors.white,
          size: 20,
        );
      } else if (batteryPercent >= 40) {
        icon = Icon(
          FontAwesomeIcons.batteryHalf,
          color: Colors.white,
          size: 20,
        );
      } else if (batteryPercent >= 5) {
        icon = Icon(
          FontAwesomeIcons.batteryQuarter,
          color: Colors.white,
          size: 20,
        );
      } else {
        icon = Icon(
          FontAwesomeIcons.batteryEmpty,
          color: Colors.white,
          size: 20,
        );
      }
      setState(() => {icon = icon});
    } catch (ex) {
      print(ex);
      throw ex;
    }
  }
}
