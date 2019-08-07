
import 'package:clickandgo/screens/main.dart';
import 'package:flutter/material.dart';
import 'package:splashscreen/splashscreen.dart';

class Splash extends StatefulWidget {
  @override
  SplashState createState() => new SplashState();
}

class SplashState extends State<Splash> {
  @override
  Widget build(BuildContext context) {
    // TODO: implement build
    return new SplashScreen(
      imageBackground: AssetImage("images/background1.png"),
        seconds: 10,
        navigateAfterSeconds: new Main(),
        image: new Image.asset('images/logo.png', width: 150,),
        backgroundColor: Colors.white,
        styleTextUnderTheLoader: new TextStyle(),
        photoSize: 100.0,
        onClick: ()=>print("Flutter Egypt"),
        loaderColor: Colors.red
    );;
  }

}