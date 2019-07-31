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
  @override
  Widget build(BuildContext context) {
    var battery = Battery();
    var batteryPercent;
    Icon batteryIcon = Icon(
      FontAwesomeIcons.batteryEmpty,
      color: Colors.white,
      size: 20,
    );
    Timer.periodic(const Duration(seconds: 10), (timer) async {
      try {
        batteryPercent = await battery.batteryLevel;
        Icon batteryIcon1 = Icon(
          FontAwesomeIcons.batteryEmpty,
          color: Colors.white,
          size: 20,
        );
        print(batteryPercent);
        if (batteryPercent >= 80) {
          batteryIcon1 = Icon(FontAwesomeIcons.batteryFull);
        } else if (batteryPercent >= 60) {
          batteryIcon1 = Icon(FontAwesomeIcons.batteryThreeQuarters);
        } else if (batteryPercent >= 40) {
          batteryIcon1 = Icon(FontAwesomeIcons.batteryHalf);
        } else if (batteryPercent >= 5) {
          batteryIcon1 = Icon(FontAwesomeIcons.batteryQuarter);
        } else {
          batteryIcon1 = Icon(FontAwesomeIcons.batteryEmpty);
        }
        setState(() => {batteryIcon = batteryIcon1});
      } catch (ex) {}
    });

    double heigth = MediaQuery.of(context).size.height;
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
                  batteryIcon,
                ],
              )
            ],
          ),
        ],
      ),
    );
  }
}
