import 'package:flutter/material.dart';
import 'package:flutter/widgets.dart';
import 'package:novum_client/services/protobuf/novum.pbgrpc.dart';

class TableButton extends StatelessWidget {
  TableButton(
      {@required this.height,
      @required this.price,
      @required this.name,
      @required this.state});
  String id;
  final double height;
  final double price;
  final String name;
  final TableState state;

  Widget build(BuildContext context) {
    double width = MediaQuery.of(context).size.width;

    Color c = Colors.white;

    switch (state) {
      case TableState.ORDERED:
        c = Colors.lightGreen;
        break;
      case TableState.WAITING:
        c = Colors.orange[400];
        break;
      case TableState.IMPATIENT:
        c = Colors.red;
        break;
    }

    return ButtonTheme(
      buttonColor: c,
      minWidth: width / 4,
      height: height / 3,
      child: RaisedButton(
          shape: new ContinuousRectangleBorder(
              side: BorderSide(color: Colors.white, width: 0.4)),
          textColor: Colors.white,
          onPressed: () {},
          // child: Text(
          //   id,
          //   style: TextStyle(fontSize: 20),
          // ),
          child: GridTile(
            footer: Text(price.toString()),
            child: Text(
              name,
              style: TextStyle(fontWeight: FontWeight.w300, fontSize: 40),
            ),
          )),
    );
  }
}
