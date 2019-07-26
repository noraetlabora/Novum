import 'package:flutter/material.dart';
import 'package:novum_client/widgets/bottombutton.dart';

class BottomButtonBar extends StatelessWidget {
  final int amount;
  final String text;
  BottomButtonBar({@required this.amount, this.text});

  Widget build(BuildContext context) {
    final children = <Widget>[];
    var split = text.split(" ");
    for (var i = 0; i < amount; i++) {
      children.add(new BottomButton(amount: amount, text: split[i]));
    }
    return Row(
      mainAxisSize: MainAxisSize.max, // this will take space as minimum as posible(to center)
      children: children,
    );
  }
}