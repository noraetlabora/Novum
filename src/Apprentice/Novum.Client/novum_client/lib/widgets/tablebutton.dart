import 'package:flutter/material.dart';
import 'package:flutter/widgets.dart';

class TableButton extends StatelessWidget {
  
  TableButton({@required this.id,
               @required this.height,
               @required this.price});
  final String id;
  final double height;
  final double price;
  String name;
  double amount;


  Widget build(BuildContext context) {
    double width = MediaQuery.of(context).size.width;
    return ButtonTheme(
      buttonColor: Colors.lightGreen,
      minWidth: width / 4,
      height: height / 3,
      child: RaisedButton(
        shape: new ContinuousRectangleBorder(side: BorderSide(color: Colors.white, width: 0.4)),
        textColor: Colors.white,
        onPressed: () {},
        // child: Text(
        //   id,
        //   style: TextStyle(fontSize: 20),
        // ),
        child: GridTile(footer: Text(price.toString()), child: Text(id,style: TextStyle(fontWeight: FontWeight.w300,fontSize: 40),),)
      ),
    );
  }
}
