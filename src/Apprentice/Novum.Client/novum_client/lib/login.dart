import 'package:flutter/material.dart';
import 'package:flutter/services.dart';

void main() => runApp(LoginApp());

class LoginApp extends StatelessWidget{
  Widget build(BuildContext context) {
     SystemChrome.setEnabledSystemUIOverlays([]);
    return MaterialApp(
      theme: ThemeData(
        primarySwatch: Colors.yellow,
      ),
      home: Login(),
    );
  }
}

class Login extends StatelessWidget{

  @override
  Widget build(BuildContext context){
     double width = MediaQuery.of(context).size.width;
    return new Scaffold(
      body: new Center(
        child: new Row(
          mainAxisSize: MainAxisSize.min, // this will take space as minimum as posible(to center)
          children: <Widget>[
            Expanded(
              child: Align(
                alignment: Alignment.bottomCenter,
                  child: ButtonTheme(
                    minWidth: width/2,
                    height: 65.0,
                    child: RaisedButton(
                      onPressed: () {},
                      child: Text("Funktion"),
                    ),
                  ),
              ),
            ),
            Expanded(
              child: Align(
              alignment: Alignment.bottomCenter,
                  child: ButtonTheme(
                    minWidth: width/2,
                    height: 65.0,
                    child: RaisedButton(
                      onPressed: () {},
                      child: Text("OK"),
                    ),
                  ),
                ),
            ),
          ],
          ),
        ),
      );
  }
}