import 'package:flutter/material.dart';
import 'package:flutter/services.dart';

class FClient extends StatelessWidget {
  // This widget is the root of your application.

  @override
  Widget build(BuildContext context) {
    SystemChrome.setEnabledSystemUIOverlays([]);
    return MaterialApp(
      theme: ThemeData(
        primarySwatch: Colors.yellow,
      ),
      home: MyHomePage(),
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
    return Scaffold(
      appBar: AppBar(
        title: const Text("Funktionen"),
        actions: <Widget>[
          Column(
            mainAxisSize: MainAxisSize.max,
            children: <Widget>[
              ListView(
                children: <Widget>[
                  Container(
                    child: RaisedButton(
                      onPressed: () {},
                      child: Text("Sprache"),
                    ),
                  )
                ],
              ),
              Row(
                mainAxisSize: MainAxisSize.min,
                children: <Widget>[
                  ButtonTheme(
                buttonColor: Colors.yellow,
                child: RaisedButton(
                  onPressed: () {
                    Navigator.of(context).pop();
                  },
                  child: Text("Zurück"),
                ),
              ),
                ],
              )
            ],
          ),
        ],
      ),
    );
  }
}
