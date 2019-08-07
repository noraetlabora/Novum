///
//  Generated code. Do not modify.
//  source: Desktop/novum.proto
//
// @dart = 2.3
// ignore_for_file: camel_case_types,non_constant_identifier_names,library_prefixes,unused_import,unused_shown_name,return_of_invalid_type

const ClientType$json = const {
  '1': 'ClientType',
  '2': const [
    const {'1': 'ORDERMAN', '2': 0},
    const {'1': 'WINDOWS', '2': 1},
    const {'1': 'WEB', '2': 2},
  ],
};

const LoginInputType$json = const {
  '1': 'LoginInputType',
  '2': const [
    const {'1': 'MANUALLY', '2': 0},
    const {'1': 'KEY', '2': 1},
  ],
};

const TableState$json = const {
  '1': 'TableState',
  '2': const [
    const {'1': 'ORDERED', '2': 0},
    const {'1': 'WAITING', '2': 1},
    const {'1': 'IMPATIENT', '2': 2},
  ],
};

const InitializeRequest$json = const {
  '1': 'InitializeRequest',
  '2': const [
    const {'1': 'id', '3': 1, '4': 1, '5': 9, '10': 'id'},
    const {'1': 'clientType', '3': 2, '4': 1, '5': 14, '6': '.Novum.Server.ClientType', '10': 'clientType'},
    const {'1': 'clientVersion', '3': 3, '4': 1, '5': 9, '10': 'clientVersion'},
    const {'1': 'test', '3': 4, '4': 1, '5': 13, '10': 'test'},
  ],
};

const InitializeReply$json = const {
  '1': 'InitializeReply',
  '2': const [
    const {'1': 'unixTimestamp', '3': 1, '4': 1, '5': 13, '10': 'unixTimestamp'},
  ],
};

const LoginRequest$json = const {
  '1': 'LoginRequest',
  '2': const [
    const {'1': 'input', '3': 1, '4': 1, '5': 9, '10': 'input'},
    const {'1': 'inputType', '3': 2, '4': 1, '5': 14, '6': '.Novum.Server.LoginInputType', '10': 'inputType'},
  ],
};

const LoginReply$json = const {
  '1': 'LoginReply',
};

const GetTablesRequest$json = const {
  '1': 'GetTablesRequest',
};

const GetTablesReply$json = const {
  '1': 'GetTablesReply',
  '2': const [
    const {'1': 'tables', '3': 1, '4': 3, '5': 11, '6': '.Novum.Server.Table', '10': 'tables'},
  ],
};

const Tables$json = const {
  '1': 'Tables',
  '2': const [
    const {'1': 'tables', '3': 1, '4': 3, '5': 11, '6': '.Novum.Server.Table', '10': 'tables'},
  ],
};

const Table$json = const {
  '1': 'Table',
  '2': const [
    const {'1': 'id', '3': 1, '4': 1, '5': 9, '10': 'id'},
    const {'1': 'name', '3': 2, '4': 1, '5': 9, '10': 'name'},
    const {'1': 'amount', '3': 3, '4': 1, '5': 1, '10': 'amount'},
    const {'1': 'waiterId', '3': 4, '4': 1, '5': 9, '10': 'waiterId'},
    const {'1': 'guests', '3': 5, '4': 1, '5': 13, '10': 'guests'},
    const {'1': 'state', '3': 6, '4': 1, '5': 14, '6': '.Novum.Server.TableState', '10': 'state'},
  ],
};

const CancellationReasons$json = const {
  '1': 'CancellationReasons',
  '2': const [
    const {'1': 'cancellationReasons', '3': 1, '4': 3, '5': 11, '6': '.Novum.Server.CancellationReason', '10': 'cancellationReasons'},
  ],
};

const CancellationReason$json = const {
  '1': 'CancellationReason',
  '2': const [
    const {'1': 'id', '3': 1, '4': 1, '5': 9, '10': 'id'},
    const {'1': 'name', '3': 2, '4': 1, '5': 9, '10': 'name'},
  ],
};

const Theme$json = const {
  '1': 'Theme',
  '2': const [
    const {'1': 'primary', '3': 1, '4': 1, '5': 13, '10': 'primary'},
    const {'1': 'primaryVariant', '3': 2, '4': 1, '5': 13, '10': 'primaryVariant'},
    const {'1': 'secondary', '3': 3, '4': 1, '5': 13, '10': 'secondary'},
    const {'1': 'secondaryVariant', '3': 4, '4': 1, '5': 13, '10': 'secondaryVariant'},
    const {'1': 'background', '3': 10, '4': 1, '5': 13, '10': 'background'},
    const {'1': 'surface', '3': 11, '4': 1, '5': 13, '10': 'surface'},
    const {'1': 'onPrimary', '3': 20, '4': 1, '5': 13, '10': 'onPrimary'},
    const {'1': 'onSecondary', '3': 21, '4': 1, '5': 13, '10': 'onSecondary'},
    const {'1': 'onBackground', '3': 22, '4': 1, '5': 13, '10': 'onBackground'},
    const {'1': 'onSurface', '3': 23, '4': 1, '5': 13, '10': 'onSurface'},
  ],
};

