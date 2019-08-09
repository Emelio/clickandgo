import 'package:clickandgo/screens/ryder/login.dart';
import 'package:clickandgo/screens/splash.dart';
import 'package:flutter/material.dart';

void main() => runApp(MaterialApp(
      title: "Click and Go",
      home: Splash(),
      routes: <String, WidgetBuilder>{
    'login': (BuildContext context) => RyderLogin(),
  },
));



