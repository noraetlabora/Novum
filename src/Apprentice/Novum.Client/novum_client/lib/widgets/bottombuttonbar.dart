import 'package:flutter/material.dart';

import 'buttons/bottombutton.dart';

class BottomButtonBar extends StatelessWidget {
  final List<BottomButton> buttons;
  BottomButtonBar({@required this.buttons});

  Widget build(BuildContext context) {
    double width = MediaQuery.of(context).size.width;
    final children = <Widget>[];
    for (int i = 0; i < buttons.length; i++) {
      BottomButton button = buttons[i];
      button.width = width / (buttons.length);
      children.add(button);
    }

    return Row(
      mainAxisSize: MainAxisSize
          .max, // this will take space as minimum as posible(to center)
      children: children,
    );
  }
}
