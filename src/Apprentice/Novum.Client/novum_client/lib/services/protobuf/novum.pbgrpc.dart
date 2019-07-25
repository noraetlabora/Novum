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
import 'google/protobuf/empty.pb.dart' as $1;
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
  static final _$logout = $grpc.ClientMethod<$1.Empty, $1.Empty>(
      '/Novum.Server.Authentication/Logout',
      ($1.Empty value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $1.Empty.fromBuffer(value));

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

  $grpc.ResponseFuture<$1.Empty> logout($1.Empty request,
      {$grpc.CallOptions options}) {
    final call = $createCall(_$logout, $async.Stream.fromIterable([request]),
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
    $addMethod($grpc.ServiceMethod<$1.Empty, $1.Empty>(
        'Logout',
        logout_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $1.Empty.fromBuffer(value),
        ($1.Empty value) => value.writeToBuffer()));
  }

  $async.Future<$0.InitializeReply> initialize_Pre($grpc.ServiceCall call,
      $async.Future<$0.InitializeRequest> request) async {
    return initialize(call, await request);
  }

  $async.Future<$0.LoginReply> login_Pre(
      $grpc.ServiceCall call, $async.Future<$0.LoginRequest> request) async {
    return login(call, await request);
  }

  $async.Future<$1.Empty> logout_Pre(
      $grpc.ServiceCall call, $async.Future<$1.Empty> request) async {
    return logout(call, await request);
  }

  $async.Future<$0.InitializeReply> initialize(
      $grpc.ServiceCall call, $0.InitializeRequest request);
  $async.Future<$0.LoginReply> login(
      $grpc.ServiceCall call, $0.LoginRequest request);
  $async.Future<$1.Empty> logout($grpc.ServiceCall call, $1.Empty request);
}

class StaticDataClient extends $grpc.Client {
  static final _$getCancellationReasons =
      $grpc.ClientMethod<$1.Empty, $0.CancellationReasons>(
          '/Novum.Server.StaticData/GetCancellationReasons',
          ($1.Empty value) => value.writeToBuffer(),
          ($core.List<$core.int> value) =>
              $0.CancellationReasons.fromBuffer(value));

  StaticDataClient($grpc.ClientChannel channel, {$grpc.CallOptions options})
      : super(channel, options: options);

  $grpc.ResponseFuture<$0.CancellationReasons> getCancellationReasons(
      $1.Empty request,
      {$grpc.CallOptions options}) {
    final call = $createCall(
        _$getCancellationReasons, $async.Stream.fromIterable([request]),
        options: options);
    return $grpc.ResponseFuture(call);
  }
}

abstract class StaticDataServiceBase extends $grpc.Service {
  $core.String get $name => 'Novum.Server.StaticData';

  StaticDataServiceBase() {
    $addMethod($grpc.ServiceMethod<$1.Empty, $0.CancellationReasons>(
        'GetCancellationReasons',
        getCancellationReasons_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $1.Empty.fromBuffer(value),
        ($0.CancellationReasons value) => value.writeToBuffer()));
  }

  $async.Future<$0.CancellationReasons> getCancellationReasons_Pre(
      $grpc.ServiceCall call, $async.Future<$1.Empty> request) async {
    return getCancellationReasons(call, await request);
  }

  $async.Future<$0.CancellationReasons> getCancellationReasons(
      $grpc.ServiceCall call, $1.Empty request);
}

class RuntimeDataClient extends $grpc.Client {
  static final _$getTables = $grpc.ClientMethod<$1.Empty, $0.Tables>(
      '/Novum.Server.RuntimeData/GetTables',
      ($1.Empty value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $0.Tables.fromBuffer(value));

  RuntimeDataClient($grpc.ClientChannel channel, {$grpc.CallOptions options})
      : super(channel, options: options);

  $grpc.ResponseFuture<$0.Tables> getTables($1.Empty request,
      {$grpc.CallOptions options}) {
    final call = $createCall(_$getTables, $async.Stream.fromIterable([request]),
        options: options);
    return $grpc.ResponseFuture(call);
  }
}

abstract class RuntimeDataServiceBase extends $grpc.Service {
  $core.String get $name => 'Novum.Server.RuntimeData';

  RuntimeDataServiceBase() {
    $addMethod($grpc.ServiceMethod<$1.Empty, $0.Tables>(
        'GetTables',
        getTables_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $1.Empty.fromBuffer(value),
        ($0.Tables value) => value.writeToBuffer()));
  }

  $async.Future<$0.Tables> getTables_Pre(
      $grpc.ServiceCall call, $async.Future<$1.Empty> request) async {
    return getTables(call, await request);
  }

  $async.Future<$0.Tables> getTables($grpc.ServiceCall call, $1.Empty request);
}
