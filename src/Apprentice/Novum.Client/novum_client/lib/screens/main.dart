import 'dart:async';

import 'package:novum_client/popups/dialogs.dart';
import 'package:novum_client/services/authenticationService.dart';

import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:flutter_spinkit/flutter_spinkit.dart';
import 'package:novum_client/services/grpc.dart';
import 'package:novum_client/services/systemService.dart';

import 'login.dart';

int kill = 0;

void main() {
  runApp(Client());
}

class Client extends StatelessWidget {
  // This widget is the root of your application.

  @override
  Widget build(BuildContext context) {
    SystemChrome.setEnabledSystemUIOverlays([]);
    return MaterialApp(
      title: 'novum_client',
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
  static String ip = "192.168.0.150";
  static int port = 50051;

  bool init = false;
  @override
  Widget build(BuildContext context) {
    Grpc.set(ip, port);
    SystemService.ping();

    Timer.periodic(Duration(seconds: 3), (timer) async {
      if (kill != -1) {
        if (init == false) {
          try {
            print("timer hit");
            var initReply = await AuthenticationService.initialize();

            if (initReply) {
              kill = -1;
              init = true;
              Navigator.push(
                context,
                MaterialPageRoute(builder: (context) => LoginApp()),
              );
            }
          } catch (e) {
            init = false;
          }
        }
      }
    });

    Timer.periodic(Duration(seconds: 1), (timer) {
      Grpc.set(ip, port);
    });

    return Scaffold(
      body: Container(
        color: Colors.white,
        child: Center(
          child: Column(
            mainAxisAlignment: MainAxisAlignment.center,
            children: <Widget>[
              SpinKitFadingCircle(color: Colors.black, size: 80.0),
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
