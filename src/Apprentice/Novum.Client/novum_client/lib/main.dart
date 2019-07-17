import 'dart:async';
import "dialogs.dart";

import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:flutter_spinkit/flutter_spinkit.dart';

void main(){
  runApp(Client());
} 

class Client extends StatelessWidget {
  // This widget is the root of your application.

  
  @override
  Widget build(BuildContext context) {
    SystemChrome.setEnabledSystemUIOverlays([]);
    timer(5);
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

void timer(int _seconds){
  Timer(Duration(seconds: _seconds), () {
    // navigate to Login screen
});
}





class _MyHomePageState extends State<MyHomePage> {
  

  @override
  Widget build(BuildContext context) {
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
        onPressed: () => dialogSelection.errorDialog(404, "Page not found", 2, context),
        tooltip: 'Increment',
        child: Icon(Icons.settings),
      ), // This trailing comma makes auto-formatting nicer for build methods.
    );
  }


  
  
}


