import 'dart:async';

import 'package:novum_client/login.dart';
import 'package:novum_client/services/authenticationService.dart';

import "dialogs.dart";

import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:flutter_spinkit/flutter_spinkit.dart';

import 'services/grpc.dart';
import 'services/systemService.dart';

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
  static String ip = "192.168.0.150";

  bool init = false;
  @override
  Widget build(BuildContext context) {
    Grpc.set(ip, 50051);
    SystemService.ping();

    Timer.periodic(Duration(seconds: 3), (timer) async {
      if (init == false) {
        try {
          print("timer hit");
          var initReply = await AuthenticationService.initialize();

          if (initReply) {
            Navigator.push(
              context,
              MaterialPageRoute(builder: (context) => LoginApp()),
            );
            init = true;
          }
        } catch (e) {
          init = false;
        }
      }
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
        onPressed: () => DialogSelection.inputDialog(
            context, "IP Konfiguration", "IP Adresse", "OK", "Abbruch"),
        tooltip: 'Increment',
        child: Icon(Icons.settings),
      ), // This trailing comma makes auto-formatting nicer for build methods.
    );
  }

  static void changeIp(String ipAddress) {
    ip = ipAddress;
    Grpc.set(ip, 50051);
  }
}
