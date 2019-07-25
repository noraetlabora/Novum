import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:novum_client/widgets/pinpad.dart';
import 'package:novum_client/widgets/statusbar.dart';
import 'package:novum_client/widgets/bottombuttonbar.dart';

void main() => runApp(LoginApp());

class LoginApp extends StatelessWidget {
  Widget build(BuildContext context) {
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
  @override
  Widget build(BuildContext context) {
    //TextEditingController tfController = TextEditingController();
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
              height: heigth * 0.2995,
              decoration: new BoxDecoration(
                image: new DecorationImage(
                  image: ExactAssetImage('assets/NovaTouch-logo.jpg'),
                  fit: BoxFit.cover,
                ),
              )
            ),
          new PinPad(hide: true, hintText: "PIN eingeben",),
          new BottomButtonBar(amount: 2, text: "Funktionen OK",),
        ],
      ),
    );
  }
}
