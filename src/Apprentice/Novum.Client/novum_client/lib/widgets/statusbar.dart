import 'dart:async';

import 'package:battery/battery.dart';
import 'package:flutter/material.dart';
import 'package:font_awesome_flutter/font_awesome_flutter.dart';
import 'package:novum_client/utils/utils.dart';

class StatusBar extends StatefulWidget {
  @override
  StatusBarState createState() => StatusBarState();
}

class StatusBarState extends State<StatusBar> {
  double heigth;
  double width;

  var battery = Battery();
  var batteryPercent = 0;
  bool killswitch = false;
  Icon icon =
      Icon(FontAwesomeIcons.batteryEmpty, color: Colors.white, size: 20);
  @override
  Widget build(BuildContext context) {
    heigth = MediaQuery.of(context).size.height;
    width = MediaQuery.of(context).size.width;
    if (!killswitch) {
      getIcon();
      killswitch = true;
    }

    Timer(Duration(seconds: 5), () async {
      getIcon();
    });

    Battery().onBatteryStateChanged.listen((BatteryState state) {
      if (state == BatteryState.charging) {
        setState(() {});
      }
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
              getBatteryIcon(),

            ],
          ),
        ],
      ),
    );
  }

  Future<void> getIcon() async {
    try {
      batteryPercent = await battery.batteryLevel;

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

  Padding getBatteryIcon() {
    return Padding(
        padding: EdgeInsets.fromLTRB(width - 110, 0, 0, 0),
        child: Row(
          mainAxisAlignment: MainAxisAlignment.end,
          mainAxisSize: MainAxisSize.max,
          children: <Widget>[
            icon,
          ],
        )
      );
  }
}
