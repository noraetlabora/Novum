import 'package:example_flutter/widgets/bottombuttonbar.dart';
import 'package:example_flutter/widgets/pinpad.dart';
import 'package:example_flutter/widgets/sidebar.dart';
import 'package:example_flutter/widgets/statusbar.dart';
import 'package:example_flutter/widgets/tableview.dart' as tv;
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';


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
            child: tv.Table(),
          ),
          new PinPad(hide: false, hintText: "Tischnummer"),
          new BottomButtonBar(text: "Rechnungen OK", amount: 2, id: "table")
        ],
      ),
      drawer: new SideBar(),
    );
  }
}
