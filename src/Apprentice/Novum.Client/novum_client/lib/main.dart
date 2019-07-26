import 'dart:async';

import 'package:novum_client/services/grpc.dart';
import 'package:novum_client/services/systemService.dart';

import "dialogs.dart";
import "login.dart";

import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:flutter_spinkit/flutter_spinkit.dart';

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
  _MyHomePageState createState() => _MyHomePageState();
}

class _MyHomePageState extends State<MyHomePage> {
  @override
  Widget build(BuildContext context) {
    Grpc.set("192.168.0.160", 50051);
    SystemService.ping();
    Timer(Duration(seconds: 3), () {
      if (kill == 0) {
        kill = -1;
        Navigator.push(
          context,
          MaterialPageRoute(builder: (context) => LoginApp()),
        );
      }

      //killed
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
            context, "Texteingabe", "Text", "OK", "Abbruch"),
        tooltip: 'Increment',
        child: Icon(Icons.settings),
      ), // This trailing comma makes auto-formatting nicer for build methods.
    );
  }
}
