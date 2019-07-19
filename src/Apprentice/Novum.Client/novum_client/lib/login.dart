import 'dart:async';

import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:battery/battery.dart';

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

  @override
  Widget build(BuildContext context) {
    SystemChrome.setEnabledSystemUIOverlays([]);
    double width = MediaQuery.of(context).size.width;

    return Scaffold(
      body: Column(
        crossAxisAlignment: CrossAxisAlignment.end,
        mainAxisAlignment: MainAxisAlignment.end,
        mainAxisSize: MainAxisSize.max,
        children: <Widget>[
          Container(
            height: 30,
            color: Colors.black,
            child: Column(
              children: <Widget>[
                Row(
                  mainAxisSize: MainAxisSize.max,
                  children: <Widget>[
                    Icon(
                      Icons.person,
                      color: Colors.white,
                    ),
                    Center(
                      child: Text(
                        "Kassa 1",
                        style: TextStyle(color: Colors.white),
                      ),
                    ),
                  ],
                ),
                Row(
                  mainAxisAlignment: MainAxisAlignment.end,
                  mainAxisSize: MainAxisSize.max,
                  children: <Widget>[
                  
                  ],
                )
              ],
            ),
          ),
          new Container(
              width: width,
              height: MediaQuery.of(context).size.height / 3.34,
              decoration: new BoxDecoration(
                image: new DecorationImage(
                  image: ExactAssetImage('assets/NovaTouch-logo.jpg'),
                  fit: BoxFit.cover,
                ),
              )),
          Row(
            mainAxisAlignment: MainAxisAlignment.center,
            mainAxisSize: MainAxisSize.max,
            children: <Widget>[
              Container(
                color: Color.fromRGBO(200, 200, 200, 1.0),
                width: width,
                height: MediaQuery.of(context).size.height / 12,
                child: Center(
                  child: Text(
                    "PIN eingeben",
                    style: TextStyle(
                        fontSize: 24,
                        color: Color.fromRGBO(120, 120, 120, 1.0)),
                    textAlign: TextAlign.center,
                  ),
                ),
              ),
            ],
          ),
          Column(
            children: <Widget>[
              Row(
                mainAxisAlignment: MainAxisAlignment.center,
                children: <Widget>[
                  ButtonTheme(
                    minWidth: width / 3,
                    height: 75.0,
                    child: RaisedButton(
                      shape: new ContinuousRectangleBorder(),
                      textColor: Colors.white,
                      onPressed: () {},
                      child: Text(
                        "1",
                        style: TextStyle(fontSize: 17),
                      ),
                      color: Color.fromRGBO(100, 100, 100, 1.0),
                    ),
                  ),
                  ButtonTheme(
                    minWidth: width / 3,
                    height: 75.0,
                    child: RaisedButton(
                      shape: new ContinuousRectangleBorder(),
                      textColor: Colors.white,
                      onPressed: () {},
                      child: Text(
                        "2",
                        style: TextStyle(fontSize: 17),
                      ),
                      color: Color.fromRGBO(100, 100, 100, 1.0),
                    ),
                  ),
                  ButtonTheme(
                    minWidth: width / 3,
                    height: 75.0,
                    child: RaisedButton(
                      shape: new ContinuousRectangleBorder(),
                      textColor: Colors.white,
                      onPressed: () {},
                      child: Text(
                        "3",
                        style: TextStyle(fontSize: 17),
                      ),
                      color: Color.fromRGBO(100, 100, 100, 1.0),
                    ),
                  ),
                ],
              ),
              Row(
                mainAxisAlignment: MainAxisAlignment.center,
                children: <Widget>[
                  ButtonTheme(
                    minWidth: width / 3,
                    height: 75.0,
                    child: RaisedButton(
                      shape: new ContinuousRectangleBorder(),
                      textColor: Colors.white,
                      onPressed: () {},
                      child: Text(
                        "4",
                        style: TextStyle(fontSize: 17),
                      ),
                      color: Color.fromRGBO(100, 100, 100, 1.0),
                    ),
                  ),
                  ButtonTheme(
                    minWidth: width / 3,
                    height: 75.0,
                    child: RaisedButton(
                      shape: new ContinuousRectangleBorder(),
                      textColor: Colors.white,
                      onPressed: () {},
                      child: Text(
                        "5",
                        style: TextStyle(fontSize: 17),
                      ),
                      color: Color.fromRGBO(100, 100, 100, 1.0),
                    ),
                  ),
                  ButtonTheme(
                    minWidth: width / 3,
                    height: 75.0,
                    child: RaisedButton(
                      shape: new ContinuousRectangleBorder(),
                      textColor: Colors.white,
                      onPressed: () {},
                      child: Text(
                        "6",
                        style: TextStyle(fontSize: 17),
                      ),
                      color: Color.fromRGBO(100, 100, 100, 1.0),
                    ),
                  ),
                ],
              ),
              Row(
                mainAxisAlignment: MainAxisAlignment.center,
                children: <Widget>[
                  ButtonTheme(
                    minWidth: width / 3,
                    height: 75.0,
                    child: RaisedButton(
                      shape: new ContinuousRectangleBorder(),
                      textColor: Colors.white,
                      onPressed: () {},
                      child: Text(
                        "7",
                        style: TextStyle(fontSize: 17),
                      ),
                      color: Color.fromRGBO(100, 100, 100, 1.0),
                    ),
                  ),
                  ButtonTheme(
                    minWidth: width / 3,
                    height: 75.0,
                    child: RaisedButton(
                      shape: new ContinuousRectangleBorder(),
                      textColor: Colors.white,
                      onPressed: () {},
                      child: Text(
                        "8",
                        style: TextStyle(fontSize: 17),
                      ),
                      color: Color.fromRGBO(100, 100, 100, 1.0),
                    ),
                  ),
                  ButtonTheme(
                    minWidth: width / 3,
                    height: 75.0,
                    child: RaisedButton(
                      shape: new ContinuousRectangleBorder(),
                      textColor: Colors.white,
                      onPressed: () {},
                      child: Text(
                        "9",
                        style: TextStyle(fontSize: 17),
                      ),
                      color: Color.fromRGBO(100, 100, 100, 1.0),
                    ),
                  ),
                ],
              ),
              Row(
                mainAxisAlignment: MainAxisAlignment.center,
                children: <Widget>[
                  ButtonTheme(
                    minWidth: width / 3,
                    height: 75.0,
                    child: RaisedButton(
                      shape: new ContinuousRectangleBorder(),
                      textColor: Colors.white,
                      onPressed: () {},
                      child: Text(
                        "",
                        style: TextStyle(fontSize: 17),
                      ),
                      color: Color.fromRGBO(100, 100, 100, 1.0),
                    ),
                  ),
                  ButtonTheme(
                    minWidth: width / 3,
                    height: 75.0,
                    child: RaisedButton(
                      shape: new ContinuousRectangleBorder(),
                      textColor: Colors.white,
                      onPressed: () {},
                      child: Text(
                        "0",
                        style: TextStyle(fontSize: 17),
                      ),
                      color: Color.fromRGBO(100, 100, 100, 1.0),
                    ),
                  ),
                  ButtonTheme(
                    minWidth: width / 3,
                    height: 75.0,
                    child: RaisedButton(
                        color: Color.fromRGBO(100, 100, 100, 1.0),
                        shape: new ContinuousRectangleBorder(),
                        textColor: Colors.white,
                        onPressed: () {},
                        child: Column(
                          children: <Widget>[Icon(Icons.backspace)],
                        )),
                  ),
                ],
              ),
            ],
          ),
          Row(
            mainAxisSize: MainAxisSize
                .min, // this will take space as minimum as posible(to center)
            children: <Widget>[
              ButtonTheme(
                buttonColor: Colors.yellow,
                minWidth: width / 2,
                height: 65.0,
                child: RaisedButton(
                  onPressed: () {},
                  child: Text("Funktion"),
                ),
              ),
              ButtonTheme(
                buttonColor: Colors.yellow,
                minWidth: width / 2,
                height: 65.0,
                child: RaisedButton(
                  shape: new ContinuousRectangleBorder(),
                  onPressed: () {},
                  child: Text("OK"),
                ),
              ),
            ],
          ),
        ],
      ),
    );
  }
}
