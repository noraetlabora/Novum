///
//  Generated code. Do not modify.
//  source: Desktop/novum.proto
//
// @dart = 2.3
// ignore_for_file: camel_case_types,non_constant_identifier_names,library_prefixes,unused_import,unused_shown_name,return_of_invalid_type

import 'dart:async' as $async;

import 'dart:core' as $core show int, String, List;

import 'package:grpc/service_api.dart' as $grpc;
import 'google/protobuf/empty.pb.dart' as $0;
import 'novum.pb.dart' as $1;
export 'novum.pb.dart';

class SystemClient extends $grpc.Client {
  static final _$ping = $grpc.ClientMethod<$0.Empty, $0.Empty>(
      '/Novum.Server.System/Ping',
      ($0.Empty value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $0.Empty.fromBuffer(value));

  SystemClient($grpc.ClientChannel channel, {$grpc.CallOptions options})
      : super(channel, options: options);

  $grpc.ResponseFuture<$0.Empty> ping($0.Empty request,
      {$grpc.CallOptions options}) {
    final call = $createCall(_$ping, $async.Stream.fromIterable([request]),
        options: options);
    return $grpc.ResponseFuture(call);
  }
}

abstract class SystemServiceBase extends $grpc.Service {
  $core.String get $name => 'Novum.Server.System';

  SystemServiceBase() {
    $addMethod($grpc.ServiceMethod<$0.Empty, $0.Empty>(
        'Ping',
        ping_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.Empty.fromBuffer(value),
        ($0.Empty value) => value.writeToBuffer()));
  }

  $async.Future<$0.Empty> ping_Pre(
      $grpc.ServiceCall call, $async.Future<$0.Empty> request) async {
    return ping(call, await request);
  }

  $async.Future<$0.Empty> ping($grpc.ServiceCall call, $0.Empty request);
}

class AuthenticationClient extends $grpc.Client {
  static final _$initialize =
      $grpc.ClientMethod<$1.InitializeRequest, $1.InitializeReply>(
          '/Novum.Server.Authentication/Initialize',
          ($1.InitializeRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) =>
              $1.InitializeReply.fromBuffer(value));
  static final _$login = $grpc.ClientMethod<$1.LoginRequest, $1.LoginReply>(
      '/Novum.Server.Authentication/Login',
      ($1.LoginRequest value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $1.LoginReply.fromBuffer(value));
  static final _$logout = $grpc.ClientMethod<$0.Empty, $0.Empty>(
      '/Novum.Server.Authentication/Logout',
      ($0.Empty value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $0.Empty.fromBuffer(value));

  AuthenticationClient($grpc.ClientChannel channel, {$grpc.CallOptions options})
      : super(channel, options: options);

  $grpc.ResponseFuture<$1.InitializeReply> initialize(
      $1.InitializeRequest request,
      {$grpc.CallOptions options}) {
    final call = $createCall(
        _$initialize, $async.Stream.fromIterable([request]),
        options: options);
    return $grpc.ResponseFuture(call);
  }

  $grpc.ResponseFuture<$1.LoginReply> login($1.LoginRequest request,
      {$grpc.CallOptions options}) {
    final call = $createCall(_$login, $async.Stream.fromIterable([request]),
        options: options);
    return $grpc.ResponseFuture(call);
  }

  $grpc.ResponseFuture<$0.Empty> logout($0.Empty request,
      {$grpc.CallOptions options}) {
    final call = $createCall(_$logout, $async.Stream.fromIterable([request]),
        options: options);
    return $grpc.ResponseFuture(call);
  }
}

abstract class AuthenticationServiceBase extends $grpc.Service {
  $core.String get $name => 'Novum.Server.Authentication';

  AuthenticationServiceBase() {
    $addMethod($grpc.ServiceMethod<$1.InitializeRequest, $1.InitializeReply>(
        'Initialize',
        initialize_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $1.InitializeRequest.fromBuffer(value),
        ($1.InitializeReply value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$1.LoginRequest, $1.LoginReply>(
        'Login',
        login_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $1.LoginRequest.fromBuffer(value),
        ($1.LoginReply value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.Empty, $0.Empty>(
        'Logout',
        logout_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.Empty.fromBuffer(value),
        ($0.Empty value) => value.writeToBuffer()));
  }

  $async.Future<$1.InitializeReply> initialize_Pre($grpc.ServiceCall call,
      $async.Future<$1.InitializeRequest> request) async {
    return initialize(call, await request);
  }

  $async.Future<$1.LoginReply> login_Pre(
      $grpc.ServiceCall call, $async.Future<$1.LoginRequest> request) async {
    return login(call, await request);
  }

  $async.Future<$0.Empty> logout_Pre(
      $grpc.ServiceCall call, $async.Future<$0.Empty> request) async {
    return logout(call, await request);
  }

  $async.Future<$1.InitializeReply> initialize(
      $grpc.ServiceCall call, $1.InitializeRequest request);
  $async.Future<$1.LoginReply> login(
      $grpc.ServiceCall call, $1.LoginRequest request);
  $async.Future<$0.Empty> logout($grpc.ServiceCall call, $0.Empty request);
}

class StaticDataClient extends $grpc.Client {
  static final _$getCancellationReasons =
      $grpc.ClientMethod<$0.Empty, $1.CancellationReasons>(
          '/Novum.Server.StaticData/GetCancellationReasons',
          ($0.Empty value) => value.writeToBuffer(),
          ($core.List<$core.int> value) =>
              $1.CancellationReasons.fromBuffer(value));
  static final _$getTheme = $grpc.ClientMethod<$0.Empty, $1.Theme>(
      '/Novum.Server.StaticData/GetTheme',
      ($0.Empty value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $1.Theme.fromBuffer(value));

  StaticDataClient($grpc.ClientChannel channel, {$grpc.CallOptions options})
      : super(channel, options: options);

  $grpc.ResponseFuture<$1.CancellationReasons> getCancellationReasons(
      $0.Empty request,
      {$grpc.CallOptions options}) {
    final call = $createCall(
        _$getCancellationReasons, $async.Stream.fromIterable([request]),
        options: options);
    return $grpc.ResponseFuture(call);
  }

  $grpc.ResponseFuture<$1.Theme> getTheme($0.Empty request,
      {$grpc.CallOptions options}) {
    final call = $createCall(_$getTheme, $async.Stream.fromIterable([request]),
        options: options);
    return $grpc.ResponseFuture(call);
  }
}

abstract class StaticDataServiceBase extends $grpc.Service {
  $core.String get $name => 'Novum.Server.StaticData';

  StaticDataServiceBase() {
    $addMethod($grpc.ServiceMethod<$0.Empty, $1.CancellationReasons>(
        'GetCancellationReasons',
        getCancellationReasons_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.Empty.fromBuffer(value),
        ($1.CancellationReasons value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.Empty, $1.Theme>(
        'GetTheme',
        getTheme_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.Empty.fromBuffer(value),
        ($1.Theme value) => value.writeToBuffer()));
  }

  $async.Future<$1.CancellationReasons> getCancellationReasons_Pre(
      $grpc.ServiceCall call, $async.Future<$0.Empty> request) async {
    return getCancellationReasons(call, await request);
  }

  $async.Future<$1.Theme> getTheme_Pre(
      $grpc.ServiceCall call, $async.Future<$0.Empty> request) async {
    return getTheme(call, await request);
  }

  $async.Future<$1.CancellationReasons> getCancellationReasons(
      $grpc.ServiceCall call, $0.Empty request);
  $async.Future<$1.Theme> getTheme($grpc.ServiceCall call, $0.Empty request);
}

class RuntimeDataClient extends $grpc.Client {
  static final _$getTables = $grpc.ClientMethod<$0.Empty, $1.Tables>(
      '/Novum.Server.RuntimeData/GetTables',
      ($0.Empty value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $1.Tables.fromBuffer(value));

  RuntimeDataClient($grpc.ClientChannel channel, {$grpc.CallOptions options})
      : super(channel, options: options);

  $grpc.ResponseFuture<$1.Tables> getTables($0.Empty request,
      {$grpc.CallOptions options}) {
    final call = $createCall(_$getTables, $async.Stream.fromIterable([request]),
        options: options);
    return $grpc.ResponseFuture(call);
  }
}

abstract class RuntimeDataServiceBase extends $grpc.Service {
  $core.String get $name => 'Novum.Server.RuntimeData';

  RuntimeDataServiceBase() {
    $addMethod($grpc.ServiceMethod<$0.Empty, $1.Tables>(
        'GetTables',
        getTables_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.Empty.fromBuffer(value),
        ($1.Tables value) => value.writeToBuffer()));
  }

  $async.Future<$1.Tables> getTables_Pre(
      $grpc.ServiceCall call, $async.Future<$0.Empty> request) async {
    return getTables(call, await request);
  }

  $async.Future<$1.Tables> getTables($grpc.ServiceCall call, $0.Empty request);
}
