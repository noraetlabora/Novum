import 'package:flutter/material.dart';

class StatusBar extends StatelessWidget {
  Widget build(BuildContext context) {
    double heigth = MediaQuery.of(context).size.height;
    return Container(
      height: heigth * 0.0469,
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
            children: <Widget>[],
          )
        ],
      ),
    );
  }
}
