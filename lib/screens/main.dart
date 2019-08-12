 import 'package:flutter/material.dart';

class Main extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    // TODO: implement build
    return Scaffold(
      body: Container(
        decoration: BoxDecoration(
          image: DecorationImage(
            image: AssetImage("images/background1.png"),
            fit: BoxFit.cover,
          ),
        ),
        child:  Column(
          mainAxisAlignment: MainAxisAlignment.spaceEvenly,
          children: <Widget>[
            Image.asset('images/logo.png'),
            Text('Please select an account type below', style: TextStyle(color: Colors.white),),
            Row(
              mainAxisAlignment: MainAxisAlignment.spaceEvenly,
              children: <Widget>[
                RaisedButton(
                  onPressed: (){
                    Navigator.pushNamed(context, 'login');
                  },
                  color: Color(0xFF3b446c),
                  elevation: 4,
                  child: Container(
                    margin : EdgeInsets.all(30),

                    child: new Icon( Icons.drive_eta, color: Colors.white, size: 30)
                    //Text("Driver", style: TextStyle(color: Colors.white),),
                  ),
                ),
                RaisedButton(
                  onPressed: (){
                    Navigator.pushNamed(context, 'login');
                  },
                  color: Color(0xFF3b446c),
                  elevation: 4,
                  child: Container(
                    margin : EdgeInsets.all(30),

                    child: new Icon( Icons.people, color: Colors.white, size: 30,),
                    //Text("Ryder", style: TextStyle(color: Colors.white),),
                  ),
                ),
              ],
            )
          ],
        ),
      ),
    );
  }

 }