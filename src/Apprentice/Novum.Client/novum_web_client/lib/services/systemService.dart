import 'package:example_flutter/services/protobuf/google/protobuf/empty.pb.dart';

import 'grpc.dart';

class SystemService {
  static Future<String> ping() async {
    try {
      var start = new DateTime.now();
      await Grpc.systemClient.ping(new Empty());
      var stop = new DateTime.now();
      var duration = Duration(
          milliseconds:
              stop.millisecondsSinceEpoch - start.millisecondsSinceEpoch);
      print("ping lastet " + duration.inMilliseconds.toString() + " ms");

      return (duration.inMilliseconds.toString() + " ms");
    } catch (e) {
      print(e);
      //throw e;
    }
  }
}
