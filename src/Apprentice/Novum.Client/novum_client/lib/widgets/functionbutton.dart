import 'package:flutter/material.dart';

import 'deviceinformationlist.dart';

class FunctionButton extends StatelessWidget {
  FunctionButton({@required this.buttonText, @required this.widht, @required this.height});
  final String buttonText;
  final double widht;
  final double height;

  
  @override
  Widget build(BuildContext context) {
    // double width = MediaQuery.of(context).size.width;
    // double heigth = MediaQuery.of(context).size.height;
    return ButtonTheme(
      minWidth: widht - 30,
      height: height * 0.1,
      child: RaisedButton(
        shape: new ContinuousRectangleBorder(
            side: BorderSide(color: Colors.white, width: 0.5)),
        textColor: Colors.white,
        onPressed: () {
          switch(buttonText){
              case "Informationen":
                Navigator.push(
                  context,
                  MaterialPageRoute(builder: (context) => DeviceInfo()),
                );
              break;
          }
        },
        child: Text(
          buttonText,
          style: TextStyle(fontSize: 17),
        ),
        color: Color.fromRGBO(100, 100, 100, 1.0),
      ),
    );
  }
}
