import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:novum_client/utils/utils.dart';
import 'package:novum_client/widgets/buttons/bottombutton.dart';
import 'package:novum_client/widgets/sidebar.dart';
import 'package:novum_client/widgets/pinpad.dart';
import 'package:novum_client/widgets/statusbar.dart';
import 'package:novum_client/widgets/bottombuttonbar.dart';

void main() => runApp(LoginApp());

class LoginApp extends StatelessWidget {
  Widget build(BuildContext context) {
    return MaterialApp(
      debugShowCheckedModeBanner: false,
      home: Login(),
    );
  }
}

class Login extends StatelessWidget {
  static String pin = "";
  bool tof = true;
  List<BottomButton> buttons = <BottomButton>[];

  @override
  Widget build(BuildContext context) {
    SystemChrome.setEnabledSystemUIOverlays([]);
    double width = MediaQuery.of(context).size.width;
    double heigth = MediaQuery.of(context).size.height;

    buttons.add(new BottomButton(
      text: "OK",
      id: "login",
    ));

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
            buttons: buttons,
          ),
        ],
      ),
      drawer: new SideBar(),
    );
  }
}
