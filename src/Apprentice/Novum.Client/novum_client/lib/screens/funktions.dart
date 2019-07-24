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
      appBar: PreferredSize(
      preferredSize: Size.fromHeight(heigth*0.095),
      child: AppBar(
        backgroundColor: Colors.grey[400],
        title: Text("Information"),
      ),
      ),
      body: Column(
        mainAxisSize: MainAxisSize.min,
        mainAxisAlignment: MainAxisAlignment.end,
        children: <Widget>[
          Container(
          height: heigth*0.813,
          child: Padding(
            padding: EdgeInsets.symmetric(horizontal: 18),
            child: new DeviceInfo(),
          ),
          ),
          Row(
            crossAxisAlignment: CrossAxisAlignment.end,
            children: <Widget>[
              ButtonTheme(
                buttonColor: Colors.yellow[850],
                minWidth: width,
                height: heigth * 0.0915,
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
