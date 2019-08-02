import 'package:example_flutter/services/protobuf/novum.pbgrpc.dart';
import 'package:flutter/material.dart';
import 'package:flutter/widgets.dart';
class TableButton extends StatelessWidget {
  TableButton(
      {@required this.height,
      @required this.amount,
      @required this.name,
      @required this.state,
      @required this.guests,
      @required this.waiterId,
      @required this.id});
  final String id;
  final double height;
  final double amount;
  final String name;
  final TableState state;
  final int guests;
  final String waiterId;

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
          child: GridTile(
            footer: Text(amount.toString()),
            child: Text(
              name,
              style: TextStyle(fontWeight: FontWeight.w300, fontSize: 40),
            ),
          )),
    );
  }
}
