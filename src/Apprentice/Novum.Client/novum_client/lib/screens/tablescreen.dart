import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:novum_client/widgets/bottombuttonbar.dart';
import 'package:novum_client/widgets/pinpad.dart';
import 'package:novum_client/widgets/sidebar.dart';
import 'package:novum_client/widgets/statusbar.dart';
import 'package:novum_client/widgets/table.dart' as const1;

class TableScreen extends StatelessWidget {
  Widget build(BuildContext context) {
    SystemChrome.setEnabledSystemUIOverlays([]);
    double width = MediaQuery.of(context).size.width;
    double heigth = MediaQuery.of(context).size.height;

    return Scaffold(
      body: Column(
        crossAxisAlignment: CrossAxisAlignment.end,
        mainAxisAlignment: MainAxisAlignment.end,
        mainAxisSize: MainAxisSize.max,
        children: <Widget>[
          new StatusBar(),
          new Container(
            width: width,
            height: heigth * 0.3684,
            child: const1.Table(height: (heigth * 0.2995)),
          ),
          new PinPad(hide: false, hintText: "Tischnummer"),
          new BottomButtonBar(text: "Rechnungen OK", amount: 2, id: "table")
        ],
      ),
      drawer: new SideBar(),
    );
  }
}
