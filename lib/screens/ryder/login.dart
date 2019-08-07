
import 'package:flutter/material.dart';

class RyderLogin extends StatefulWidget {
  State<RyderLogin> createState() => RyderLoginState();
}

class RyderLoginState extends State<RyderLogin> {
  @override
  Widget build(BuildContext context) {
    // TODO: implement build
    return Scaffold(
      body:Container(
        decoration: BoxDecoration(
          image: DecorationImage(
            image: AssetImage("images/background1.png"),
            fit: BoxFit.cover,
          ),
        ),
        child:  Stack(
          children: <Widget>[

            Align(
              alignment: Alignment.center,
                child: Container(
                  padding: EdgeInsets.only(top:60),
                  margin: EdgeInsets.all(20),
                  child: Column(
                    children: <Widget>[
                      Padding(
                        padding: EdgeInsets.symmetric(vertical: 10),
                      child: TextField(
                        decoration: InputDecoration(
                        hintText: "Password",
                        hintStyle: TextStyle(color: Colors.white),
                        icon: Icon(Icons.mail),
                        enabledBorder: UnderlineInputBorder(
                          borderSide: BorderSide(color: Colors.white),
                        ),
                        focusedBorder: UnderlineInputBorder(
                          borderSide: BorderSide(color: Colors.white),
                        ),
                      )
                    ),
                  ),
                      Padding(
                        padding: EdgeInsets.symmetric(vertical: 10),
                        child: TextField(
                            decoration: InputDecoration(
                              hintText: "please enter email",
                              hintStyle: TextStyle(color: Colors.white),
                              icon: Icon(Icons.security),
                              enabledBorder: UnderlineInputBorder(
                                borderSide: BorderSide(color: Colors.white),
                              ),
                              focusedBorder: UnderlineInputBorder(
                                borderSide: BorderSide(color: Colors.white),
                              ),
                            )
                        ),
                      ),
                    Padding(
                      padding: EdgeInsets.only(top:20),
                      child: SizedBox(

                          width: double.infinity, // match_parent
                          height: 45,
                          child: RaisedButton(
                            onPressed: (){},
                            child: Text("login"),
                          )
                      ),
                    )

                    ],
                  ),
                )
            ),
            Align(
              alignment: Alignment.bottomCenter,
                    child: Container(
                      margin: EdgeInsets.all(20),
                      child: Row(
                        mainAxisAlignment: MainAxisAlignment.spaceBetween,
                        children: <Widget>[
                          Text("Don't have an account"),
                          Text("Forget Password")
                        ],
                      ),
                    )
                )

          ],
        ),
      )


    );
  }

}