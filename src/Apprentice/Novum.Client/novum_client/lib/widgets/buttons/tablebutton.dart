import 'package:flutter/material.dart';
import 'package:flutter/widgets.dart';

class TableButton extends StatelessWidget {
  TableButton(
      {@required this.height, @required this.price, @required this.name});
  String id;
  final double height;
  final double price;
  final String name;

  Widget build(BuildContext context) {
    double width = MediaQuery.of(context).size.width;
    return ButtonTheme(
      buttonColor: Colors.lightGreen,
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
