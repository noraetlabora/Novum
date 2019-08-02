import 'package:example_flutter/widgets/buttons/functionbutton.dart';
import 'package:flutter/material.dart';


class Functions extends StatelessWidget {
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

    return Scaffold(
      appBar: AppBar(
        title: Text("Funktionen"),
      ),
      drawer: Drawer(
        child: (ListView.builder(
          padding: const EdgeInsets.symmetric(vertical: 10),
          itemCount: buttons.length,
          itemBuilder: (context, int index) {
            return Container(height: 50, child: Center(child: buttons[index]));
          },
        )),
      ),
    );
  }
}
