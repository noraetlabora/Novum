import 'package:flutter/material.dart';
import 'package:flutter/rendering.dart';
import 'package:permission_handler/permission_handler.dart';
import 'package:android_device_info/android_device_info.dart';

import '../utils/row_item.dart';

const memoryUnit = 'MB';

class DeviceInfo extends StatefulWidget {
  @override
  _NetworkTabState createState() => _NetworkTabState();
}

class _NetworkTabState extends State<DeviceInfo> {
  var data = {};
  var bData = {};

  @override
  void initState() {
    super.initState();
    AndroidDeviceInfo().getBatteryInfo().then((bData) {
      if (mounted) {
        setState(() {
          this.bData = bData;
        });
      }
    });
    getData();
  }

  getData() async {
    var data = {};

    var network = await AndroidDeviceInfo().getNetworkInfo();
    var sim = await AndroidDeviceInfo().getSimInfo();
    var memory = await AndroidDeviceInfo().getMemoryInfo(unit: memoryUnit);
    var display = await AndroidDeviceInfo().getDisplayInfo();
    var dInfo = await AndroidDeviceInfo().getSystemInfo();
    var nfc = await AndroidDeviceInfo().getNfcInfo();

    data.addAll(memory);
    data.addAll(display);
    data.addAll(sim);
    data.addAll(network);
    data.addAll(dInfo);
    data.addAll(nfc);

    if (mounted) {
      setState(() {
        this.data = data;
      });
    }

    var permission =
        await PermissionHandler().checkPermissionStatus(PermissionGroup.phone);
    if (permission == PermissionStatus.denied) {
      var permissions =
          await PermissionHandler().requestPermissions([PermissionGroup.phone]);
      if (permissions[PermissionGroup.phone] == PermissionStatus.granted) {
        data.addAll(memory);
        data.addAll(display);
        data.addAll(sim);
        data.addAll(network);
        data.addAll(dInfo);
        data.addAll(nfc);
        if (mounted) {
          setState(() {
            this.data = data;
          });
        }
      }
    }
  }

  @override
  Widget build(BuildContext context) {
    if (data.isEmpty) {
      return Center(child: CircularProgressIndicator());
    }
    final totalRam = data['totalRAM'].round();
    final totalExternalMemorySize = data['totalExternalMemorySize'].round();
    final totalInternalMemorySize = data['totalInternalMemorySize'].round();
    final availableExternalMemorySize =
        data['availableExternalMemorySize'].round();
    final availableInternalMemorySize =
        data['availableInternalMemorySize'].round();

    return SingleChildScrollView(
      child: Column(
        children: <Widget>[
          Divider(),
          RowItem('Model', data['model']),
          RowItem('Product', data['product']),
          RowItem('Device Type', data['deviceType']),
          RowItem('Android Version', data['osVersion']),
          Divider(),
          RowItem('Display Resolution', data['resolution']),
          RowItem(
            'Physical Size',
            data['physicalSize'].toStringAsFixed(2) + ' in',
          ),
          Divider(),
          RowItem('Total RAM', '$totalRam $memoryUnit'),
          RowItem(
            'Total Internal Memory',
            '$totalInternalMemorySize $memoryUnit',
          ),
          RowItem(
            'Available Internal Memory',
            '$availableInternalMemorySize $memoryUnit',
          ),
          RowItem(
            'Total External Storage',
            '$totalExternalMemorySize $memoryUnit',
          ),
          RowItem('Available External Storage',
              '$availableExternalMemorySize $memoryUnit'),
          Divider(),
          RowItem('Charge Level', '${bData['batteryPercentage']}%'),
          RowItem('Health', bData['batteryHealth']),
          RowItem('is Charging', '${bData['isDeviceCharging']}'),
          RowItem('Source', '${bData['chargingSource']}'),
          RowItem('Technology', bData['batteryTechnology']),
          RowItem('Temperature', '${bData['batteryTemperature']}°c'),
          RowItem('Voltage', '${bData['batteryVoltage']}'),
          Divider(),
          RowItem('Network Available', '${data['isNetworkAvailable']}'),
          RowItem('Network', '${data['networkType']}'),
          RowItem('iPv4 Address', '${data['iPv4Address']}'),
          RowItem('iPv6 Address', '${data['iPv6Address']}'),
          RowItem('WiFi Enabled', '${data['isWifiEnabled']}'),
          Divider(),
          RowItem('NFC Present', '${data['isNfcPresent']}'),
          RowItem('NFC Enabled', '${data['isNfcEnabled']}'),
          Divider(),
        ],
      ),
    );
  }
}
