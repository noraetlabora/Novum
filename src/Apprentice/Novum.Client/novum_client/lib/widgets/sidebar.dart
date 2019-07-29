import 'package:flutter/material.dart';
import 'package:novum_client/widgets/functionbutton.dart';

class SideBar extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    double width = MediaQuery.of(context).size.width;
    double heigth = MediaQuery.of(context).size.height;
    final List<FunctionButton> buttons = <FunctionButton>[];
    final List<String> buttonNames = <String>[];
    buttonNames.add("Abmelden");
    buttonNames.add("Informationen");

    for (int i = 0; i < buttonNames.length; i++) {
      buttons.add(new FunctionButton(
          buttonText: buttonNames[i], height: heigth, widht: width));
    }

    return Drawer(
      child: ListView(
        padding: EdgeInsets.zero,
        children: <Widget>[
          DrawerHeader(
            child: Text("Test"),
            decoration: BoxDecoration(color: Colors.yellow),
            ),
          ListTile(
            title: Text("Abmelden"),
            onTap: (){},
          ),
          ListTile(
            title: Text("Funktionen"),
            onTap: (){},
          ),
      ],
      ),
    );
  }
}
