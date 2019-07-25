///
//  Generated code. Do not modify.
//  source: novum.proto
//
// @dart = 2.3
// ignore_for_file: camel_case_types,non_constant_identifier_names,library_prefixes,unused_import,unused_shown_name,return_of_invalid_type

import 'dart:async' as $async;

import 'package:protobuf/protobuf.dart' as $pb;

import 'dart:core' as $core show String, Map, ArgumentError, dynamic;
import 'novum.pb.dart' as $0;
import 'novum.pbjson.dart';

export 'novum.pb.dart';

abstract class AuthenticationServiceBase extends $pb.GeneratedService {
  $async.Future<$0.InitializeReply> initialize($pb.ServerContext ctx, $0.InitializeRequest request);
  $async.Future<$0.LoginReply> login($pb.ServerContext ctx, $0.LoginRequest request);
  $async.Future<$0.LogoutReply> logou($pb.ServerContext ctx, $0.LogoutRequest request);

  $pb.GeneratedMessage createRequest($core.String method) {
    switch (method) {
      case 'Initialize': return $0.InitializeRequest();
      case 'Login': return $0.LoginRequest();
      case 'Logou': return $0.LogoutRequest();
      default: throw $core.ArgumentError('Unknown method: $method');
    }
  }

  $async.Future<$pb.GeneratedMessage> handleCall($pb.ServerContext ctx, $core.String method, $pb.GeneratedMessage request) {
    switch (method) {
      case 'Initialize': return this.initialize(ctx, request);
      case 'Login': return this.login(ctx, request);
      case 'Logou': return this.logou(ctx, request);
      default: throw $core.ArgumentError('Unknown method: $method');
    }
  }

  $core.Map<$core.String, $core.dynamic> get $json => AuthenticationServiceBase$json;
  $core.Map<$core.String, $core.Map<$core.String, $core.dynamic>> get $messageJson => AuthenticationServiceBase$messageJson;
}

