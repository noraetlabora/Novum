import 'package:novum_client/services/grpc.dart';
import 'package:novum_client/services/protobuf/google/protobuf/empty.pb.dart';

class SystemService {

  static Future ping() async {
    try {
      var start = new DateTime.now();
      await Grpc.systemClient.ping(new Empty());
      var stop = new DateTime.now();
      var duration = Duration(milliseconds: stop.millisecondsSinceEpoch - start.millisecondsSinceEpoch);
      print("ping lastet " + duration.inMilliseconds.toString() + " ms");
    }
    catch(e) { 
      print(e); 
   } 
  }
}