///
//  Generated code. Do not modify.
//  source: novum.proto
//
// @dart = 2.3
// ignore_for_file: camel_case_types,non_constant_identifier_names,library_prefixes,unused_import,unused_shown_name,return_of_invalid_type

import 'dart:async' as $async;

import 'dart:core' as $core show int, String, List;

import 'package:grpc/service_api.dart' as $grpc;
import 'novum.pb.dart' as $0;
export 'novum.pb.dart';

class AuthenticationClient extends $grpc.Client {
  static final _$initialize =
      $grpc.ClientMethod<$0.InitializeRequest, $0.InitializeReply>(
          '/Novum.Server.Authentication/Initialize',
          ($0.InitializeRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) =>
              $0.InitializeReply.fromBuffer(value));
  static final _$login = $grpc.ClientMethod<$0.LoginRequest, $0.LoginReply>(
      '/Novum.Server.Authentication/Login',
      ($0.LoginRequest value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $0.LoginReply.fromBuffer(value));
  static final _$logout = $grpc.ClientMethod<$0.LogoutRequest, $0.LogoutReply>(
      '/Novum.Server.Authentication/Logout',
      ($0.LogoutRequest value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $0.LogoutReply.fromBuffer(value));
  static final _$getTables =
      $grpc.ClientMethod<$0.GetTablesRequest, $0.GetTablesReply>(
          '/Novum.Server.Authentication/GetTables',
          ($0.GetTablesRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $0.GetTablesReply.fromBuffer(value));

  AuthenticationClient($grpc.ClientChannel channel, {$grpc.CallOptions options})
      : super(channel, options: options);

  $grpc.ResponseFuture<$0.InitializeReply> initialize(
      $0.InitializeRequest request,
      {$grpc.CallOptions options}) {
    final call = $createCall(
        _$initialize, $async.Stream.fromIterable([request]),
        options: options);
    return $grpc.ResponseFuture(call);
  }

  $grpc.ResponseFuture<$0.LoginReply> login($0.LoginRequest request,
      {$grpc.CallOptions options}) {
    final call = $createCall(_$login, $async.Stream.fromIterable([request]),
        options: options);
    return $grpc.ResponseFuture(call);
  }

  $grpc.ResponseFuture<$0.LogoutReply> logout($0.LogoutRequest request,
      {$grpc.CallOptions options}) {
    final call = $createCall(_$logout, $async.Stream.fromIterable([request]),
        options: options);
    return $grpc.ResponseFuture(call);
  }

  $grpc.ResponseFuture<$0.GetTablesReply> getTables($0.GetTablesRequest request,
      {$grpc.CallOptions options}) {
    final call = $createCall(_$getTables, $async.Stream.fromIterable([request]),
        options: options);
    return $grpc.ResponseFuture(call);
  }
}

abstract class AuthenticationServiceBase extends $grpc.Service {
  $core.String get $name => 'Novum.Server.Authentication';

  AuthenticationServiceBase() {
    $addMethod($grpc.ServiceMethod<$0.InitializeRequest, $0.InitializeReply>(
        'Initialize',
        initialize_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.InitializeRequest.fromBuffer(value),
        ($0.InitializeReply value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.LoginRequest, $0.LoginReply>(
        'Login',
        login_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.LoginRequest.fromBuffer(value),
        ($0.LoginReply value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.LogoutRequest, $0.LogoutReply>(
        'Logout',
        logout_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.LogoutRequest.fromBuffer(value),
        ($0.LogoutReply value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.GetTablesRequest, $0.GetTablesReply>(
        'GetTables',
        getTables_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.GetTablesRequest.fromBuffer(value),
        ($0.GetTablesReply value) => value.writeToBuffer()));
  }

  $async.Future<$0.InitializeReply> initialize_Pre($grpc.ServiceCall call,
      $async.Future<$0.InitializeRequest> request) async {
    return initialize(call, await request);
  }

  $async.Future<$0.LoginReply> login_Pre(
      $grpc.ServiceCall call, $async.Future<$0.LoginRequest> request) async {
    return login(call, await request);
  }

  $async.Future<$0.LogoutReply> logout_Pre(
      $grpc.ServiceCall call, $async.Future<$0.LogoutRequest> request) async {
    return logout(call, await request);
  }

  $async.Future<$0.GetTablesReply> getTables_Pre($grpc.ServiceCall call,
      $async.Future<$0.GetTablesRequest> request) async {
    return getTables(call, await request);
  }

  $async.Future<$0.InitializeReply> initialize(
      $grpc.ServiceCall call, $0.InitializeRequest request);
  $async.Future<$0.LoginReply> login(
      $grpc.ServiceCall call, $0.LoginRequest request);
  $async.Future<$0.LogoutReply> logout(
      $grpc.ServiceCall call, $0.LogoutRequest request);
  $async.Future<$0.GetTablesReply> getTables(
      $grpc.ServiceCall call, $0.GetTablesRequest request);
}
