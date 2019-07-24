import 'package:flutter/material.dart';
import 'package:flutter/rendering.dart';
import 'package:flutter/services.dart';
import 'package:novum_client/widgets/deviceinformationlist.dart';

class FClient extends StatelessWidget {
  // This widget is the root of your application.

  @override
  Widget build(BuildContext context) {
    SystemChrome.setEnabledSystemUIOverlays([]);
    double width = MediaQuery.of(context).size.width;
    double heigth = MediaQuery.of(context).size.height;
    return Scaffold(
      appBar: AppBar(
        title: Text("Information"),
      ),
      body: Column(
        mainAxisSize: MainAxisSize.max,
        children: <Widget>[
          Padding(
            padding: EdgeInsets.symmetric(horizontal: 18),
            child: new DeviceInfo(),
            ),
          Row(
            crossAxisAlignment: CrossAxisAlignment.end,
            children: <Widget>[
            ButtonTheme(
          buttonColor: Colors.yellow,
          minWidth: width / 2,
          height: heigth * 0.1015,
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
    );
  }
}