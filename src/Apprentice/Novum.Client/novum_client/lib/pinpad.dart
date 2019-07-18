import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:flutter/widgets.dart';

main() => runApp(Client());

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
  PinPad createState() => PinPad();
}

class PinPad extends State<MyHomePage> {
  @override
  Widget build(BuildContext context) {
    print("launched PinPad");
    double width = MediaQuery.of(context).size.width;

    return Scaffold(
      body: Column(
        crossAxisAlignment: CrossAxisAlignment.end,
        mainAxisAlignment: MainAxisAlignment.end,
        mainAxisSize: MainAxisSize.max,
        children: <Widget>[
          new Container(
              width: width,
              height:  MediaQuery.of(context).size.height / 3,
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
