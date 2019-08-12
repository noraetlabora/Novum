import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:novum_client/utils/utils.dart';
import 'package:novum_client/widgets/bottombuttonbar.dart';
import 'package:novum_client/widgets/buttons/bottombutton.dart';
import 'package:novum_client/widgets/pinpad.dart';
import 'package:novum_client/widgets/sidebar.dart';
import 'package:novum_client/widgets/statusbar.dart';
import 'package:novum_client/widgets/tableview.dart' as tv;

class TableScreen extends StatelessWidget {
  List<BottomButton> buttons = <BottomButton>[];
  Widget build(BuildContext context) {
    SystemChrome.setEnabledSystemUIOverlays([]);
    double width = MediaQuery.of(context).size.width;
    double heigth = MediaQuery.of(context).size.height;

    buttons.add(new BottomButton(
      text: "OK",
      id: "table",
    ));
    buttons.add(new BottomButton(
      text: "Rechnungen",
      id: "table",
    ));

    return Scaffold(
      body: Column(
        children: <Widget>[
          new StatusBar(),
          new Container(
            width: width,
            height: heigth * 0.3684,
            color: Utils.colorScheme.background,
            child: tv.Table(),
          ),
          new PinPad(hide: false, hintText: "Tischnummer"),
          new BottomButtonBar(
            buttons: buttons,
          )
        ],
      ),
      drawer: new SideBar(),
    );
  }
}
