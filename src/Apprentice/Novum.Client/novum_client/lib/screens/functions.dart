import 'package:flutter/material.dart';
import 'package:novum_client/widgets/functionbutton.dart';

class Functions extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    double width = MediaQuery.of(context).size.width;
    double heigth = MediaQuery.of(context).size.height;
    final List<FunctionButton> buttons = <FunctionButton>[];
    buttons.add(new FunctionButton(
      buttonText: "Abmelden",
      widht: width,
      height: heigth,
    ));
    buttons.add(new FunctionButton(
      buttonText: "Informationen",
      widht: width,
      height: heigth,
    ));

    return Column(
      children: <Widget>[
      ListView.separated(
        padding: const EdgeInsets.all(8.0),
        itemCount: buttons.length,
        itemBuilder: (BuildContext context, int index) {
          return Container(height: 50, child: Center(child: buttons[index]));
        },
        separatorBuilder: (BuildContext context, int index) => const Divider(),
      )
    ]);
  }
}
