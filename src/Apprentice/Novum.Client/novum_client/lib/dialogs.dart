import 'package:flutter/material.dart';
import 'package:flutter/services.dart';

class dialogSelection {
  
  static void InformationDialog(
      String title, String message, BuildContext context) {
    _displayDialog(context) async {
      return showDialog(
          context: context,
          builder: (context) {
            return AlertDialog(
              title: Text(title),
              content: Text(message),
              actions: <Widget>[
                new FlatButton(
                  textColor: Colors.black,
                  child: new Text('OK'),
                  onPressed: () {
                    Navigator.of(context).pop();
                  },
                ),
              ],
            );
          });
    }

    _displayDialog(context);
  }

  static void errorDialog(
      int errorcode, String errorMessage, int level, BuildContext context) {
    _displayDialog(context) async {
      return showDialog(
          context: context,
          builder: (context) {
            return AlertDialog(
              title: Text("ERROR"),
              content: Text(errorcode.toString() + " - " + errorMessage),
              actions: <Widget>[
                new FlatButton(
                  textColor: Colors.black,
                  child: new Text('OK'),
                  onPressed: () {
                    Navigator.of(context).pop();
                  },
                ),
              ],
              backgroundColor: Colors.red,
            );
          });
    }

    _displayDialog(context);
  }


  static void inputDialog(BuildContext context, String title, String hintText, String button1, String button2){
    TextEditingController tfController = TextEditingController();
     _displayDialog(BuildContext context) async {
    return showDialog(
        context: context,
        builder: (context) {
          return AlertDialog(
            title: Text(title),
            content: TextField(
              controller: tfController,
              decoration: InputDecoration(hintText: hintText),
            ),
            actions: <Widget>[
              new FlatButton(
                textColor: Colors.black,
                child: new Text(button1),
                onPressed: () {
                  Navigator.of(context).pop();
                } ,
              ),
              new FlatButton(
                textColor: Colors.black,
                child: new Text(button2),
                onPressed: () {
                  Navigator.of(context).pop();
                } ,
              )
            ],
          );
        });
  }

  _displayDialog(context);
  }
}
