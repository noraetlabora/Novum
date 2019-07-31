import 'package:flutter/material.dart';

import 'buttons/bottombutton.dart';

class BottomButtonBar extends StatelessWidget {
  final String id;
  final int amount;
  final String text;
  BottomButtonBar(
      {@required this.amount, @required this.text, @required this.id});

  String getId() {
    return this.id;
  }

  Widget build(BuildContext context) {
    final children = <Widget>[];
    var split = text.split(" ");
    for (var i = 0; i < amount; i++) {
      children.add(new BottomButton(
        amount: amount,
        text: split[i],
        bar: this,
      ));
    }
    return Row(
      mainAxisSize: MainAxisSize
          .max, // this will take space as minimum as posible(to center)
      children: children,
    );
  }
}
