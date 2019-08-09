import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:novum_client/utils/utils.dart';
import 'package:novum_client/widgets/sidebar.dart';
import 'package:novum_client/widgets/pinpad.dart';
import 'package:novum_client/widgets/statusbar.dart';
import 'package:novum_client/widgets/bottombuttonbar.dart';

import '../main.dart';

void main() => runApp(LoginApp());

class LoginApp extends StatelessWidget {
  Widget build(BuildContext context) {
    Initialize.isInit = true;
    return MaterialApp(
      debugShowCheckedModeBanner: false,
      home: Login(),
    );
  }
}

class Login extends StatelessWidget {
  static String pin = "";
  bool tof = true;
  static BuildContext context1;
  @override
  Widget build(BuildContext context) {
    context1 = context;
    SystemChrome.setEnabledSystemUIOverlays([]);
    double width = MediaQuery.of(context).size.width;
    double heigth = MediaQuery.of(context).size.height;

    return new Scaffold(
      primary: false,
      body: Column(
        crossAxisAlignment: CrossAxisAlignment.end,
        mainAxisAlignment: MainAxisAlignment.end,
        mainAxisSize: MainAxisSize.max,
        children: <Widget>[
          new StatusBar(),
          new Container(
              width: width,
              height: heigth * 0.3684,
              decoration: new BoxDecoration(
                color: Utils.colorScheme.background,
                image: new DecorationImage(
                  image: ExactAssetImage('assets/NovaTouch-logo.png'),
                  fit: BoxFit.cover,
                ),
              )),
          new PinPad(hide: true, hintText: "PIN eingeben"),
          new BottomButtonBar(
            amount: 1,
            text: "OK",
            id: "login",
          ),
        ],
      ),
      drawer: new SideBar(),
    );
  }
}
