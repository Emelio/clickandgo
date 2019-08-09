
import 'dart:async';
import 'package:clickandgo/screens/main.dart';
import 'package:flutter/material.dart';

class Splash extends StatefulWidget {
  @override
  SplashState createState() => new SplashState();
}


class SplashState extends State<Splash> with SingleTickerProviderStateMixin {
  AnimationController _controller;
  Animation<double> _animation;
  Timer _timer;

  
  @override
  void initState() {
    super.initState();
    _controller = AnimationController(
      duration: Duration(milliseconds: 2000),
      vsync: this,
    );

    _animation = Tween(begin: 0.0, end: 1.0).animate(_controller)
      ..addListener(() {
        setState(() {});
      });
      _controller.repeat(reverse: true);

     _timer= new Timer(Duration(seconds: 10),(){
       Navigator.push(context,
      MaterialPageRoute(builder: (context) => Main()));
     });
  
    
  }

  @override
  void dispose() {
    _controller.dispose();
    _timer.cancel();
    super.dispose();
    
      
    
    
  }

  @override
  Widget build(BuildContext context) {
    // TODO: implement build
    return new Container(
    child: new FadeTransition(
      opacity: _animation,
      child:  Center(
      child:  Container(
    decoration: BoxDecoration(
      image: DecorationImage(
        image: AssetImage('images/logo.png'),
        fit: BoxFit.none,
      ),
    ),
    ) ,
    ),
    ),
    color: Colors.white,

  );

    //  new SplashScreen(
    //     imageBackground: AssetImage("images/background1.png"),
    //     seconds: 10,
    //     navigateAfterSeconds: new Main(),
    //     image: new Image.asset('images/logo.png', width: 150,),
    //     backgroundColor: Colors.white,
    //     styleTextUnderTheLoader: new TextStyle(),
    //     photoSize: 100.0,
    //     onClick: ()=>print("Flutter Egypt"),
    //     loaderColor: Colors.red,
        
    // );
    
  }
}


