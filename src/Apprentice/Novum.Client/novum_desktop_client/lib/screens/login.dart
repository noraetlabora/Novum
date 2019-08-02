import 'package:example_flutter/widgets/bottombuttonbar.dart';
import 'package:example_flutter/widgets/pinpad.dart';
import 'package:example_flutter/widgets/sidebar.dart';
import 'package:example_flutter/widgets/statusbar.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';


import '../main.dart';

void main() => runApp(LoginApp());

class LoginApp extends StatelessWidget {
  Widget build(BuildContext context) {
    Initialize.isInit = true;
    return MaterialApp(
      theme: ThemeData(
        primarySwatch: Colors.yellow,
      ),
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
                image: new DecorationImage(
                  image: ExactAssetImage('assets/NovaTouch-logo.jpg'),
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
