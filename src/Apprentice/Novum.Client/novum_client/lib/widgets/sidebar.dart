import 'package:flutter/material.dart';
import 'package:novum_client/widgets/functionbutton.dart';

class SideBar extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    double width = MediaQuery.of(context).size.width;
    double height = MediaQuery.of(context).size.height;

    return Container(
        width: width * 0.7,
        child: Drawer(
          child: ListView(
            padding: EdgeInsets.zero,
            children: <Widget>[
              Container(
                height: height*0.15,
                child: DrawerHeader(
                  child: Text("Test"),
                  decoration: BoxDecoration(color: Colors.yellow),
                ),
              ),
              ListTile(
                title: Text("Abmelden"),
                onTap: () {},
              ),
              ListTile(
                title: Text("Funktionen"),
                onTap: () {},
              ),
            ],
          ),
        ));
  }
}
