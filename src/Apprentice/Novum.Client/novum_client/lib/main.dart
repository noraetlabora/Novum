import 'dart:async';
import 'dart:io';

import 'package:flutter/foundation.dart';
import 'package:novum_client/popups/dialogs.dart';
import 'package:novum_client/services/authenticationService.dart';

import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:flutter_spinkit/flutter_spinkit.dart';
import 'package:novum_client/services/grpc.dart';
import 'package:novum_client/services/staticDataService.dart';
import 'package:novum_client/utils/utils.dart';

import 'screens/login.dart';

int kill = 0;

void main() {
  if (Platform.isWindows) {
    Utils.isWindows = true;
    debugDefaultTargetPlatformOverride = TargetPlatform.fuchsia;
  } else {
    Utils.isWindows = false;
    debugDefaultTargetPlatformOverride = TargetPlatform.android;
  }
  SystemChrome.setPreferredOrientations([DeviceOrientation.portraitUp]);
  runApp(Client());
}

class Client extends StatelessWidget {
  // This widget is the root of your application.
  @override
  Widget build(BuildContext context) {
    SystemChrome.setEnabledSystemUIOverlays([]);
    return MaterialApp(
      debugShowCheckedModeBanner: false,
      home: MyHomePage(title: 'initialize screen'),
    );
  }
}

class MyHomePage extends StatefulWidget {
  MyHomePage({Key key, this.title}) : super(key: key);
  final String title;

  @override
  Initialize createState() => Initialize();
}

class Initialize extends State<MyHomePage> {
  static bool isInit = false;
  static String ip = "127.0.0.1";
  static int port = 50051;

  bool init = false;
  @override
  Widget build(BuildContext context) {
    Grpc.set(ip, port);

    Timer.periodic(Duration(seconds: 3), (timer) async {
      Grpc.set(ip, port);

      if (kill != -1) {
        if (!init) {
          try {
            print("timer hit");
            var initReply = await AuthenticationService.initialize();
            var themeReply = await StaticDataService.theme();

            if (initReply) {
              kill = -1;
              init = true;
              Utils.setColors(
                  themeReply.primary,
                  themeReply.secondary,
                  themeReply.secondaryVariant,
                  themeReply.background,
                  themeReply.surface,
                  themeReply.onPrimary,
                  themeReply.onSecondary);
              timer.cancel();
              print("Timer canceled");
              Navigator.push(
                context,
                MaterialPageRoute(builder: (context) => LoginApp()),
              );
            }
          } catch (e) {
            init = null;
          }
        }
      }
    });

    return Scaffold(
      body: Container(
        color: Utils.colorScheme.background,
        child: Center(
          child: Column(
            mainAxisAlignment: MainAxisAlignment.center,
            children: <Widget>[
              SpinKitFadingCircle(
                color: Colors.black,
                size: 80.0,
                duration: Duration(milliseconds: 1200),
              ),
              Text(
                '\n \n Verbindung wird hergestellt',
              ),
            ],
          ),
        ),
      ),
      floatingActionButton: FloatingActionButton(
        onPressed: () => DialogSelection.inputDialog(context,
            "IP/Port Konfiguration", "IP Adresse:Port", "OK", "Abbruch"),
        tooltip: 'Increment',
        backgroundColor: Utils.colorScheme.primary,
        child: Icon(Icons.settings),
      ), // This trailing comma makes auto-formatting nicer for build methods.
    );
  }

  static void changeIp(String newConfig) {
    var split = newConfig.split(":");
    ip = split[0];
    port = int.parse(split[1]);
    Grpc.set(ip, port);
  }
}
