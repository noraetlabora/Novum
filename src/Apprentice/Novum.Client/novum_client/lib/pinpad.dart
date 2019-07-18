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
      theme: ThemeData(
        primarySwatch: Colors.grey,
      ),
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
    return new Column(
      
      crossAxisAlignment: CrossAxisAlignment.end,
      mainAxisSize: MainAxisSize.max,
      mainAxisAlignment: MainAxisAlignment.end,
      children: <Widget>[
        Column(
          children: <Widget>[
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
                        onPressed: () {},
                        child: Text(
                          "1",
                          style: TextStyle(fontSize: 17),
                        ),
                      ),
                    ),
                    ButtonTheme(
                      minWidth: width / 3,
                      height: 75.0,
                      child: RaisedButton(
                        shape: new ContinuousRectangleBorder(),
                        onPressed: () {},
                        child: Text(
                          "2",
                          style: TextStyle(fontSize: 17),
                        ),
                      ),
                    ),
                    ButtonTheme(
                      minWidth: width / 3,
                      height: 75.0,
                      child: RaisedButton(
                        shape: new ContinuousRectangleBorder(),
                        onPressed: () {},
                        child: Text(
                          "3",
                          style: TextStyle(fontSize: 17),
                        ),
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
                        onPressed: () {},
                        child: Text(
                          "4",
                          style: TextStyle(fontSize: 17),
                        ),
                      ),
                    ),
                    ButtonTheme(
                      minWidth: width / 3,
                      height: 75.0,
                      child: RaisedButton(
                        shape: new ContinuousRectangleBorder(),
                        onPressed: () {},
                        child: Text(
                          "5",
                          style: TextStyle(fontSize: 17),
                        ),
                      ),
                    ),
                    ButtonTheme(
                      minWidth: width / 3,
                      height: 75.0,
                      child: RaisedButton(
                        shape: new ContinuousRectangleBorder(),
                        onPressed: () {},
                        child: Text(
                          "6",
                          style: TextStyle(fontSize: 17),
                        ),
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
                        onPressed: () {},
                        child: Text(
                          "7",
                          style: TextStyle(fontSize: 17),
                        ),
                      ),
                    ),
                    ButtonTheme(
                      minWidth: width / 3,
                      height: 75.0,
                      child: RaisedButton(
                        shape: new ContinuousRectangleBorder(),
                        onPressed: () {},
                        child: Text(
                          "8",
                          style: TextStyle(fontSize: 17),
                        ),
                      ),
                    ),
                    ButtonTheme(
                      minWidth: width / 3,
                      height: 75.0,
                      child: RaisedButton(
                        shape: new ContinuousRectangleBorder(),
                        onPressed: () {},
                        child: Text(
                          "9",
                          style: TextStyle(fontSize: 17),
                        ),
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
                        onPressed: () {},
                        child: Text(
                          "",
                          style: TextStyle(fontSize: 17),
                        ),
                      ),
                    ),
                    ButtonTheme(
                      minWidth: width / 3,
                      height: 75.0,
                      child: RaisedButton(
                        shape: new ContinuousRectangleBorder(),
                        onPressed: () {},
                        child: Text(
                          "0",
                          style: TextStyle(fontSize: 17),
                        ),
                      ),
                    ),
                    ButtonTheme(
                      minWidth: width / 3,
                      height: 75.0,
                      child: RaisedButton(
                          shape: new ContinuousRectangleBorder(),
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
                    onPressed: () {},
                    child: Text("OK"),
                  ),
                ),
              ],
            ),
          ],
        ),
      ],
    );
  }
}
